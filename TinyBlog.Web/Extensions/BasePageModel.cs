using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyBlog.Data;
using TinyBlog.Domain;

namespace TinyBlog.Web.Pages
{
    public class BasePageModel : PageModel
    {
        public Blog Blog { get; set; }

    }
}
