using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyBlog.Core.Entities;
using TinyBlog.Core.Interfaces;

namespace TinyBlog.Infrastructure.Repos
{
    public class AzureTablePostRepository : IPostRepository
    {
        private readonly CloudStorageAccount cloudStorageAccount;
        private readonly IConfiguration configuration;
        private readonly CloudTableClient tableClient;
        private readonly CloudTable table;

        public AzureTablePostRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            cloudStorageAccount = CloudStorageAccount.Parse(configuration["AppSettings:StorageConnectionString"]);
            // Create table if it does not exist.
            tableClient = cloudStorageAccount.CreateCloudTableClient();
            table = tableClient.GetTableReference("Posts");
            table.CreateIfNotExists();
        }

        public async Task<Post> Add(Post entity)
        {
            entity.PartitionKey = GetPartitionKey(entity);
            entity.RowKey = Guid.NewGuid().ToString();
            TableOperation tableOperation = TableOperation.Insert(entity);
            await table.ExecuteAsync(tableOperation);
            return entity;
        }

        public async Task Delete(Post entity)
        {
            TableOperation tableOperation = TableOperation.Retrieve<Post>(GetPartitionKey(entity), entity.RowKey);
            TableResult tableResult = await table.ExecuteAsync(tableOperation);

            Post deleteItem = (Post)tableResult.Result;

            if (deleteItem != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteItem);
                await table.ExecuteAsync(deleteOperation);
            }
        }

        public async Task<Post> GetById(string id, string partitionKey = "")
        {
            TableOperation tableOperation = TableOperation.Retrieve<Post>(partitionKey, id);
            TableResult tableResult = await table.ExecuteAsync(tableOperation);

            return (Post)tableResult.Result;
        }

        public async Task<Dictionary<string, int>> GetCategories()
        {
            var posts = await GetPublicPosts();
            return posts
                .SelectMany(p => p.PostCategories)
                .GroupBy(c => c, (c, items) => new { Category = c, Count = items.Count() })
                .OrderBy(x => x.Category)
                .ToDictionary(x => x.Category, x => x.Count);
        }

        public async Task<Post> GetPostBySlug(string slug)
        {
            var tableQuery = new TableQuery<Post>().Where(TableQuery.GenerateFilterCondition("Slug", QueryComparisons.Equal, slug)).Take(1);
            IEnumerable<Post> tableResult = await Task.Run(() => table.ExecuteQuery(tableQuery));
            if (tableResult.Any())
                return tableResult.FirstOrDefault();
            return null;
        }

        public async Task<IEnumerable<Post>> GetPostsByCategory(string category)
        {
            var posts = await GetPublicPosts();
            return posts.Where(x => x.PostCategories.Contains(category));
        }

        public async Task<IEnumerable<Post>> GetPublicPosts()
        {
            TableQuery<Post> tableQuery = new TableQuery<Post>().Where(TableQuery.CombineFilters(
                TableQuery.GenerateFilterConditionForBool("IsPublished", QueryComparisons.Equal, true),
                TableOperators.And,
                TableQuery.GenerateFilterConditionForDate("PubDate", QueryComparisons.GreaterThanOrEqual, DateTime.UtcNow)));
            return await Task.Run(() => table.ExecuteQuery(tableQuery));
        }

        public async Task<List<Post>> ListAll()
        {
            var tableQuery = new TableQuery<Post>();
            return await Task.Run(() => table.ExecuteQuery(tableQuery).ToList());
        }

        public async Task Update(Post entity)
        {
            entity.PartitionKey = GetPartitionKey(entity);
            TableOperation tableOperation = TableOperation.InsertOrReplace(entity);
            await table.ExecuteAsync(tableOperation);
        }

        private string GetPartitionKey(Post post)
        {
            return post.PubDate.ToString("yyyy-MM");
        }
    }
}
