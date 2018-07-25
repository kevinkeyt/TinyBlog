using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TinyBlog.Core.Interfaces;
using TinyBlog.Web.ViewModels;

namespace TinyBlog.Web.Pages.Admin
{
    public class PostModel : BasePageModel
    {
        [BindProperty]
        public PostViewModel Post { get; set; }

        private IHostingEnvironment Environment { get; set; }
        private readonly ILogger<PostModel> logger;

        public PostModel(IHostingEnvironment environment, IBlogRepository blogRepository, IPostRepository postRepository, ILogger<PostModel> logger) : base(blogRepository, postRepository)
        {
            this.Environment = environment;
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
                Post = PostViewModel.FromPostEntity(postRepository.GetById(id));
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
                var post = PostViewModel.ToPostEntity(Post);
                if (string.IsNullOrEmpty(Post.Id))
                {
                    // Add
                    post.Id = Guid.NewGuid().ToString();
                    post.PublishPost(post.PubDate);
                    postRepository.Add(post);
                    logger.LogInformation($"Post {Post.Title} was added on {DateTime.UtcNow}.");
                    return Redirect($"/Admin/Post/{post.Id}");
                }
                else
                {
                    postRepository.Update(post);
                    logger.LogInformation($"Post {Post.Title} was updated on {DateTime.UtcNow}.");
                }
            }
            Blog = BlogViewModel.FromBlogEntity(blogRepository.GetBlogInfo());
            Categories = postRepository.GetCategories();
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