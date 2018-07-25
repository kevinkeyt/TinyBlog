using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using TinyBlog.Core.Interfaces;
using TinyBlog.Web.ViewModels;

namespace TinyBlog.Web.Pages.Admin
{
    public class BlogModel : BasePageModel
    {
        [BindProperty]
        public BlogViewModel BlogInfo { get; set; }

        private readonly ILogger<BlogModel> logger;
        public BlogModel(IBlogRepository blogRepository, IPostRepository postRepository, ILogger<BlogModel> logger) : base(blogRepository, postRepository)
        {
            this.logger = logger;
        }

        public IActionResult OnGet()
        {
            BlogInfo = Blog;
            return Page();
        }

        public IActionResult OnPost()
        {
            Blog = BlogInfo;
            if (ModelState.IsValid)
            {
                blogRepository.SaveBlogInfo(BlogViewModel.ToBlogEntity(BlogInfo));
                logger.LogInformation($"Blog info was updated on {DateTime.UtcNow}");
            }
            return Page();
        }
    }
}