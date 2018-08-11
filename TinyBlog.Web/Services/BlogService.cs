using AutoMapper;
using TinyBlog.Core.Entities;
using TinyBlog.Core.Interfaces;
using TinyBlog.Web.Interfaces;
using TinyBlog.Web.ViewModels;

namespace TinyBlog.Web.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository blogRepository;
        private readonly IMapper mapper;

        public BlogService(IBlogRepository blogRepository, IMapper mapper)
        {
            this.blogRepository = blogRepository;
            this.mapper = mapper;
        }

        public BlogViewModel GetBlogInfo()
        {
            return mapper.Map<BlogViewModel>(blogRepository.GetBlogInfo());
        }

        public void SaveBlogInfo(BlogViewModel model)
        {
            blogRepository.SaveBlogInfo(mapper.Map<Blog>(model));
        }
    }
}
