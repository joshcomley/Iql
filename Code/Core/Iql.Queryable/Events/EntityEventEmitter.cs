using System;
using System.Collections.Generic;

namespace Iql.Queryable.Events
{
    public class EventEmitter<TEvent> : IEventEmitter<TEvent>
    {
        private int _subscriptionId;
        private Dictionary<int, Action<TEvent>> Subscriptions { get; }

        public EventEmitter()
        {
            Subscriptions = new Dictionary<int, Action<TEvent>>();
        }

        void IEventEmitterBase.Emit(object propertyChangeEvent)
        {
            Emit((TEvent) propertyChangeEvent);
        }

        int IEventEmitterBase.Subscribe(Action<object> propertyChangeEvent)
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
            foreach (var subscription in Subscriptions.Keys)
            {
                Subscriptions[subscription](propertyChangeEvent);
            }
        }
    }
}