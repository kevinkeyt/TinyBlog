using System.Collections.Generic;
using TinyBlog.Core.Entities;

namespace TinyBlog.Core.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetPublicPosts();
        IEnumerable<Post> GetPostsByCategory(string category);
        Post GetPostBySlug(string Slug);
        Dictionary<string, int> GetCategories();
    }
}
