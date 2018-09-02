using System.Collections.Generic;
using TinyBlog.Core.Entities;

namespace TinyBlog.Core.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetPublicPosts();
        IEnumerable<Post> GetPostsByCategory(string category);
        List<Post> ListAll();
        Post GetPostBySlug(string slug, bool authenticated = false);
        Dictionary<string, int> GetCategories();
        Post Add(Post post);
        void Update(Post post);
        Post GetById(string Id);
    }
}
