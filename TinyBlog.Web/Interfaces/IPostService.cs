using System.Collections.Generic;
using System.Threading.Tasks;
using TinyBlog.Web.ViewModels;

namespace TinyBlog.Web.Interfaces
{
    public interface IPostService
    {
        Task<Dictionary<string, int>> GetCategories();
        Task<IEnumerable<PostViewModel>> GetPostsByCategory(string category);
        Task<IEnumerable<PostViewModel>> GetAll();
        Task<IEnumerable<PostViewModel>> GetPublicPosts();
        PostViewModel GetPostBySlug(string slug, bool IsAdmin);
        PostViewModel GetById(string id);
        void Update(PostViewModel model);
        PostViewModel Add(PostViewModel model);
    }
}
