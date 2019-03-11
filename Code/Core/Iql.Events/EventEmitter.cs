using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Events
{
    public class EventEmitter<TEvent> : EventEmitterBase<TEvent, Action<TEvent>>, IEventManager<TEvent>
    {
        public EventEmitter(BackfireMode backfireMode = BackfireMode.None) : base(backfireMode)
        {

        }

        EventSubscription IEventSubscriberBase.Subscribe(Action<object> action)
        {
            return Subscribe(e =>
            {
                action(e);
            });
        }

        public EventSubscription Subscribe(Action<TEvent> action)
        {
            var sub = SubscribeInternal(action);
            switch (BackfireMode)
            {
                case BackfireMode.All:
                    var history = Backfires.ToArray();
                    foreach (var ev in history)
                    {
                        EmitToSubscriptions(() => ev, null, new[] { action }.ToList());
                    }
                    break;
                case BackfireMode.Last:
                    if (Backfires.Any())
                    {
                        var last = Backfires.LastOrDefault();
                        EmitToSubscriptions(() => last, null, new[] { action }.ToList());
                    }
                    break;
            }
            return sub;
        }

        public TEvent Emit(Func<TEvent> eventObjectFactory, Action<TEvent> afterEvent = null, IEnumerable<EventSubscription> subscriptions = null)
        {
            return EmitToSubscriptions(eventObjectFactory, afterEvent, ResolveSubscriptionActions(subscriptions));
        }

        private TEvent EmitToSubscriptions(Func<TEvent> eventObjectFactory, Action<TEvent> afterEvent, List<Action<TEvent>> subscriptionActions)
        {
            eventObjectFactory = BuildEventObjectFactory(eventObjectFactory);
            if (subscriptionActions != null && subscriptionActions.Count > 0)
            {
                var ev = eventObjectFactory();
                for (var i = 0; i < subscriptionActions.Count; i++)
                {
                    try
                    {
                        subscriptionActions[i](ev);
                    }
                    catch (Exception e)
                    {
                        ValidateException(e);
                    }
                }

                if (afterEvent != null)
                {
                    try
                    {
                        afterEvent(ev);
                    }
                    catch (Exception e)
                    {
                        ValidateException(e);
                    }
                }

                return ev;
            }

            return default(TEvent);
        }

        private static void ValidateException(Exception e)
        {
            if (e != null)
            {
                if (EventEmitterExceptions.ShouldBeThrown(e))
                {
                    throw e;
                }
#if !TypeScript
                ValidateException(e.InnerException);
#endif
            }
        }
    }
}