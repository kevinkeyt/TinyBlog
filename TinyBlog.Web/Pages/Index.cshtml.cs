using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TinyBlog.Data;
using TinyBlog.Domain;

namespace TinyBlog.Web.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly IDataContext dataContext;
        private readonly ILogger<IndexModel> logger;

        public IndexModel(IDataContext dataContext, ILogger<IndexModel> logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public IEnumerable<Post> Posts { get; set; }

        public IActionResult OnGetAsync()
        {
            Posts = (HttpContext.User.Identity.IsAuthenticated) ? this.dataContext.GetAllPosts() : dataContext.GetPublicPosts();
            Blog = dataContext.GetBlogInfo();
            return Page();
        }
    }
}
