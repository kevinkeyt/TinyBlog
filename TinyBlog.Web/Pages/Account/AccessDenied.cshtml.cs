using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TinyBlog.Web.Pages.Account
{
    public class AccessDeniedModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}