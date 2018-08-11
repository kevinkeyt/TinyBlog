using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TinyBlog.Web.Interfaces;
using TinyBlog.Web.ViewModels;

namespace TinyBlog.Web.Pages
{
    public class PostModel : BasePageModel
    {
        private readonly ILogger<PostModel> logger;
        public PostModel(IBlogService blogService, IPostService postService, ILogger<PostModel> logger) : base(blogService, postService)
        {
            this.logger = logger;
        }

        public string Slug { get; set; }
        public PostViewModel Post { get; set; }

        public IActionResult OnGetAsync(string slug)
        {
            Slug = slug;
            Post = postService.GetPostBySlug(slug, HttpContext.User.Identity.IsAuthenticated);
            // If post is null it should redirect to 404
            if(Post == null)
            {
                logger.LogInformation($"Could not find post for slug {slug}.");
                return NotFound();
            }
            return Page();
        }
    }
}