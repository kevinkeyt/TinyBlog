using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using TinyBlog.Data;
using TinyBlog.Domain;

namespace TinyBlog.Web.Pages.Admin
{
    public class BlogModel : BasePageModel
    {
        [BindProperty]
        public Blog BlogInfo { get; set; }

        private readonly IDataContext dataContext;
        private readonly ILogger<BlogModel> logger;

        public BlogModel(IDataContext dataContext, ILogger<BlogModel> logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public IActionResult OnGet()
        {
            Blog = dataContext.GetBlogInfo();
            BlogInfo = Blog;

            return Page();
        }

        public IActionResult OnPost()
        {
            Blog = BlogInfo;
            if (ModelState.IsValid)
            {
                dataContext.SaveBlogInfo(BlogInfo);
                logger.LogInformation($"Blog info was updated on {DateTime.UtcNow}");
            }
            return Page();
        }
    }
}