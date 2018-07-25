using System.Collections.Generic;

namespace TinyBlog.Core.SharedKernel
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }

        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}
