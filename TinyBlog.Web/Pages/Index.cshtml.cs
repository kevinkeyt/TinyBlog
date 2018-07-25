using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TinyBlog.Core.Interfaces;
using TinyBlog.Web.ViewModels;

namespace TinyBlog.Web.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly ILogger<IndexModel> logger;

        public IndexModel(IBlogRepository blogRepository, IPostRepository postRepository, ILogger<IndexModel> logger) : base(blogRepository, postRepository)
        {
            this.logger = logger;
        }

        public IEnumerable<PostViewModel> Posts { get; set; }

        public IActionResult OnGetAsync(string c)
        {
            if (!string.IsNullOrEmpty(c))
                Posts = postRepository.GetPostsByCategory(c)
                    .Select(x => PostViewModel.FromPostEntity(x));
            else
                Posts = (HttpContext.User.Identity.IsAuthenticated) ? postRepository.ListAll()
                    .Select(x => PostViewModel.FromPostEntity(x)) : postRepository.GetPublicPosts()
                    .Select(x => PostViewModel.FromPostEntity(x));
            
            return Page();
        }
    }
}
