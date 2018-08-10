using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TinyBlog.Core.Entities;
using TinyBlog.Core.Interfaces;

namespace TinyBlog.Data.Repos
{
    public class PostRepository : IPostRepository
    {
        private readonly IHostingEnvironment environment;
        private readonly IMemoryCache memoryCache;
        private readonly MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(24));
        private readonly string folder;

        public PostRepository(IHostingEnvironment environment, IMemoryCache memoryCache)
        {
            this.environment = environment;
            this.memoryCache = memoryCache;
            folder = environment.ContentRootPath + "\\Data\\Posts\\";
        }

        public Post Add(Post entity)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var file = Path.Combine(folder, entity.Id + ".json");
            if (!File.Exists(file))
            {
                entity.LastModified = DateTime.UtcNow;
                var json = JsonConvert.SerializeObject(entity);
                var posts = ListAll();
                posts.Insert(0, entity);
                posts.Sort((p1, p2) => p2.PubDate.CompareTo(p1.PubDate));
                memoryCache.Remove("posts");
                memoryCache.Set("posts", posts, cacheOptions);
                File.WriteAllText(file, json);
            }
            return entity;
        }

        public void Delete(Post entity)
        {
            var file = Path.Combine(folder, entity.Id + ".json");
            File.Delete(file);
            memoryCache.Remove("posts");
            ListAll();
        }

        public Post GetById(string id)
        {
            return ListAll().SingleOrDefault(x => x.Id == id);
        }

        public Dictionary<string, int> GetCategories()
        {
            return GetPublicPosts()
                .SelectMany(p => p.PostCategories)
                .GroupBy(c => c, (c, items) => new { Category = c, Count = items.Count() })
                .OrderBy(x => x.Category)
                .ToDictionary(x => x.Category, x => x.Count);
        }

        public Post GetPostBySlug(string slug, bool authenticated = false)
        {
            if (authenticated)
                return ListAll().SingleOrDefault(x => x.Slug == slug);
            else
                return GetPublicPosts().SingleOrDefault(x => x.Slug == slug);
        }

        public IEnumerable<Post> GetPostsByCategory(string category)
        {
            return GetPublicPosts().Where(x => x.PostCategories.Contains(category));
        }

        public IEnumerable<Post> GetPublicPosts()
        {
            return ListAll().Where(p => (p.IsPublished == true && p.PubDate <= DateTime.UtcNow));
        }

        public List<Post> ListAll()
        {
            List<Post> posts;
            if (!memoryCache.TryGetValue("posts", out posts))
            {
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                posts = new List<Post>();

                foreach (var file in Directory.EnumerateFiles(folder, "*.json", SearchOption.TopDirectoryOnly))
                {
                    posts.Add(JsonConvert.DeserializeObject<Post>(File.ReadAllText(file)));
                }

                if (posts.Count > 0)
                {
                    posts.Sort((p1, p2) => p2.PubDate.CompareTo(p1.PubDate));
                }
                memoryCache.Set("posts", posts, cacheOptions);
            }
            return posts;
        }

        public void Update(Post entity)
        {
            var file = Path.Combine(folder, entity.Id + ".json");
            if (File.Exists(file))
            {
                entity.LastModified = DateTime.UtcNow;
                var json = JsonConvert.SerializeObject(entity);
                var posts = ListAll();
                var existingItem = posts.SingleOrDefault(x => x.Id == entity.Id);
                if (existingItem != null)
                {
                    var index = posts.IndexOf(existingItem);
                    if (index != -1)
                        posts[index] = entity;
                }
                posts.Sort((p1, p2) => p2.PubDate.CompareTo(p1.PubDate));
                memoryCache.Remove("posts");
                memoryCache.Set("posts", posts, cacheOptions);
                File.WriteAllText(file, json);
            }
        }
    }
}
