using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TinyBlog.Data;

namespace TinyBlog.Web.Pages.Account
{
    public class SignedOutModel : BasePageModel
    {
        private readonly ILogger<SignedOutModel> logger;

        public SignedOutModel(ILogger<SignedOutModel> logger, IDataContext dataContext) : base(dataContext)
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