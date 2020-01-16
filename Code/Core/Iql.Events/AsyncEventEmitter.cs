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

        public AsyncEventEmitter<TEvent> RegisterWith(IqlEventEmitterManager manager)
        {
            manager.Register(this);
            return this;
        }

        EventSubscription IAsyncEventSubscriberBase.SubscribeAsync(Func<object, Task> action, string key = null, int? allowedCount = null, int? priority = null)
        {
            return SubscribeAsync(async e => { await action(e); },
                key,
                allowedCount,
                priority);
        }

        EventSubscription IAsyncEventSubscriberBase.SubscribeOnceAsync(Func<object, Task> action, string key = null, int? priority = null)
        {
            return SubscribeOnceAsync(async e =>
                {
                    await action(e);
                },
                key,
                priority);
        }

        public EventSubscription SubscribeOnceAsync(Func<TEvent, Task> action, string key = null, int? priority = null)
        {
            return SubscribeAsync(action, key, 1, priority);
        }

        public EventSubscription SubscribeAsync(Func<TEvent, Task> action, string key = null, int? allowedCount = null, int? priority = null)
        {
            var sub = SubscribeInternal(action, key, allowedCount, priority);
            switch (BackfireMode)
            {
                case BackfireMode.All:
                    foreach (var ev in Backfires)
                    {
#pragma warning disable 4014
                        EmitToSubscriptionsAsync(() => ev, null, new[] { sub }.ToList());
#pragma warning restore 4014
                    }
                    break;
                case BackfireMode.Last:
                    if (Backfires.Any())
                    {
                        var last = Backfires.LastOrDefault();
#pragma warning disable 4014
                        EmitToSubscriptionsAsync(() => last, null, new[] { sub }.ToList());
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

        private async Task<TEvent> EmitToSubscriptionsAsync(Func<TEvent> eventObjectFactory, Func<TEvent, Task> afterEvent, List<EventSubscription> subscriptionActions)
        {
            eventObjectFactory = BuildEventObjectFactory(eventObjectFactory);
            if (subscriptionActions != null && subscriptionActions.Count > 0)
            {
                var ev = eventObjectFactory();
                for (var i = 0; i < subscriptionActions.Count; i++)
                {
                    var subscriptionAction = subscriptionActions[i];
                    if (subscriptionAction.Paused || !subscriptionAction.IsWithinAllowedCallCount)
                    {
                        continue;
                    }

                    if (IsPaused)
                    {
                        _queuedEmits.Add(async () => await EmitToSubscriptionAsync(subscriptionAction, ev));
                    }
                    else
                    {
                        await EmitToSubscriptionAsync(subscriptionAction, ev);
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

        private static async Task EmitToSubscriptionAsync(EventSubscription subscriptionAction, TEvent ev)
        {
            try
            {
                subscriptionAction.RegisterCalled();
                await ((Func<TEvent, Task>)subscriptionAction.Action)(ev);
                if (!subscriptionAction.IsWithinAllowedCallCount &&
                    !subscriptionAction.KeepSubscriptionAfterCallCount)
                {
                    subscriptionAction.Unsubscribe();
                }
            }
            catch (Exception e)
            {
                if (EventEmitterExceptions.ShouldBeThrown(e))
                {
                    throw e;
                }
            }
        }

        async Task<object> IAsyncEventEmitterBase.EmitAsync(Func<object> eventObjectFactory = null, Func<object, Task> afterEvent = null, IEnumerable<EventSubscription> subscriptions = null)
        {
            var result = await EmitAsync(
                eventObjectFactory == null ? (Func<TEvent>)null : () => (TEvent)eventObjectFactory(),
                afterEvent == null ? (Func<TEvent, Task>)null : async _ => { await afterEvent(_); },
                subscriptions);
            return result;
        }

        private readonly List<Func<Task>> _queuedEmits = new List<Func<Task>>();
        public override void ResumeInternal()
        {
            ResumeInternalAsync();
        }
        public override async Task ResumeInternalAsync()
        {
            foreach (var emit in _queuedEmits)
            {
                await emit();
            }
            _queuedEmits.Clear();
        }

        public new AsyncEventEmitter<TEvent> Configure(Action<AsyncEventEmitter<TEvent>> onHasSubscription,
            Action<AsyncEventEmitter<TEvent>> onHasNoSubscription = null)
        {
            return (AsyncEventEmitter<TEvent>)ConfigureBase(
                onHasSubscription == null
                    ? (Action<IEventSubscriberRoot>)null
                    : _ => onHasSubscription((AsyncEventEmitter<TEvent>)_),
                onHasNoSubscription == null
                    ? (Action<IEventSubscriberRoot>)null
                    : _ => onHasNoSubscription((AsyncEventEmitter<TEvent>)_)
            );
        }
    }
}