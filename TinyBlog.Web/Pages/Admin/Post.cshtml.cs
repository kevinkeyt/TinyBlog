using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using TinyBlog.Data;
using TinyBlog.Domain;

namespace TinyBlog.Web.Pages.Admin
{
    public class PostModel : PageModel
    {
        [BindProperty]
        public Post Post { get; set; }

        public Dictionary<string, int> Categories { get; set; }

        private readonly IDataContext dataContext;

        public PostModel(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IActionResult OnGet(string id)
        {
            Categories = dataContext.GetCategories();
            if (string.IsNullOrEmpty(id))
            {
                Post = new Post();
            }
            else
            {
                Post = dataContext.GetPostById(id);
            }
            // If post is null it should redirect to 404
            if(Post == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                dataContext.Save(Post);
            }

            return Page();
        }

        //public class AddCategoryModel
        //{
        //    public string Id { get; set; }
        //    public string Category { get; set; }
        //}

        //public JsonResult OnPostAddCategory(AddCategoryModel model)
        //{
        //    Post = dataContext.GetPostById(model.Id);

        //    if (!Post.PostCategories.Contains(model.Category))
        //    {
        //        Post.PostCategories.Add(model.Category);
        //        // Save
        //        dataContext.Save(Post);
        //        return new JsonResult(new { added = true });
        //    }
        //    return new JsonResult(new { added = false });
        //}
    }
}