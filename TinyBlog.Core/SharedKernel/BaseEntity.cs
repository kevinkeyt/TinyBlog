using Microsoft.Azure.Cosmos.Table;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TinyBlog.Core.SharedKernel
{
    public abstract class BaseEntity: TableEntity
    {
        [JsonIgnore]
        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}
