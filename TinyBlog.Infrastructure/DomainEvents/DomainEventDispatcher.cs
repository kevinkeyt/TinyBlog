using System.Collections.Generic;
using TinyBlog.Core.Interfaces;
using TinyBlog.Core.SharedKernel;
using Autofac;
using System.Collections;

namespace TinyBlog.Infrastructure.DomainEvents
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly ILifetimeScope _lifetimeScope;

        public DomainEventDispatcher(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public void Dispatch(BaseDomainEvent eventToDispatch)
        {
            foreach (dynamic handler in GetHandlers(eventToDispatch))
            {
                handler.Handle((dynamic)eventToDispatch);
            }
        }

        private IEnumerable GetHandlers<TEvent>(TEvent eventToDispatch) where TEvent : BaseDomainEvent
        {
            return (IEnumerable)_lifetimeScope.Resolve(
                typeof(IEnumerable<>).MakeGenericType(
                    typeof(IHandle<>).MakeGenericType(eventToDispatch.GetType())));
        }
    }
}
