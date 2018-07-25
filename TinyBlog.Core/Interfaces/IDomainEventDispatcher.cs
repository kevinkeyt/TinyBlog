using TinyBlog.Core.SharedKernel;

namespace TinyBlog.Core.Interfaces
{
    public interface IDomainEventDispatcher
    {
        void Dispatch(BaseDomainEvent domainEvent);
    }
}
