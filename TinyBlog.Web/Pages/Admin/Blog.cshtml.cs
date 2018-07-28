using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using TinyBlog.Core.Interfaces;
using TinyBlog.Web.ViewModels;

namespace TinyBlog.Web.Pages.Admin
{
    public class BlogModel : BasePageModel
    {
        [BindProperty]
        public BlogViewModel BlogInfo { get; set; }

        private readonly ILogger<BlogModel> logger;
        private IHostingEnvironment environment { get; set; }
        public BlogModel(IHostingEnvironment environment, IBlogRepository blogRepository, IPostRepository postRepository, ILogger<BlogModel> logger) : base(blogRepository, postRepository)
        {
            this.environment = environment;
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

        public async Task<IActionResult> OnPostUploadImage(IFormFile file)
        {
            var folder = environment.WebRootPath + "\\images\\";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            if (file.Length > 0)
            {
                var filePath = Path.Combine(folder, file.FileName);
                try
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return new JsonResult(new { url = "/images/" + file.FileName });
            }
            return null;
        }
    }
}