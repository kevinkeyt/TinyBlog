using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TinyBlog.Web.Interfaces;

namespace TinyBlog.Web.Pages.Account
{
    public class AccessDeniedModel : BasePageModel
    {
        private readonly ILogger<AccessDeniedModel> logger;

        public AccessDeniedModel(IBlogService blogService, IPostService postService, ILogger<AccessDeniedModel> logger) : base(blogService, postService)
        {
            this.logger = logger;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}