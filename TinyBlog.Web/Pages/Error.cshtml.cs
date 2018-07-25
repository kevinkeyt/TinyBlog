using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TinyBlog.Core.Interfaces;

namespace TinyBlog.Web.Pages
{
    public class ErrorModel : BasePageModel
    {
        private readonly ILogger<ErrorModel> logger;

        public ErrorModel(IBlogRepository blogRepository, IPostRepository postRepository, ILogger<ErrorModel> logger) : base(blogRepository, postRepository)
        {
            this.logger = logger;
        }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult OnGetAsync()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return Page();
        }
    }
}
