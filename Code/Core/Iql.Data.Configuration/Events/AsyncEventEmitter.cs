using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iql.Queryable.Events
{
    public class AsyncEventEmitter<TEvent> : IAsyncEventManager<TEvent>
    {
        private int _subscriptionId;

        private Dictionary<int, Func<TEvent, Task>> _subscriptions;

        public AsyncEventEmitter()
        {

        }

        EventSubscription IAsyncEventSubscriberBase.SubscribeAsync(Func<object, Task> propertyChangeEvent)
        {
            return SubscribeAsync(async e =>
            {
                await propertyChangeEvent(e);
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

        public void UnsubscribeAll()
        {
            _subscriptions = new Dictionary<int, Func<TEvent, Task>>();
            SubscriptionActions = new List<Func<TEvent, Task>>();
        }

        public EventSubscription SubscribeAsync(Func<TEvent, Task> propertyChangeEvent)
        {
            if (_subscriptions == null)
            {
                _subscriptions = new Dictionary<int, Func<TEvent, Task>>();
            }
            var id = ++_subscriptionId;
            _subscriptions.Add(id, propertyChangeEvent);
            SubscriptionActions = _subscriptions.Values.ToList();
            return new EventSubscription(this, id);
        }

        private List<Func<TEvent, Task>> SubscriptionActions { get; set; }

        public async Task<TEvent> EmitAsync(Func<TEvent> propertyChangeEvent, Func<TEvent, Task> afterEvent = null)
        {
            if (SubscriptionActions != null && SubscriptionActions.Count > 0)
            {
                var ev = propertyChangeEvent();
                for (var i = 0; i < SubscriptionActions.Count; i++)
                {
                    await SubscriptionActions[i](ev);
                }
                if (afterEvent != null)
                {
                    await afterEvent(ev);
                }

                return ev;
            }

            return default(TEvent);
        }

        public void Dispose()
        {
            UnsubscribeAll();
        }
    }
}