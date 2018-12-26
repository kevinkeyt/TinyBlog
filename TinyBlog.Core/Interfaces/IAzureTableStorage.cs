using System.Collections.Generic;
using System.Threading.Tasks;
using TinyBlog.Core.SharedKernel;

namespace TinyBlog.Core.Interfaces
{
    public interface IAzureTableStorage<T> where T : BaseEntity, new()
    {
        Task Delete(string partitionKey, string rowKey);
        Task<T> GetItem(string partitionKey, string rowKey);
        Task<List<T>> GetList();
        Task<List<T>> GetList(string partitionKey);
        Task Insert(T item);
        Task Update(T item);
    }
}
