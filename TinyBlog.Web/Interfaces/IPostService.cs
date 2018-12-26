using System.Collections.Generic;
using System.Threading.Tasks;
using TinyBlog.Web.ViewModels;

namespace TinyBlog.Web.Interfaces
{
    public interface IPostService
    {
        Task<Dictionary<string, int>> GetCategories();
        Dictionary<string, int> GetCategoriesNonAsync();
        Task<IEnumerable<PostViewModel>> GetPostsByCategory(string category);
        Task<IEnumerable<PostViewModel>> GetAll();
        Task<IEnumerable<PostViewModel>> GetPublicPosts();
        Task<PostViewModel> GetPostBySlug(string slug);
        Task<PostViewModel> GetById(string id, string partitionKey);
        Task Update(PostViewModel model);
        Task<PostViewModel> Add(PostViewModel model);
    }
}
