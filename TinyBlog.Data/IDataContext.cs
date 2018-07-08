using System;
using System.Collections.Generic;
using System.Text;
using TinyBlog.Domain;

namespace TinyBlog.Data
{
    public interface IDataContext
    {
        List<Post> GetAllPosts();
        IEnumerable<Post> GetPublicPosts();
        Post GetPostBySlug(string Slug);
        Post GetPostById(string Id);
        void Save(Post post);
        void Delete(Post post);
        Dictionary<string, int> GetCategories();
    }
}
