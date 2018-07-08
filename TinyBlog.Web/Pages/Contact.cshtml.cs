using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TinyBlog.Web.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; set; }

        public IActionResult OnGetAsync()
        {
            Message = "Your contact page.";
            return Page();
        }
    }
}
