using Ardalis.GuardClauses;
using TinyBlog.Core.Events;
using TinyBlog.Core.Interfaces;

namespace TinyBlog.Core.Services
{
    public class PostService : IHandle<PostPublishedEvent>
    {
        public void Handle(PostPublishedEvent domainEvent)
        {
            Guard.Against.Null(domainEvent, nameof(domainEvent));


        }
    }
}
