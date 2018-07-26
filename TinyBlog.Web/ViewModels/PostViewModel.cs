using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TinyBlog.Core.Entities;

namespace TinyBlog.Web.ViewModels
{
    public class PostViewModel
    {
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
        public List<string> PostCategories { get; set; } = new List<string>();

        public static PostViewModel FromPostEntity(Post entity)
        {
            return new PostViewModel()
            {
                Id = entity.Id,
                Title = entity.Title,
                Author = entity.Author,
                Slug = entity.Slug,
                Excerpt = entity.Excerpt,
                Content = entity.Content,
                PubDate = entity.PubDate,
                LastModified = entity.LastModified,
                PostCategories = entity.PostCategories
            };
        }

        public static Post ToPostEntity(PostViewModel model)
        {
            return new Post()
            {
                Id = model.Id,
                Title = model.Title,
                Author = model.Author,
                Slug = model.Slug,
                Excerpt = model.Excerpt,
                Content = model.Content,
                PubDate = model.PubDate,
                LastModified = model.LastModified,
                IsPublished = model.PubDate <= DateTime.UtcNow,
                PostCategories = model.PostCategories
            };
        }
    }
}
