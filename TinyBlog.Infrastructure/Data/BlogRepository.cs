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
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var file = Path.Combine(folder, "BlogInfo.json");
            Blog blogInfo = InitBlogInfo();
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

        private Blog InitBlogInfo()
        {
            return new Blog()
            {
                Name = "Tiny Blog",
                Title = "Tiny Blog Engine",
                Description = "A simple blogging engine for small to medium size blogs.",
                ShareUrl = "https://www.myblog.com",
                ShareImageUrl = "https://www.myblog.com",
                AboutTitle = "About Joe Tester",
                AboutDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. ",
                ContactTitle = "Contact Me",
                ContactDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. ",
                Twitter = "https://twitter.com/joe",
                TwitterName = "@joetester",
                Facebook = "https://facebook.com/joe",
                GitHub = "https://github.com/joe",
                LinkedIn = "https://linkedin.com/joe"
            };
        }
    }
}
