using System.Threading.Tasks;
using TinyBlog.Core.Entities;

namespace TinyBlog.Core.Interfaces
{
    public interface IBlogRepository
    {
        Task<Blog> GetBlogInfo();
        Task SaveBlogInfo(Blog blog);
    }
}
