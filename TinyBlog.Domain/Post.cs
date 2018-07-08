using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TinyBlog.Domain
{
    public class Post
    {
        public Post()
        {
            Id = Guid.NewGuid().ToString();
            PostCategories = new List<string>();
            Comments = new List<Comment>();
            PubDate = DateTime.UtcNow;
            LastModified = DateTime.UtcNow;
        }
        [Required]
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        public string Excerpt { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime PubDate { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsPublished { get; set; }
        public List<string> PostCategories { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
