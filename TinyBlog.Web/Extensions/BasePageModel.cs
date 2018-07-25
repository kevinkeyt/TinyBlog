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

        public BasePageModel(IBlogRepository blogRepository, IPostRepository postRepository)
        {
            this.blogRepository = blogRepository;
            this.postRepository = postRepository;
            Blog = BlogViewModel.FromBlogEntity(blogRepository.GetBlogInfo());
            Categories = postRepository.GetCategories();
        }
    }
}
