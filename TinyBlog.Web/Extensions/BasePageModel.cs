using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using TinyBlog.Data;
using TinyBlog.Domain;

namespace TinyBlog.Web.Pages
{
    public class BasePageModel : PageModel
    {
        public Blog Blog { get; set; }

        public Dictionary<string, int> Categories { get; set; }

        internal readonly IDataContext dataContext;

        public BasePageModel(IDataContext dataContext)
        {
            this.dataContext = dataContext;
            Blog = dataContext.GetBlogInfo();
            Categories = dataContext.GetCategories();
        }
    }
}
