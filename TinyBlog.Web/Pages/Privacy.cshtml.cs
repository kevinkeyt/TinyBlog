using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TinyBlog.Web.Interfaces;

namespace TinyBlog.Web.Pages
{
    public class PrivacyModel : BasePageModel
    {
        private readonly ILogger<PrivacyModel> logger;

        public PrivacyModel(IBlogService blogService, IPostService postService, ILogger<PrivacyModel> logger) : base(blogService, postService)
        {
            this.logger = logger;
        }

        public IActionResult OnGetAsync()
        {
            return Page();
        }
    }
}