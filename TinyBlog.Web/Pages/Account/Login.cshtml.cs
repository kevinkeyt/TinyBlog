using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TinyBlog.Data;
using TinyBlog.Domain;

namespace TinyBlog.Web.Pages.Account
{
    public class LoginModel : BasePageModel
    {
        private readonly ILogger<LoginModel> logger;
        private readonly IConfiguration configuration;
        private readonly IDataContext dataContext;

        public LoginModel(ILogger<LoginModel> logger, IConfiguration configuration, IDataContext dataContext)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.dataContext = dataContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Blog = dataContext.GetBlogInfo();
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                var user = await AuthenticateUser();
                if(user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login");
                    logger.LogInformation($"Invalid Login For User {user.Email} logged in at {DateTime.UtcNow}.");
                    return Page();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("FullName", user.FullName),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. Required when setting the 
                    // ExpireTimeSpan option of CookieAuthenticationOptions 
                    // set with AddCookie. Also required when setting 
                    // ExpiresUtc.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                logger.LogInformation($"User {user.Email} logged in at {DateTime.UtcNow}");
            
                return LocalRedirect(Url.GetLocalUrl(returnUrl));
            }
            return Page();
        }

        private async Task<ApplicationUser> AuthenticateUser()
        {
            if(Input.Email == configuration["AppSettings:LoginEmail"] && Input.Password == configuration["AppSettings:LoginPassword"])
            {
                return await Task.Run(() => new ApplicationUser()
                {
                    Email = Input.Email,
                    FullName = configuration["AppSettings:FullName"]
                });
            }
            return null;
        }
    }
}