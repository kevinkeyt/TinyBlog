using Microsoft.Azure.Cosmos.Table;
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
        private readonly IAzureTableStorage<Post> azureTableStorage;

        public AzureTablePostRepository(IAzureTableStorage<Post> azureTableStorage)
        {
            this.azureTableStorage = azureTableStorage;
        }

        public async Task<Post> Add(Post entity)
        {
            entity.PartitionKey = GetPartitionKey(entity);
            entity.RowKey = Guid.NewGuid().ToString();
            await azureTableStorage.Insert(entity);
            return entity;
        }

        public async Task Delete(Post entity)
        {
            await azureTableStorage.Delete(entity.PartitionKey, entity.RowKey);
        }

        public async Task<Post> GetById(string id, string partitionKey = "")
        {
            return await azureTableStorage.GetItem(partitionKey, id);
        }

        public async Task<Dictionary<string, int>> GetCategories()
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            var posts = await GetPublicPosts();
            var categories = posts.SelectMany(p => p.GetPostCategories()).OrderBy(x => x);

            foreach(var c in categories)
            {
                var key = c.Trim();
                int val;
                if(list.TryGetValue(key, out val))
                {
                    list[key] = val + 1;
                }
                else
                {
                    list.Add(key, 1);
                }
            }
            return list;
        }

        public async Task<Post> GetPostBySlug(string slug)
        {
            var table = await azureTableStorage.GetTableAsync();
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
            var table = await azureTableStorage.GetTableAsync();
            TableQuery<Post> tableQuery = new TableQuery<Post>().Where(TableQuery.CombineFilters(
                TableQuery.GenerateFilterConditionForBool("IsPublished", QueryComparisons.Equal, true),
                TableOperators.And,
                TableQuery.GenerateFilterConditionForDate("PubDate", QueryComparisons.LessThanOrEqual, DateTimeOffset.UtcNow)));
            var qry =  await Task.Run(() => table.ExecuteQuery(tableQuery));
            return qry.OrderByDescending(x => x.PubDate);
        }

        public async Task<List<Post>> ListAll()
        {
            return await azureTableStorage.GetList();
        }

        public async Task Update(Post entity)
        {
            entity.PartitionKey = GetPartitionKey(entity);
            await azureTableStorage.Update(entity);
        }

        private string GetPartitionKey(Post post)
        {
            return post.PubDate.ToString("yyyy-MM");
        }
    }
}
