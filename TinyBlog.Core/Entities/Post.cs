using System;
using System.Collections.Generic;
using System.Linq;
using TinyBlog.Core.Events;
using TinyBlog.Core.SharedKernel;

namespace TinyBlog.Core.Entities
{
    public class Post : BaseEntity
    {
        public Post() {
            LastModified = DateTime.UtcNow;
        }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Slug { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        public DateTime PubDate { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsPublished { get; set; }
        public string PostCategories { get; set; }

        public void SetPubDate(DateTime pubDate)
        {
            PubDate = pubDate;
            IsPublished = pubDate <= DateTime.UtcNow;
            if(IsPublished)
                Events.Add(new PostPublishedEvent(this));
        }

        public (bool, string)AddCategory(string category)
        {
            var _postCategories = GetPostCategories();
            if (!_postCategories.Any(x => x == category))
            {
                _postCategories.Add(category);
                PostCategories = string.Join(",", _postCategories);
                return (true, "Success");
            }
            return (false, "Already exists");
        }

        public (bool, string) RemoveCategory(string category)
        {
            var _postCategories = GetPostCategories();
            if (_postCategories.Any(x => x == category))
            {
                _postCategories.Remove(category);
                PostCategories = string.Join(",", _postCategories);
                return (true, "Success");
            }
            return (false, "Does not exist");
        }

        public List<string> GetPostCategories()
        {
            if (!string.IsNullOrEmpty(PostCategories))
                return PostCategories.Split(',').ToList();
            return new List<string>();
        }
    }
}
