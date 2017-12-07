using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Queryable.Events
{
    public class EventEmitter<TEvent> : IEventManager<TEvent>
    {
        private int _subscriptionId;
        private Dictionary<int, Action<TEvent>> Subscriptions { get; }

        public EventEmitter()
        {
            Subscriptions = new Dictionary<int, Action<TEvent>>();
        }

        int IEventSubscriberBase.Subscribe(Action<object> propertyChangeEvent)
        {
            return Subscribe(e =>
            {
                propertyChangeEvent(e);
            });
        }

        public void Unsubscribe(int subscription)
        {
            Subscriptions.Remove(subscription);
        }

        public int Subscribe(Action<TEvent> propertyChangeEvent)
        {
            var id = ++_subscriptionId;
            Subscriptions.Add(id, propertyChangeEvent);
            return id;
        }

        public void Emit(TEvent propertyChangeEvent)
        {
            var subscriptions = Subscriptions.Keys.ToList();
            foreach (var subscription in subscriptions)
            {
                Subscriptions[subscription](propertyChangeEvent);
            }
        }
    }
}