using System;
using System.Threading.Tasks;
using TinyBlog.Core.Entities;
using TinyBlog.Core.Interfaces;

namespace TinyBlog.Infrastructure.Repos
{
    public class AzureTableBlogRepository : IBlogRepository
    {
        private readonly IAzureTableStorage<Blog> azureTableStorage;

        public AzureTableBlogRepository(IAzureTableStorage<Blog> azureTableStorage)
        {
            this.azureTableStorage = azureTableStorage;
        }

        public async Task<Blog> GetBlogInfo()
        {
            var blog = await azureTableStorage.GetItem("blogs", "primary");
            if (blog != null)
                return blog;
            blog = InitBlogInfo();
            await SaveBlogInfo(blog);
            return blog;
        }

        public async Task SaveBlogInfo(Blog blog)
        {
            blog.RowKey = "primary";
            blog.PartitionKey = "blogs";
            var exists = await azureTableStorage.GetItem("blogs", "primary");
            if(exists != null)
            {
                await azureTableStorage.Update(blog);
            }
            else
            {

                await azureTableStorage.Insert(blog);
            }
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
