using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iql.Events
{
    public class EventEmitter<TEvent> : EventEmitterBase<TEvent, Action<TEvent>>, IEventManager<TEvent>
    {
        public EventEmitter(BackfireMode backfireMode = BackfireMode.None) : base(backfireMode)
        {

        }

        public EventEmitter<TEvent> RegisterWith(IqlEventEmitterManager manager)
        {
            manager.Register(this);
            return this;
        }

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

        public TEvent Emit(Func<TEvent> eventObjectFactory = null, Action<TEvent> afterEvent = null, IEnumerable<EventSubscription> subscriptions = null)
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
                    if (IsPaused)
                    {
                        _queuedEmits.Add(() => EmitToSubscription(subscriptionAction, ev));
                    }
                    else
                    {
                        EmitToSubscription(subscriptionAction, ev);
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

        private static void EmitToSubscription(SubscriptionAction<Action<TEvent>> subscriptionAction, TEvent ev)
        {
            try
            {
                subscriptionAction.Subscription.RegisterCalled();
                subscriptionAction.Action(ev);
                if (!subscriptionAction.Subscription.IsWithinAllowedCallCount &&
                    !subscriptionAction.Subscription.KeepSubscriptionAfterCallCount)
                {
                    subscriptionAction.Subscription.Unsubscribe();
                }
            }
            catch (Exception e)
            {
                ValidateException(e);
            }
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

        object IEventEmitterBase.Emit(Func<object> eventObjectFactory = null, Action<object> afterEvent = null, IEnumerable<EventSubscription> subscriptions = null)
        {
            return Emit(
                eventObjectFactory == null ? (Func<TEvent>)null : () => { return (TEvent)eventObjectFactory(); },
                afterEvent == null ? (Action<TEvent>)null : _ => { afterEvent(_); },
                subscriptions);
        }

        private readonly List<Action> _queuedEmits = new List<Action>();
        public override Task ResumeInternalAsync()
        {
            ResumeInternal();
            return Task.FromResult<object>(null);
        }

        public override void ResumeInternal()
        {
            foreach (var emit in _queuedEmits)
            {
                emit();
            }

            _queuedEmits.Clear();
        }
    }
}