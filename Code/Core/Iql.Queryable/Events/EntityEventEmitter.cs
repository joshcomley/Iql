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
            if (Subscriptions.Count == 0)
            {
                SubscriptionActions = null;
            }
        }

        public int Subscribe(Action<TEvent> propertyChangeEvent)
        {
            var id = ++_subscriptionId;
            Subscriptions.Add(id, propertyChangeEvent);
            this.SubscriptionActions = Subscriptions.Values.ToList();
            return id;
        }

        private List<Action<TEvent>> SubscriptionActions { get; set; }

        public void Emit(Func<TEvent> propertyChangeEvent)
        {
            if (SubscriptionActions != null && SubscriptionActions.Count > 0)
            {
                var ev = propertyChangeEvent();
                for (var i = 0; i < SubscriptionActions.Count; i++)
                {
                    SubscriptionActions[i](ev);
                }
            }
        }
    }
}