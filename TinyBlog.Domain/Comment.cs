using System;

namespace TinyBlog.Domain
{
    public class Comment
    {
        public Comment()
        {
            Id = Guid.NewGuid().ToString();
            PubDate = DateTime.UtcNow;
        }
        public string Id { get; set; }
        public string Author { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Content { get; set; }
        public DateTime PubDate { get; set; }
        public string Ip { get; set; }
        public string UserAgent { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsApproved { get; set; }
    }
}
