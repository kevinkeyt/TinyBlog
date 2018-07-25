using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TinyBlog.Core.Interfaces;

namespace TinyBlog.Web.Pages.Account
{
    public class LogoutModel : BasePageModel
    {
        private readonly ILogger<LogoutModel> logger;

        public LogoutModel(ILogger<LogoutModel> logger, IBlogRepository blogRepository, IPostRepository postRepository) : base(blogRepository, postRepository)
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