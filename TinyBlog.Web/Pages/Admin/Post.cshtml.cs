using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TinyBlog.Data;
using TinyBlog.Domain;

namespace TinyBlog.Web.Pages.Admin
{
    public class PostModel : BasePageModel
    {
        [BindProperty]
        public Post Post { get; set; }

        private readonly ILogger<PostModel> logger;

        public PostModel(IDataContext dataContext, ILogger<PostModel> logger) : base(dataContext)
        {
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
    }
}