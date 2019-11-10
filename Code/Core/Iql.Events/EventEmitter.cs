﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Events
{
    public class EventEmitter<TEvent> : EventEmitterBase<TEvent, Action<TEvent>>, IEventManager<TEvent>
    {
        public EventEmitter(BackfireMode backfireMode = BackfireMode.None) : base(backfireMode)
        {

        }

        public int SubscriptionCount => Subscriptions?.Count ?? 0;

        EventSubscription IEventSubscriberBase.Subscribe(Action<object> action, string key = null, int? allowedCount = null)
        {
            return Subscribe(e =>
                {
                    action(e);
                },
                key, allowedCount);
        }

        EventSubscription IEventSubscriberBase.SubscribeOnce(Action<object> action, string key = null)
        {
            return SubscribeOnce(e =>
                {
                    action(e);
                },
                key);
        }

        public EventSubscription SubscribeOnce(Action<TEvent> action, string key = null)
        {
            return Subscribe(action, key, 1);
        }

        public EventSubscription Subscribe(Action<TEvent> action, string key = null, int? allowedCount = null)
        {
            var sub = SubscribeInternal(action, key, allowedCount);
            switch (BackfireMode)
            {
                case BackfireMode.All:
                    var history = Backfires.ToArray();
                    foreach (var ev in history)
                    {
                        EmitToSubscriptions(() => ev, null, new[] { new SubscriptionAction<Action<TEvent>>(sub, action) }.ToList());
                    }
                    break;
                case BackfireMode.Last:
                    if (Backfires.Any())
                    {
                        var last = Backfires.LastOrDefault();
                        EmitToSubscriptions(() => last, null, new[] { new SubscriptionAction<Action<TEvent>>(sub, action) }.ToList());
                    }
                    break;
            }
            return sub;
        }

        public TEvent Emit(Func<TEvent> eventObjectFactory, Action<TEvent> afterEvent = null, IEnumerable<EventSubscription> subscriptions = null)
        {
            return EmitToSubscriptions(eventObjectFactory, afterEvent, ResolveSubscriptionActions(subscriptions));
        }

        private TEvent EmitToSubscriptions(Func<TEvent> eventObjectFactory, Action<TEvent> afterEvent, List<SubscriptionAction<Action<TEvent>>> subscriptionActions)
        {
            eventObjectFactory = BuildEventObjectFactory(eventObjectFactory);
            if (subscriptionActions != null && subscriptionActions.Count > 0)
            {
                var ev = eventObjectFactory();
                for (var i = 0; i < subscriptionActions.Count; i++)
                {
                    var subscriptionAction = subscriptionActions[i];
                    if (subscriptionAction.Subscription.Paused || !subscriptionAction.Subscription.IsWithinAllowedCallCount)
                    {
                        continue;
                    }
                    try
                    {
                        subscriptionAction.Subscription.RegisterCalled();
                        subscriptionAction.Action(ev);
                        if (!subscriptionAction.Subscription.IsWithinAllowedCallCount && !subscriptionAction.Subscription.KeepSubscriptionAfterCallCount)
                        {
                            subscriptionAction.Subscription.Unsubscribe();
                        }
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