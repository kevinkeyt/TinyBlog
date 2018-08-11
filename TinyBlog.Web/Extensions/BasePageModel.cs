using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using TinyBlog.Web.Interfaces;
using TinyBlog.Web.ViewModels;

namespace TinyBlog.Web.Pages
{
    public class BasePageModel : PageModel
    {
        public BlogViewModel Blog { get; set; }
        public Dictionary<string, int> Categories { get; set; }

        internal readonly IBlogService blogService;
        internal readonly IPostService postService;
        public BasePageModel(IBlogService blogService, IPostService postService)
        {
            this.blogService = blogService;
            this.postService = postService;
            Blog = blogService.GetBlogInfo();
            Categories = postService.GetCategories();
        }
    }
}
