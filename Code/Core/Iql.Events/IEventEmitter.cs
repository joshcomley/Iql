using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Iql.Events
{
    public interface IAsyncEventManager<TEvent> : IAsyncEventEmitter<TEvent>, IAsyncEventSubscriber<TEvent>
    {

    }
    public interface IAsyncEventEmitter<TEvent> : IEventEmitterCommon<TEvent>
    {
        Task<TEvent> EmitAsync(Func<TEvent> eventObjectFactory, Func<TEvent, Task> afterEventAsync = null, IEnumerable<EventSubscription> subscriptions = null);
    }

    public interface IAsyncEventSubscriber<out TEvent> : IAsyncEventSubscriberBase
    {
        EventSubscription SubscribeAsync(Func<TEvent, Task> action);
    }

    public interface IAsyncEventSubscriberBase : IEventUnsubscriber
    {
        EventSubscription SubscribeAsync(Func<object, Task> propertyChangeEvent);
    }

    public interface IEventManager<TEvent> : IEventEmitter<TEvent>, IEventSubscriber<TEvent>
    {
        BackfireMode BackfireMode { get; set; }
    }

    public interface IEventEmitterCommon<TEvent>
    {
        List<TEvent> Backfires { get; }
    }

    public interface IEventEmitter<TEvent> : IEventEmitterCommon<TEvent>
    {
        TEvent Emit(Func<TEvent> eventObjectFactory, Action<TEvent> afterEvent = null, IEnumerable<EventSubscription> subscriptions = null);
    }

    public interface IEventSubscriber<out TEvent> : IEventSubscriberBase
    {
        EventSubscription Subscribe(Action<TEvent> action);
    }

    public interface IEventSubscriberSubscriber
    {
        EventEmitter<EventSubscription> OnSubscribe { get; }
    }

    public interface IEventSubscriberBase : IEventUnsubscriber, IEventSubscriberSubscriber
    {
        int SubscriptionCount { get; }
        EventSubscription Subscribe(Action<object> propertyChangeEvent);
    }

    public interface IEventUnsubscriber : IDisposable
    {
        EventEmitter<EventSubscription> OnUnsubscribe { get; }
        void ClearBackfires();
        bool HasBackfires { get; }
        int BackfireCount { get; }
        void Unsubscribe(int subscription);
        void UnsubscribeAll();
    }
}