using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TinyBlog.Data;

namespace TinyBlog.Web.Pages
{
    public class ContactModel : BasePageModel
    {
        private readonly ILogger<ContactModel> logger;

        public ContactModel(IDataContext dataContext, ILogger<ContactModel> logger) : base(dataContext)
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
