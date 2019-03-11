using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iql.Events
{
    public class AsyncEventEmitter<TEvent> : EventEmitterBase<TEvent, Func<TEvent, Task>>, IAsyncEventManager<TEvent>
    {
        public AsyncEventEmitter(BackfireMode backfireMode = BackfireMode.None) : base(backfireMode)
        {
            
        }
        EventSubscription IAsyncEventSubscriberBase.SubscribeAsync(Func<object, Task> action)
        {
            return SubscribeAsync(async e =>
            {
                await action(e);
            });
        }

        public EventSubscription SubscribeAsync(Func<TEvent, Task> action)
        {
            var sub = SubscribeInternal(action);
            switch (BackfireMode)
            {
                case BackfireMode.All:
                    foreach (var ev in Backfires)
                    {
#pragma warning disable 4014
                        EmitToSubscriptionsAsync(() => ev, null, new[] { action }.ToList());
#pragma warning restore 4014
                    }
                    break;
                case BackfireMode.Last:
                    if (Backfires.Any())
                    {
                        var last = Backfires.LastOrDefault();
#pragma warning disable 4014
                        EmitToSubscriptionsAsync(() => last, null, new[] { action }.ToList());
#pragma warning restore 4014
                    }
                    break;
            }
            return sub;
        }

        public async Task<TEvent> EmitAsync(Func<TEvent> eventObjectFactory, Func<TEvent, Task> afterEvent = null, IEnumerable<EventSubscription> subscriptions = null)
        {
            return await EmitToSubscriptionsAsync(eventObjectFactory, afterEvent, ResolveSubscriptionActions(subscriptions));
        }

        private async Task<TEvent> EmitToSubscriptionsAsync(Func<TEvent> eventObjectFactory, Func<TEvent, Task> afterEvent, List<Func<TEvent, Task>> subscriptionActions)
        {
            eventObjectFactory = BuildEventObjectFactory(eventObjectFactory);
            if (subscriptionActions != null && subscriptionActions.Count > 0)
            {
                var ev = eventObjectFactory();
                for (var i = 0; i < subscriptionActions.Count; i++)
                {
                    try
                    {
                        await subscriptionActions[i](ev);
                    }
                    catch (Exception e)
                    {
                        if (EventEmitterExceptions.ShouldBeThrown(e))
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
    }
}