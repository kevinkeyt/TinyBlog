using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TinyBlog.Web.Interfaces;
using TinyBlog.Web.ViewModels;

namespace TinyBlog.Web.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly ILogger<IndexModel> logger;
        public IndexModel(IBlogService blogService, IPostService postService, ILogger<IndexModel> logger) : base(blogService, postService)
        {
            this.logger = logger;
        }

        public IEnumerable<PostViewModel> Posts { get; set; }

        public IActionResult OnGetAsync(string c)
        {
            if (!string.IsNullOrEmpty(c))
                Posts = postService.GetPostsByCategory(c);
            else
                Posts = (HttpContext.User.Identity.IsAuthenticated) ? postService.GetAll() : postService.GetPublicPosts();
            
            return Page();
        }
    }
}
