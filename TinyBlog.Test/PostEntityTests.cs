using System;
using System.Collections.Generic;
using System.Text;
using TinyBlog.Core.Entities;
using Xunit;

namespace TinyBlog.Test
{
    public class PostEntityTests
    {
        private static Post CreatePost()
        {
            return new Post
            {
                Title = "Test Post",
                Author = "Joe Tester",
                Slug = "test-post"
            };
        }

        [Fact]
        public void NewPost_HasId()
        {
            var post = CreatePost();
            Assert.NotEqual(string.Empty, post.RowKey);
        }

        [Fact]
        public void NewPost_HasRequiredValues_Set()
        {
            var post = CreatePost();
            Assert.NotEqual(string.Empty, post.Title);
            Assert.NotEqual(string.Empty, post.Author);
            Assert.NotEqual(string.Empty, post.Slug);
            Assert.NotEqual(DateTime.MinValue, post.LastModified);
        }

        [Fact]
        public void Post_Categories_AreNotNull()
        {
            var post = CreatePost();
            Assert.NotNull(post.PostCategories);
        }

        [Fact]
        public void Post_SetPubDate_PublishesPost()
        {
            var post = CreatePost();
            post.SetPubDate(DateTime.UtcNow.AddDays(-1));
            Assert.True(post.IsPublished);
        }

        [Fact]
        public void Post_SetPubDate_UnPublishesPost()
        {
            var post = CreatePost();
            post.SetPubDate(DateTime.UtcNow.AddDays(1));
            Assert.False(post.IsPublished);
        }

        [Fact]
        public void Post_AddCategory_IsSuccessful()
        {
            var post = CreatePost();
            var (success, msg) = post.AddCategory("Test");
            Assert.True(success);
            Assert.True(post.PostCategories.Count == 1);
        }

        [Fact]
        public void Post_RemoveCategory_IsSuccessful()
        {
            var post = CreatePost();
            post.AddCategory("Test");
            var (success, msg) = post.RemoveCategory("Test");
            Assert.True(success);
            Assert.True(post.PostCategories.Count == 0);
        }
    }
}
