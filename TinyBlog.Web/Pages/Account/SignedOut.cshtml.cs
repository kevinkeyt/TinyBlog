using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TinyBlog.Web.Interfaces;

namespace TinyBlog.Web.Pages.Account
{
    public class SignedOutModel : BasePageModel
    {
        private readonly ILogger<SignedOutModel> logger;

        public SignedOutModel(IBlogService blogService, IPostService postService, ILogger<SignedOutModel> logger) : base(blogService, postService)
        {
            this.logger = logger;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Redirect to home page if the user is authenticated.
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}