using TinyBlog.Core.Entities;
using TinyBlog.Core.SharedKernel;

namespace TinyBlog.Core.Events
{
    public class PostPublishedEvent : BaseDomainEvent
    {
        public Post PublishedItem { get; set; }

        public PostPublishedEvent(Post publishedItem)
        {
            PublishedItem = publishedItem;
        }
    }
}
