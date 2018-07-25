using TinyBlog.Core.Entities;

namespace TinyBlog.Core.Interfaces
{
    public interface IBlogRepository
    {
        Blog GetBlogInfo();
        void SaveBlogInfo(Blog blog);
    }
}
