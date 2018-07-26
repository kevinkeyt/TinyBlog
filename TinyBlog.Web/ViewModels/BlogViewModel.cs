using System.ComponentModel.DataAnnotations;
using TinyBlog.Core.Entities;

namespace TinyBlog.Web.ViewModels
{
    public class BlogViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string ShareUrl { get; set; }
        public string ShareImageUrl { get; set; }
        [Required]
        public string AboutTitle { get; set; }
        [Required]
        public string AboutDescription { get; set; }
        [Required]
        public string ContactTitle { get; set; }
        [Required]
        public string ContactDescription { get; set; }
        public string CdnUrl { get; set; }
        public string Twitter { get; set; }
        public string TwitterName { get; set; }
        public string Facebook { get; set; }
        public string GitHub { get; set; }
        public string LinkedIn { get; set; }

        public static BlogViewModel FromBlogEntity(Blog entity)
        {
            return new BlogViewModel()
            {
                Name = entity.Name,
                Title = entity.Title,
                Description = entity.Description,
                ShareUrl = entity.ShareUrl,
                ShareImageUrl = entity.ShareUrl,
                AboutTitle = entity.AboutTitle,
                AboutDescription = entity.AboutDescription,
                ContactTitle = entity.ContactTitle,
                ContactDescription = entity.ContactDescription,
                CdnUrl = entity.CdnUrl,
                Twitter = entity.Twitter,
                TwitterName = entity.TwitterName,
                Facebook = entity.Facebook,
                GitHub = entity.GitHub,
                LinkedIn = entity.LinkedIn
            };
        }

        public static Blog ToBlogEntity(BlogViewModel model)
        {
            return new Blog()
            {
                Name = model.Name,
                Title = model.Title,
                Description = model.Description,
                ShareUrl = model.ShareUrl,
                ShareImageUrl = model.ShareUrl,
                AboutTitle = model.AboutTitle,
                AboutDescription = model.AboutDescription,
                ContactTitle = model.ContactTitle,
                ContactDescription = model.ContactDescription,
                CdnUrl = model.CdnUrl,
                Twitter = model.Twitter,
                TwitterName = model.TwitterName,
                Facebook = model.Facebook,
                GitHub = model.GitHub,
                LinkedIn = model.LinkedIn
            };
        }
    }
}
