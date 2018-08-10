using Moq;
using TinyBlog.Core.Entities;
using TinyBlog.Core.Interfaces;
using Xunit;

namespace TinyBlog.Test
{
    public class BlogRepositoryTests
    {
        private readonly Mock<IBlogRepository> moqRepo;

        public BlogRepositoryTests()
        {
            moqRepo = new Mock<IBlogRepository>();
        }

        [Fact]
        public void Get_BlogInfo_Is_Typeof_Blog()
        {
            moqRepo.Setup(x => x.GetBlogInfo()).Returns(new Blog());
            Assert.IsType<Blog>(moqRepo.Object.GetBlogInfo());
        }

        [Fact]
        public void Save_Accepts_Typeof_Blog()
        {
            moqRepo.Setup(x => x.SaveBlogInfo(new Blog()));

            var blog = new Blog();
            moqRepo.Object.SaveBlogInfo(blog);
            Assert.IsType<Blog>(blog);
        }
    }
}
