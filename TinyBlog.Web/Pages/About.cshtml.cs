﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TinyBlog.Data;

namespace TinyBlog.Web.Pages
{
    public class AboutModel : BasePageModel
    {
        private readonly ILogger<AboutModel> logger;

        public AboutModel(IDataContext dataContext, ILogger<AboutModel> logger) : base(dataContext)
        {
            this.logger = logger;
        }

        public string Message { get; set; }

        public IActionResult OnGetAsync()
        {
            Message = "Your application description page.";
            return Page();
        }
    }
}
