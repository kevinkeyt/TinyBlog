using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TinyBlog.Web.Interfaces;
using TinyBlog.Web.ViewModels;

namespace TinyBlog.Web.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly ILogger<IndexModel> logger;
        public IndexModel(IBlogService blogService, IPostService postService, ILogger<IndexModel> logger) : base(blogService, postService)
        {
            this.logger = logger;
        }

        public IEnumerable<PostViewModel> Posts { get; set; }

        public async Task<IActionResult> OnGetAsync(string c)
        {
            if (!string.IsNullOrEmpty(c))
                Posts = await postService.GetPostsByCategory(c);
            else
                Posts = (HttpContext.User.Identity.IsAuthenticated) ? await postService.GetAll() : await postService.GetPublicPosts();
            
            return Page();
        }
    }
}
