using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TinyBlog.Data;
using TinyBlog.Domain;

namespace TinyBlog.Web.Pages
{
    public class PostModel : BasePageModel
    {
        private readonly ILogger<PostModel> logger;

        public PostModel(IDataContext dataContext, ILogger<PostModel> logger) : base(dataContext)
        {
            this.logger = logger;
        }

        public string Slug { get; set; }
        public Post Post { get; set; }

        public IActionResult OnGetAsync(string slug)
        {
            Slug = slug;
            Post = dataContext.GetPostBySlug(slug);
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