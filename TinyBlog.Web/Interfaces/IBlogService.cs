using System.Threading.Tasks;
using TinyBlog.Web.ViewModels;

namespace TinyBlog.Web.Interfaces
{
    public interface IBlogService
    {
        Task<BlogViewModel> GetBlogInfo();
        BlogViewModel GetBlogInfoNonAsync();
        Task SaveBlogInfo(BlogViewModel model);
    }
}
