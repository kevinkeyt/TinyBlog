using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TinyBlog.Core.Events;
using TinyBlog.Core.SharedKernel;

namespace TinyBlog.Core.Entities
{
    public class Post : BaseEntity
    {
        public Post(string title, string author, string slug) : this()
        {           
            Title = title;
            Author = author;
            Slug = slug;
        }

        private Post()
        {
            Id = Guid.NewGuid().ToString();
            LastModified = DateTime.UtcNow;
            PostCategories = new List<string>();
        }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Slug { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        [JsonProperty]
        public DateTime PubDate { get; private set; }
        public DateTime LastModified { get; set; }
        [JsonProperty]
        public bool IsPublished { get; private set; }
        [JsonProperty]
        public List<string> PostCategories { get; private set; }

        public void SetPubDate(DateTime pubDate)
        {
            PubDate = pubDate;
            IsPublished = pubDate <= DateTime.UtcNow;
            if(IsPublished)
                Events.Add(new PostPublishedEvent(this));
        }

        public (bool, string)AddCategory(string category)
        {
            if(PostCategories != null)
            {
                if(!PostCategories.Any(x => x == category))
                {
                    PostCategories.Add(category);
                    return (true, "Success");
                }
                return (false, "Already exists");
            }
            return (false, "Array is null");
        }

        public (bool, string) RemoveCategory(string category)
        {
            if (PostCategories != null)
            {
                if (PostCategories.Any(x => x == category))
                {
                    PostCategories.Remove(category);
                    return (true, "Success");
                }
                return (false, "Does not exist");
            }
            return (false, "Array is null");
        }
    }
}
