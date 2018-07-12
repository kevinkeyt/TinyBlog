using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TinyBlog.Data;

namespace TinyBlog.Web.Pages
{
    public class ErrorModel : BasePageModel
    {
        private readonly IDataContext dataContext;
        private readonly ILogger<ErrorModel> logger;

        public ErrorModel(IDataContext dataContext, ILogger<ErrorModel> logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult OnGetAsync()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            Blog = dataContext.GetBlogInfo();
            return Page();
        }
    }
}
