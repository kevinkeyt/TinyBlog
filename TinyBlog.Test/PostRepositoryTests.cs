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

        private static Post CreatePost()
        {
            return new Post()
            {
               Author = "Kevin Keyt",
               Title = "Test Post",
               Slug = "test-post"
            };
        }

        public PostRepositoryTests()
        {
            moqRepo = new Mock<IPostRepository>();
        }

        //[Fact]
        //public void Get_PublicPosts_ReturnsList()
        //{
        //    async moqRepo.(x => x.GetPublicPosts()).Returns(new List<Post>
        //    {
        //        CreatePost()
        //    });
        //    var posts = moqRepo.Object.GetPublicPosts();
        //    Assert.True(posts.Count() > 0);
        //}
    }
}
