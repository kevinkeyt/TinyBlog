using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TinyBlog.Web.Interfaces;

namespace TinyBlog.Web.Pages
{
    public class AboutModel : BasePageModel
    {
        private readonly ILogger<AboutModel> logger;

        public AboutModel(IBlogService blogService, IPostService postService, ILogger<AboutModel> logger) : base(blogService, postService)
        {
            this.logger = logger;
        }

        public string Message { get; set; }

        public IActionResult OnGetAsync()
        {
            Message = "Your application description page.";
            return Page();
        }
    }
}
