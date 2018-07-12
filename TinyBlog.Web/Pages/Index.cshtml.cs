using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TinyBlog.Data;
using TinyBlog.Domain;

namespace TinyBlog.Web.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly ILogger<IndexModel> logger;

        public IndexModel(IDataContext dataContext, ILogger<IndexModel> logger) : base(dataContext)
        {
            this.logger = logger;
        }

        public IEnumerable<Post> Posts { get; set; }

        public IActionResult OnGetAsync(string c)
        {
            if (!string.IsNullOrEmpty(c))
                Posts = dataContext.GetPostsByCategory(c);
            else
                Posts = (HttpContext.User.Identity.IsAuthenticated) ? dataContext.GetAllPosts() : dataContext.GetPublicPosts();
            
            return Page();
        }
    }
}
