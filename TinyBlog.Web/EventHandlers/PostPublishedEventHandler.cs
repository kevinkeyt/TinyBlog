using System;
using TinyBlog.Core.Events;
using TinyBlog.Core.Interfaces;

namespace TinyBlog.Web.EventHandlers
{
    public class PostPublishedEventHandler: IHandle<PostPublishedEvent>
    {
        public void Handle(PostPublishedEvent domainEvent)
        {
            // throw new NotImplementedException();
        }
    }
}
