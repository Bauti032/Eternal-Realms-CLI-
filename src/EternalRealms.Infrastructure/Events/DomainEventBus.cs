using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using EternalRealms.Core.Interfaces;

namespace EternalRealms.Infrastructure.Events
{
    public sealed class DomainEventBus : IEventBus
    {
        private readonly ConcurrentDictionary<Type, List<Delegate>> _subscriptions = new();

        public void Publish<TEvent>(TEvent @event)
            where TEvent : class
        {
            if (@event is null)
            {
                return;
            }

            if (_subscriptions.TryGetValue(typeof(TEvent), out var handlers))
            {
                foreach (var handler in handlers)
                {
                    if (handler is Action<TEvent> typedHandler)
                    {
                        typedHandler(@event);
                    }
                }
            }
        }

        public void Subscribe<TEvent>(Action<TEvent> handler)
            where TEvent : class
        {
            var handlers = _subscriptions.GetOrAdd(typeof(TEvent), _ => new List<Delegate>());
            lock (handlers)
            {
                handlers.Add(handler);
            }
        }
    }
}
