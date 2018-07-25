using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TinyBlog.Core.Interfaces;

namespace TinyBlog.Web.Pages
{
    public class AboutModel : BasePageModel
    {
        private readonly ILogger<AboutModel> logger;

        public AboutModel(IBlogRepository blogRepository, IPostRepository postRepository, ILogger<AboutModel> logger) : base(blogRepository, postRepository)
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
