using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TinyBlog.Data;

namespace TinyBlog.Web.Pages.Account
{
    public class LogoutModel : BasePageModel
    {
        private readonly ILogger<LogoutModel> logger;
        private readonly IDataContext dataContext;

        public LogoutModel(ILogger<LogoutModel> logger, IDataContext dataContext)
        {
            this.logger = logger;
            this.dataContext = dataContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            logger.LogInformation($"User {User.Identity.Name} logged out at {DateTime.UtcNow}.");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Blog = dataContext.GetBlogInfo();
            return Redirect("/Account/SignedOut");
        }
    }
}