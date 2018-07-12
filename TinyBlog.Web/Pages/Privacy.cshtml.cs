using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TinyBlog.Data;

namespace TinyBlog.Web.Pages
{
    public class PrivacyModel : BasePageModel
    {
        private readonly ILogger<PrivacyModel> logger;

        public PrivacyModel(IDataContext dataContext, ILogger<PrivacyModel> logger) : base(dataContext)
        {
            this.logger = logger;
        }

        public IActionResult OnGetAsync()
        {
            return Page();
        }
    }
}