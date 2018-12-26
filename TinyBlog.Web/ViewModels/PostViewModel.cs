using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        public string PostCategories { get; set; }

        public List<string> GetPostCategories()
        {
            if (!string.IsNullOrEmpty(PostCategories))
                return PostCategories.Split(",").ToList();
            return new List<string>();
        }
    }
}
