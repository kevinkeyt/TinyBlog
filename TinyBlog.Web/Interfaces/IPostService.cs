using System.Collections.Generic;
using TinyBlog.Web.ViewModels;

namespace TinyBlog.Web.Interfaces
{
    public interface IPostService
    {
        Dictionary<string, int> GetCategories();
        IEnumerable<PostViewModel> GetPostsByCategory(string category);
        IEnumerable<PostViewModel> GetAll();
        IEnumerable<PostViewModel> GetPublicPosts();
        PostViewModel GetPostBySlug(string slug, bool IsAdmin);
        PostViewModel GetById(string id);
        void Update(PostViewModel model);
        PostViewModel Add(PostViewModel model);
    }
}
