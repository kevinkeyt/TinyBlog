using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TinyBlog.Data;

namespace TinyBlog.Web.Pages.Account
{
    public class AccessDeniedModel : BasePageModel
    {
        private readonly IDataContext dataContext;
        private readonly ILogger<AccessDeniedModel> logger;

        public AccessDeniedModel(IDataContext dataContext, ILogger<AccessDeniedModel> logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
        }
        public IActionResult OnGet()
        {
            Blog = dataContext.GetBlogInfo();
            return Page();
        }
    }
}