using AutoMapper;
using System.Threading.Tasks;
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

        public async Task<BlogViewModel> GetBlogInfo()
        {
            var blogInfo = await blogRepository.GetBlogInfo();
            return mapper.Map<BlogViewModel>(blogInfo);
        }

        public BlogViewModel GetBlogInfoNonAsync()
        {
            Task<BlogViewModel> task = Task.Run<BlogViewModel>(async () => await GetBlogInfo());
            return task.Result;
        }

        public async Task SaveBlogInfo(BlogViewModel model)
        {
            await blogRepository.SaveBlogInfo(mapper.Map<Blog>(model));
        }
    }
}
