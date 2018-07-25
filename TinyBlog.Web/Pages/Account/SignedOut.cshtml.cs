using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TinyBlog.Core.Interfaces;

namespace TinyBlog.Web.Pages.Account
{
    public class SignedOutModel : BasePageModel
    {
        private readonly ILogger<SignedOutModel> logger;

        public SignedOutModel(ILogger<SignedOutModel> logger, IBlogRepository blogRepository, IPostRepository postRepository) : base(blogRepository, postRepository)
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