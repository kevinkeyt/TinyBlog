using Newtonsoft.Json;
using System.Collections.Generic;

namespace TinyBlog.Core.SharedKernel
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }

        [JsonIgnore]
        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}
