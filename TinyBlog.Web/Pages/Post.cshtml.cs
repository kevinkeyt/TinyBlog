using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TinyBlog.Core.Interfaces;
using TinyBlog.Web.ViewModels;

namespace TinyBlog.Web.Pages
{
    public class PostModel : BasePageModel
    {
        private readonly ILogger<PostModel> logger;
        public PostModel(IBlogRepository blogRepository, IPostRepository postRepository, ILogger<PostModel> logger, IMapper mapper) : base(blogRepository, postRepository, mapper)
        {
            this.logger = logger;
        }

        public string Slug { get; set; }
        public PostViewModel Post { get; set; }

        public IActionResult OnGetAsync(string slug)
        {
            Slug = slug;
            Post = mapper.Map<PostViewModel>(postRepository.GetPostBySlug(slug, HttpContext.User.Identity.IsAuthenticated));
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