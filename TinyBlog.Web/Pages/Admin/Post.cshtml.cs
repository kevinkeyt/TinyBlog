using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TinyBlog.Web.Interfaces;
using TinyBlog.Web.ViewModels;

namespace TinyBlog.Web.Pages.Admin
{
    public class PostModel : BasePageModel
    {
        [BindProperty]
        public PostViewModel Post { get; set; }

        private IHostingEnvironment environment { get; set; }
        private readonly ILogger<PostModel> logger;
        public PostModel(IHostingEnvironment environment, 
            IBlogService blogService, 
            IPostService postService, 
            ILogger<PostModel> logger) : base(blogService, postService)
        {
            this.environment = environment;
            this.logger = logger;
        }

        public IActionResult OnGet(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Post = new PostViewModel
                {
                    Author = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "FullName").Value
                };
            }
            else
            {
                Post = postService.GetById(id);
            }
            if(Post == null)
            {
                logger.LogInformation($"Could not find post for id {id}.");
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(Post.RowKey))
                {
                    // Add
                    Post = postService.Add(Post);
                    logger.LogInformation($"Post {Post.Title} was added on {DateTime.UtcNow}.");
                    return Redirect($"/Admin/Post/{Post.RowKey}");
                }
                else
                {
                    postService.Update(Post);
                    logger.LogInformation($"Post {Post.Title} was updated on {DateTime.UtcNow}.");
                }
            }
            Blog = blogService.GetBlogInfo();
            Categories = await postService.GetCategories();
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