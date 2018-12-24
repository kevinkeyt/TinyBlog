using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TinyBlog.Web.ViewModels
{
    public class PostViewModel
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
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
        public bool IsPublished { get; set; }
        public DateTime LastModified { get; set; }
        public List<string> PostCategories { get; set; } = new List<string>();
    }
}
