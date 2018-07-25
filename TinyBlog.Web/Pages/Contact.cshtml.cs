using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TinyBlog.Core.Interfaces;

namespace TinyBlog.Web.Pages
{
    public class ContactModel : BasePageModel
    {
        private readonly ILogger<ContactModel> logger;

        public ContactModel(IBlogRepository blogRepository, IPostRepository postRepository, ILogger<ContactModel> logger) : base(blogRepository, postRepository)
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
