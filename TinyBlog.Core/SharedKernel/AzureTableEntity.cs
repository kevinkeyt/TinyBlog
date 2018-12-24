using Microsoft.Azure.Cosmos.Table;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TinyBlog.Core.SharedKernel
{
    public class AzureTableEntity: TableEntity
    {
        [JsonIgnore]
        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}
