using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TinyBlog.Web.Pages
{
    public class AboutModel : PageModel
    {
        public string Message { get; set; }

        public IActionResult OnGetAsync()
        {
            Message = "Your application description page.";
            return Page();
        }
    }
}
