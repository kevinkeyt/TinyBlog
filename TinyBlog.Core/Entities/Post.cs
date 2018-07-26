using System;
using System.Collections.Generic;
using TinyBlog.Core.Events;
using TinyBlog.Core.SharedKernel;

namespace TinyBlog.Core.Entities
{
    public class Post : BaseEntity
    {
        public Post()
        {
        }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Slug { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        public DateTime PubDate { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsPublished { get; set; }
        public List<string> PostCategories { get; set; } = new List<string>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
