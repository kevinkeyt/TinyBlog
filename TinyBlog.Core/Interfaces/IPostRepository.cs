using System.Collections.Generic;
using System.Threading.Tasks;
using TinyBlog.Core.Entities;

namespace TinyBlog.Core.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPublicPosts();
        Task<IEnumerable<Post>> GetPostsByCategory(string category);
        Task<List<Post>> ListAll();
        Task<Post> GetPostBySlug(string slug, bool authenticated = false);
        Task<Dictionary<string, int>> GetCategories();
        Task<Post> Add(Post post);
        Task Update(Post post);
        Task<Post> GetById(string Id);
    }
}
