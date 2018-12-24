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
        private readonly string _partitionKey = "posts";

        public AzureTablePostRepository(IAzureTableStorage<Post> azureTableStorage)
        {
            this.azureTableStorage = azureTableStorage;
        }

        public async Task<Post> Add(Post entity)
        {
            entity.PartitionKey = _partitionKey;
            entity.RowKey = Guid.NewGuid().ToString();
            await azureTableStorage.Insert(entity);
            return entity;
        }

        public async Task Delete(Post entity)
        {
            await azureTableStorage.Delete(_partitionKey, entity.RowKey);
        }

        public async Task<Post> GetById(string id)
        {
            return await azureTableStorage.GetItem(_partitionKey, id);
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

        public async Task<Post> GetPostBySlug(string slug, bool authenticated = false)
        {
            if (authenticated)
            {
                var posts = await ListAll();
                return posts.SingleOrDefault(x => x.Slug == slug);
            }
            else
            {
                var posts = await GetPublicPosts();
                return posts.SingleOrDefault(x => x.Slug == slug);
            }
        }

        public async Task<IEnumerable<Post>> GetPostsByCategory(string category)
        {
            var posts = await GetPublicPosts();
            return posts.Where(x => x.PostCategories.Contains(category));
        }

        public async Task<IEnumerable<Post>> GetPublicPosts()
        {
            var list = await ListAll();
            return list.Where(p => (p.IsPublished == true && p.PubDate <= DateTime.UtcNow));
        }

        public async Task<List<Post>> ListAll()
        {
            return await azureTableStorage.GetList();
        }

        public async Task Update(Post entity)
        {
            await azureTableStorage.Update(entity);
        }
    }
}
