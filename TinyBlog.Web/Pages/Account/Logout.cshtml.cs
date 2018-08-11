using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TinyBlog.Web.Interfaces;

namespace TinyBlog.Web.Pages.Account
{
    public class LogoutModel : BasePageModel
    {
        private readonly ILogger<LogoutModel> logger;

        public LogoutModel(IBlogService blogService, IPostService postService, ILogger<LogoutModel> logger) : base(blogService, postService)
        {
            this.logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            logger.LogInformation($"User {User.Identity.Name} logged out at {DateTime.UtcNow}.");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/SignedOut");
        }
    }
}