using System.Collections.Generic;
using TinyBlog.Core.SharedKernel;

namespace TinyBlog.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(string id);
        List<T> ListAll();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
