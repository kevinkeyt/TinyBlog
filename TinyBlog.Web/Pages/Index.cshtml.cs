using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using TinyBlog.Data;
using TinyBlog.Domain;

namespace TinyBlog.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IDataContext dataContext;

        public IndexModel(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<Post> Posts { get; set; }

        public void OnGet()
        {
            this.Posts = dataContext.GetPublicPosts();
        }
    }
}
