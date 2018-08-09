using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TinyBlog.Core.Interfaces;

namespace TinyBlog.Web.Pages.Account
{
    public class AccessDeniedModel : BasePageModel
    {
        private readonly ILogger<AccessDeniedModel> logger;

        public AccessDeniedModel(IBlogRepository blogRepository, IPostRepository postRepository, IMapper mapper, ILogger<AccessDeniedModel> logger) : base(blogRepository, postRepository, mapper)
        {
            this.logger = logger;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}