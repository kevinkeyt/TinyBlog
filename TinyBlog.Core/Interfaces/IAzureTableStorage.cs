using Microsoft.Azure.Cosmos.Table;
using System.Collections.Generic;
using System.Threading.Tasks;
using TinyBlog.Core.SharedKernel;

namespace TinyBlog.Core.Interfaces
{
    //
    // Reference: https://tahirnaushad.com/2017/08/29/azure-table-storage-in-asp-net-core-2-0/
    public interface IAzureTableStorage<T> where T : BaseEntity, new()
    {
        Task Delete(string partitionKey, string rowKey);
        Task<T> GetItem(string partitionKey, string rowKey);
        Task<List<T>> GetList();
        Task<List<T>> GetList(string partitionKey);
        Task Insert(T item);
        Task Update(T item);
        Task<CloudTable> GetTableAsync();
    }
}
