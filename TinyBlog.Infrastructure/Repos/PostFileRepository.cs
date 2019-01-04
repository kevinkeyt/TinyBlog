using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TinyBlog.Core.Entities;
using TinyBlog.Core.Interfaces;

namespace TinyBlog.Infrastructure.Repos
{
    public class PostRepository : IPostRepository
    {
        private readonly IHostingEnvironment environment;
        private readonly IDomainEventDispatcher dispatcher;
        private readonly IMemoryCache memoryCache;
        private readonly MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(24));
        private readonly string folder;

        public PostRepository(IHostingEnvironment environment, IMemoryCache memoryCache, IDomainEventDispatcher dispatcher)
        {
            this.environment = environment;
            this.memoryCache = memoryCache;
            this.dispatcher = dispatcher;
            folder = environment.ContentRootPath + "\\Data\\Posts\\";
        }

        public async Task Delete(Post entity)
        {
            var file = Path.Combine(folder, entity.RowKey + ".json");
            File.Delete(file);
            memoryCache.Remove("posts");
            await ListAll();
        }

        public async Task<Post> Add(Post entity)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (string.IsNullOrEmpty(entity.RowKey))
            {
                entity.RowKey = Guid.NewGuid().ToString();
            }

            var file = Path.Combine(folder, entity.RowKey + ".json");
            if (!File.Exists(file))
            {
                entity.LastModified = DateTime.UtcNow;
                var json = JsonConvert.SerializeObject(entity);
                var posts = await ListAll();
                posts.Insert(0, entity);
                posts.Sort((p1, p2) => p2.PubDate.CompareTo(p1.PubDate));
                memoryCache.Remove("posts");
                memoryCache.Set("posts", posts, cacheOptions);
                await File.WriteAllTextAsync(file, json);

                // Update Domain Events
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach (var domainEvent in events)
                    dispatcher.Dispatch(domainEvent);
            }
            return entity;
        }

        public async Task<Post> GetById(string Id, string partitionKey = "")
        {
            var list = await ListAll();
            return list.SingleOrDefault(x => x.RowKey == Id);
        }

        public async Task<Dictionary<string, int>> GetCategories()
        {
            var list = await GetPublicPosts();
            return list.SelectMany(p => p.GetPostCategories())
                .GroupBy(c => c, (c, items) => new { Category = c, Count = items.Count() })
                .OrderBy(x => x.Category)
                .ToDictionary(x => x.Category, x => x.Count);
        }

        public async Task<Post> GetPostBySlug(string slug)
        {
            var list = await GetPublicPosts();
            return list.SingleOrDefault(x => x.Slug == slug);
        }

        public async Task<IEnumerable<Post>> GetPostsByCategory(string category)
        {
            var list = await GetPublicPosts();
            return list.Where(x => x.PostCategories.Contains(category));
        }

        public async Task<IEnumerable<Post>> GetPublicPosts()
        {
            var list = await ListAll();
            return list.Where(p => (p.IsPublished == true && p.PubDate <= DateTime.UtcNow));
        }

        public async Task<List<Post>> ListAll()
        {
            List<Post> posts;
            if (!memoryCache.TryGetValue("posts", out posts))
            {
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                posts = new List<Post>();

                foreach (var file in Directory.EnumerateFiles(folder, "*.json", SearchOption.TopDirectoryOnly))
                {
                    var content = await File.ReadAllTextAsync(file);
                    posts.Add(JsonConvert.DeserializeObject<Post>(content));
                }

                if (posts.Count > 0)
                {
                    posts.Sort((p1, p2) => p2.PubDate.CompareTo(p1.PubDate));
                }
                memoryCache.Set("posts", posts, cacheOptions);
            }
            return posts;
        }

        public async Task Update(Post entity)
        {
            var file = Path.Combine(folder, entity.RowKey + ".json");
            if (File.Exists(file))
            {
                entity.LastModified = DateTime.UtcNow;
                var json = JsonConvert.SerializeObject(entity);
                var posts = await ListAll();
                var existingItem = posts.SingleOrDefault(x => x.RowKey == entity.RowKey);
                if (existingItem != null)
                {
                    var index = posts.IndexOf(existingItem);
                    if (index != -1)
                        posts[index] = entity;
                }
                posts.Sort((p1, p2) => p2.PubDate.CompareTo(p1.PubDate));
                memoryCache.Remove("posts");
                memoryCache.Set("posts", posts, cacheOptions);
                await File.WriteAllTextAsync(file, json);

                // Update Domain Events
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach (var domainEvent in events)
                    dispatcher.Dispatch(domainEvent);
            }
        }
    }
}
