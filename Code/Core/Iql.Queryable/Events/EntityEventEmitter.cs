using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Queryable.Events
{
    public class EventEmitter<TEvent> : IEventManager<TEvent>
    {
        private int _subscriptionId;

        private Dictionary<int, Action<TEvent>> _subscriptions;

        public EventEmitter()
        {
            
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
            if (_subscriptions == null)
            {
                return;
            }
            _subscriptions.Remove(subscription);
        }

        public int Subscribe(Action<TEvent> propertyChangeEvent)
        {
            if (_subscriptions == null)
            {
                _subscriptions = new Dictionary<int, Action<TEvent>>();
            }
            var id = ++_subscriptionId;
            _subscriptions.Add(id, propertyChangeEvent);
            SubscriptionActions = _subscriptions.Values.ToList();
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