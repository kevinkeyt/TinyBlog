using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.IO;
using TinyBlog.Core.Entities;
using TinyBlog.Core.Interfaces;

namespace TinyBlog.Infrastructure.Data
{
    public class BlogRepository : IBlogRepository
    {

        private readonly IHostingEnvironment environment;
        private readonly IMemoryCache memoryCache;
        private readonly MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(24));
        private readonly string folder;

        public BlogRepository(IHostingEnvironment environment, IMemoryCache memoryCache)
        {
            this.environment = environment;
            this.memoryCache = memoryCache;
            folder = environment.ContentRootPath + "\\Data\\";
        }

        public Blog GetBlogInfo()
        {
            var file = Path.Combine(folder, "BlogInfo.json");
            Blog blogInfo = new Blog();
            if (!File.Exists(file))
            {
                var json = JsonConvert.SerializeObject(blogInfo);
                File.WriteAllText(file, json);
            }

            if (!memoryCache.TryGetValue("bloginfo", out blogInfo))
            {
                blogInfo = JsonConvert.DeserializeObject<Blog>(File.ReadAllText(file));
                memoryCache.Set("bloginfo", blogInfo, cacheOptions);
            }
            return blogInfo;
        }

        public void SaveBlogInfo(Blog blog)
        {
            var json = JsonConvert.SerializeObject(blog);
            var file = Path.Combine(folder, "BlogInfo.json");
            memoryCache.Remove("bloginfo");
            memoryCache.Set("bloginfo", blog, cacheOptions);
            File.WriteAllText(file, json);
        }
    }
}
