using System.Collections.Generic;
using TinyBlog.Core.Entities;

namespace TinyBlog.Core.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetPublicPosts();
        IEnumerable<Post> GetPostsByCategory(string category);
        Post GetPostBySlug(string slug, bool authenticated = false);
        Dictionary<string, int> GetCategories();

    }
}
