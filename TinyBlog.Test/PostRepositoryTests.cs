using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBlog.Core.Entities;
using TinyBlog.Core.Interfaces;
using Xunit;

namespace TinyBlog.Test
{
    public class PostRepositoryTests
    {
        private readonly Mock<IPostRepository> moqRepo;

        public PostRepositoryTests()
        {
            moqRepo = new Mock<IPostRepository>();
        }

        [Fact]
        public void Get_PublicPosts_ReturnsList()
        {
            moqRepo.Setup(x => x.GetPublicPosts()).Returns(new List<Post>
            {
                new Post() { Id = Guid.NewGuid().ToString() },
                new Post() { Id = Guid.NewGuid().ToString() }
            });
            var posts = moqRepo.Object.GetPublicPosts();
            Assert.True(posts.Count() > 0);
        }
    }
}
