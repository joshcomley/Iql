using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iql.Events
{
    public class AsyncEventEmitter<TEvent> : IAsyncEventManager<TEvent>
    {
        private int _subscriptionId;

        private Dictionary<int, Func<TEvent, Task>> _subscriptions;

        public AsyncEventEmitter()
        {

        }

        EventSubscription IAsyncEventSubscriberBase.SubscribeAsync(Func<object, Task> action)
        {
            return SubscribeAsync(async e =>
            {
                await action(e);
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

        public EventSubscription SubscribeAsync(Func<TEvent, Task> action)
        {
            if (_subscriptions == null)
            {
                _subscriptions = new Dictionary<int, Func<TEvent, Task>>();
            }
            var id = ++_subscriptionId;
            _subscriptions.Add(id, action);
            SubscriptionActions = _subscriptions.Values.ToList();
            return new EventSubscription(this, id);
        }

        private List<Func<TEvent, Task>> SubscriptionActions { get; set; }

        public async Task<TEvent> EmitAsync(Func<TEvent> eventObjectFactory, Func<TEvent, Task> afterEvent = null)
        {
            if (SubscriptionActions != null && SubscriptionActions.Count > 0)
            {
                var ev = eventObjectFactory == null ? (TEvent)(object)null : eventObjectFactory();
                for (var i = 0; i < SubscriptionActions.Count; i++)
                {
                    try
                    {
                        await SubscriptionActions[i](ev);
                    }
                    catch (Exception e)
                    {
                        if(EventEmitterExceptions.ShouldBeThrown(e))
                        {
                            throw e;
                        }
                    }
                }
                if (afterEvent != null)
                {
                    try
                    {
                        await afterEvent(ev);
                    }
                    catch (Exception e)
                    {
                        if (EventEmitterExceptions.ShouldBeThrown(e))
                        {
                            throw e;
                        }
                    }
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