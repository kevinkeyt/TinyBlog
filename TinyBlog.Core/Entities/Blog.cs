using TinyBlog.Core.SharedKernel;

namespace TinyBlog.Core.Entities
{
    public class Blog : BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShareUrl { get; set; }
        public string ShareImageUrl { get; set; }
        public string AboutTitle { get; set; }
        public string AboutDescription { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CdnUrl { get; set; }
        public string Twitter { get; set; }
        public string TwitterName { get; set; }
        public string Facebook { get; set; }
        public string GitHub { get; set; }
        public string LinkedIn { get; set; }
    }
}
