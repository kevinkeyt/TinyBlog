using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TinyBlog.Domain;

namespace TinyBlog.Data
{
    public class DataContext: IDataContext
    {
        private readonly IHostingEnvironment environment;
        private readonly IMemoryCache memoryCache;
        private readonly MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(24));
        private readonly string folder;

        public DataContext(IHostingEnvironment environment, IMemoryCache memoryCache)
        {
            this.environment = environment;
            this.memoryCache = memoryCache;
            folder = environment.ContentRootPath + "\\Posts\\";
        }

        public List<Post> GetAllPosts()
        {
            List<Post> posts;
            if(!memoryCache.TryGetValue("posts", out posts))
            {
                posts = LoadPosts();
                
                // Save Data to Cache
                memoryCache.Set("posts", posts, cacheOptions);
            }      
            return posts;
        }

        public IEnumerable<Post> GetPublicPosts()
        {
            return GetAllPosts().Where(p => (p.IsPublished == true && p.PubDate <= DateTime.UtcNow));
        }

        public Post GetPostBySlug(string Slug)
        {
            return GetPublicPosts().SingleOrDefault(x => x.Slug == Slug);
        }

        public Post GetPostById(string Id)
        {
            return GetAllPosts().SingleOrDefault(x => x.Id == Id);
        }

        public void Save(Post post)
        {
            var file = Path.Combine(folder, post.Id + ".json");
            post.LastModified = DateTime.UtcNow;
            var json = JsonConvert.SerializeObject(post);
            var posts = GetAllPosts();
            if (!File.Exists(file))
            {
                posts.Insert(0, post);
                posts.Sort((p1, p2) => p2.PubDate.CompareTo(p1.PubDate));
            }
            else
            {
                var existingItem = posts.SingleOrDefault(x => x.Id == post.Id);
                if(existingItem != null)
                {
                    var index = posts.IndexOf(existingItem);
                    if (index != -1)
                        posts[index] = post;
                }
                posts.Sort((p1, p2) => p2.PubDate.CompareTo(p1.PubDate));
            }
            memoryCache.Remove("posts");
            memoryCache.Set("posts", posts, cacheOptions);
            File.WriteAllText(file, json);
        }

        public void Delete(Post post)
        {
            var file = Path.Combine(folder, post.Id + ".json");
            File.Delete(file);
        }

        public Dictionary<string, int> GetCategories()
        {
            return GetPublicPosts()
                .SelectMany(p => p.PostCategories)
                .GroupBy(c => c, (c, items) => new { Category = c, Count = items.Count() })
                .OrderBy(x => x.Category)
                .ToDictionary(x => x.Category, x => x.Count);
        }

        private List<Post> LoadPosts()
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var posts = new List<Post>();

            foreach(var file in Directory.EnumerateFiles(folder, "*.json", SearchOption.TopDirectoryOnly))
            {
                posts.Add(JsonConvert.DeserializeObject<Post>(File.ReadAllText(file)));
            }

            if(posts.Count > 0)
            {
                posts.Sort((p1, p2) => p2.PubDate.CompareTo(p1.PubDate));
            }

            return posts;
        }
    }
}
