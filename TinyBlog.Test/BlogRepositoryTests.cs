using Moq;
using System.Threading.Tasks;
using TinyBlog.Core.Entities;
using TinyBlog.Core.Interfaces;
using Xunit;

namespace TinyBlog.Test
{
    public class BlogRepositoryTests
    {
        private readonly Mock<IBlogRepository> moqRepo;
        private static async Task<Blog> CreateBlogItem()
        {
            return await Task.Run(() => new Blog()
            {
                Title = "Tiny Blog Engine",
                Name = "Tiny Blog"
            });
        }
        public BlogRepositoryTests()
        {
            moqRepo = new Mock<IBlogRepository>();
        }

        [Fact]
        public async Task Get_BlogInfo_Is_Typeof_Blog()
        {
            moqRepo.Setup(x => x.GetBlogInfo()).Returns(CreateBlogItem());
            Assert.IsType<Blog>(await moqRepo.Object.GetBlogInfo());
        }

        [Fact]
        public async Task Save_Accepts_Typeof_Blog()
        {
            var blog = await CreateBlogItem();
            await moqRepo.Object.SaveBlogInfo(blog);
            Assert.IsType<Blog>(blog);
        }
    }
}
