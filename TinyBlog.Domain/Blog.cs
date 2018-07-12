using System.ComponentModel.DataAnnotations;

namespace TinyBlog.Domain
{
    public class Blog
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string AboutTitle { get; set; }
        public string AboutDescription { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CdnUrl { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string GitHub { get; set; }
        public string LinkedIn { get; set; }
    }
}
