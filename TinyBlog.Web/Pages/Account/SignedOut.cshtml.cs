using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TinyBlog.Data;

namespace TinyBlog.Web.Pages.Account
{
    public class SignedOutModel : BasePageModel
    {
        private readonly ILogger<SignedOutModel> logger;
        private readonly IDataContext dataContext;

        public SignedOutModel(ILogger<SignedOutModel> logger, IDataContext dataContext)
        {
            this.logger = logger;
            this.dataContext = dataContext;
        }

        public IActionResult OnGet()
        {
            Blog = dataContext.GetBlogInfo();

            if (User.Identity.IsAuthenticated)
            {
                // Redirect to home page if the user is authenticated.
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}