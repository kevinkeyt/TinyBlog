using TinyBlog.Web.ViewModels;

namespace TinyBlog.Web.Interfaces
{
    public interface IBlogService
    {
        BlogViewModel GetBlogInfo();
        void SaveBlogInfo(BlogViewModel model);
    }
}
