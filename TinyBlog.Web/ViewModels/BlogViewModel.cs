using System.ComponentModel.DataAnnotations;

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
    }
}
