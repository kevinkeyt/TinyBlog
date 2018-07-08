using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TinyBlog.Data;
using TinyBlog.Domain;

namespace TinyBlog.Web.Pages
{
    public class PostModel : PageModel
    {
        public string Slug { get; set; }
        public Post Post { get; set; }

        private readonly IDataContext dataContext;

        public PostModel(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void OnGet(string slug)
        {
            Slug = slug;
            Post = dataContext.GetPostBySlug(slug);
            // If post is null it should redirect to 404
        }
    }
}