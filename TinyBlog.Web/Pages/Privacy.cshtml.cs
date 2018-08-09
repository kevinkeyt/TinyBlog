using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TinyBlog.Core.Interfaces;

namespace TinyBlog.Web.Pages
{
    public class PrivacyModel : BasePageModel
    {
        private readonly ILogger<PrivacyModel> logger;

        public PrivacyModel(IBlogRepository blogRepository, IPostRepository postRepository, ILogger<PrivacyModel> logger, IMapper mapper) : base(blogRepository, postRepository, mapper)
        {
            this.logger = logger;
        }

        public IActionResult OnGetAsync()
        {
            return Page();
        }
    }
}