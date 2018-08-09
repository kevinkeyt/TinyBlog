using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using TinyBlog.Core.Interfaces;
using TinyBlog.Web.ViewModels;

namespace TinyBlog.Web.Pages
{
    public class BasePageModel : PageModel
    {
        public BlogViewModel Blog { get; set; }

        public Dictionary<string, int> Categories { get; set; }

        internal readonly IBlogRepository blogRepository;
        internal readonly IPostRepository postRepository;
        internal readonly IMapper mapper;
        public BasePageModel(IBlogRepository blogRepository, IPostRepository postRepository, IMapper mapper)
        {
            this.blogRepository = blogRepository;
            this.postRepository = postRepository;
            this.mapper = mapper;
            Blog = mapper.Map<BlogViewModel>(blogRepository.GetBlogInfo());
            Categories = postRepository.GetCategories();
        }
    }
}
