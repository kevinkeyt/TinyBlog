using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TinyBlog.Data;
using TinyBlog.Domain;

namespace TinyBlog.Web.Pages
{
    public class IndexModel : PageModel
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
            this.Posts = (HttpContext.User.Identity.IsAuthenticated) ? dataContext.GetAllPosts() : dataContext.GetPublicPosts();
            return Page();
        }
    }
}
