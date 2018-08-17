using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Entities.Events
{
    public class EventEmitter<TEvent> : IEventManager<TEvent>
    {
        private int _subscriptionId;

        private Dictionary<int, Action<TEvent>> _subscriptions;

        public EventEmitter()
        {
            
        }

        EventSubscription IEventSubscriberBase.Subscribe(Action<object> action)
        {
            return Subscribe(e =>
            {
                action(e);
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

        public EventSubscription Subscribe(Action<TEvent> action)
        {
            if (_subscriptions == null)
            {
                _subscriptions = new Dictionary<int, Action<TEvent>>();
            }
            var id = ++_subscriptionId;
            _subscriptions.Add(id, action);
            SubscriptionActions = _subscriptions.Values.ToList();
            return new EventSubscription(this, id);
        }

        private List<Action<TEvent>> SubscriptionActions { get; set; }

        public TEvent Emit(Func<TEvent> eventObjectFactory, Action<TEvent> afterEvent = null)
        {
            if (SubscriptionActions != null && SubscriptionActions.Count > 0)
            {
                var ev = eventObjectFactory == null ? (TEvent)(object)null : eventObjectFactory();
                for (var i = 0; i < SubscriptionActions.Count; i++)
                {
                    SubscriptionActions[i](ev);
                }
                if (afterEvent != null)
                {
                    afterEvent(ev);
                }

                return ev;
            }

            return default(TEvent);
        }

        public void UnsubscribeAll()
        {
            _subscriptions = new Dictionary<int, Action<TEvent>>();
            SubscriptionActions = new List<Action<TEvent>>();
        }

        public void Dispose()
        {
            UnsubscribeAll();
        }
    }
}