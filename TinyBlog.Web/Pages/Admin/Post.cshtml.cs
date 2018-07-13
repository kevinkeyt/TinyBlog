using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TinyBlog.Data;
using TinyBlog.Domain;

namespace TinyBlog.Web.Pages.Admin
{
    public class PostModel : BasePageModel
    {
        [BindProperty]
        public Post Post { get; set; }

        private IHostingEnvironment Environment { get; set; }
        private readonly ILogger<PostModel> logger;

        public PostModel(IHostingEnvironment environment, IDataContext dataContext, ILogger<PostModel> logger) : base(dataContext)
        {
            this.Environment = environment;
            this.logger = logger;
        }

        public IActionResult OnGet(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Post = new Post
                {
                    Author = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "FullName").Value
                };
            }
            else
            {
                Post = dataContext.GetPostById(id);
            }
            if(Post == null)
            {
                logger.LogInformation($"Could not find post for id {id}.");
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                dataContext.SavePost(Post);
                logger.LogInformation($"Post {Post.Title} was updated on {DateTime.UtcNow}.");
            }
            Blog = dataContext.GetBlogInfo();
            Categories = dataContext.GetCategories();
            return Page();
        }

        public async Task<IActionResult> OnPostUploadImage(IFormFile file)
        {
            var folder = Environment.WebRootPath + "\\images\\";
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