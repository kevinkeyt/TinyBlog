using TinyBlog.Core.SharedKernel;

namespace TinyBlog.Core.Interfaces
{
    public interface IHandle<T> where T : BaseDomainEvent
    {
        void Handle(T domainEvent);
    }
}
