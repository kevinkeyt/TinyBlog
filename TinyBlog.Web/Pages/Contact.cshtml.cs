using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TinyBlog.Web.Interfaces;

namespace TinyBlog.Web.Pages
{
    public class ContactModel : BasePageModel
    {
        private readonly ILogger<ContactModel> logger;

        public ContactModel(IBlogService blogService, IPostService postService, ILogger<ContactModel> logger) : base(blogService, postService)
        {
            this.logger = logger;
        }

        public string Message { get; set; }

        public IActionResult OnGetAsync()
        {
            Message = "Your contact page.";
            return Page();
        }
    }
}
