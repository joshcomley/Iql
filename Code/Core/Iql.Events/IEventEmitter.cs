﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Iql.Events
{
    public interface IAsyncEventManager<TEvent> : IAsyncEventEmitter<TEvent>, IAsyncEventSubscriber<TEvent>
    {

    }
    public interface IAsyncEventEmitter<TEvent> : IEventEmitterCommon<TEvent>, IAsyncEventEmitterBase
    {
        Task<TEvent> EmitAsync(Func<TEvent> eventObjectFactory = null, Func<TEvent, Task> afterEventAsync = null, IEnumerable<EventSubscription> subscriptions = null);
    }

    public interface IAsyncEventSubscriber<out TEvent> : IAsyncEventSubscriberBase
    {
        EventSubscription SubscribeAsync(Func<TEvent, Task> action, string key = null, int? allowedCount = null, int? priority = null);
        EventSubscription SubscribeOnceAsync(Func<TEvent, Task> action, string key = null, int? priority = null);
    }

    public interface IEventEmitterRoot : IDisposable
    {
        bool IsPaused { get; }
        string Key { get; set; }
        void Pause();
        void Resume();
        Task ResumeAsync();
    }
    public interface IAsyncEventEmitterBase : IEventEmitterRoot
    {
        Task<object> EmitAsync(Func<object> eventObjectFactory = null, Func<object, Task> afterEventAsync = null, IEnumerable<EventSubscription> subscriptions = null);
    }
    public interface IEventEmitterBase : IEventEmitterRoot
    {
        object Emit(Func<object> eventObjectFactory = null, Action<object> afterEvent = null, IEnumerable<EventSubscription> subscriptions = null);
    }
    public interface IAsyncEventSubscriberBase : IEventUnsubscriber
    {
        EventSubscription SubscribeAsync(Func<object, Task> propertyChangeEvent, string key = null, int? allowedCount = null, int? priority = null);
        EventSubscription SubscribeOnceAsync(Func<object, Task> propertyChangeEvent, string key = null, int? priority = null);
    }

    public interface IEventManager<TEvent> : IEventEmitter<TEvent>, IEventSubscriber<TEvent>
    {
        BackfireMode BackfireMode { get; set; }
    }

    public interface IEventEmitterCommon<TEvent>
    {
        List<TEvent> Backfires { get; }
    }

    public interface IEventEmitter<TEvent> : IEventEmitterCommon<TEvent>, IEventEmitterBase
    {
        TEvent Emit(Func<TEvent> eventObjectFactory = null, Action<TEvent> afterEvent = null, IEnumerable<EventSubscription> subscriptions = null);
    }

    public interface IEventSubscriber<out TEvent> : IEventSubscriberBase
    {
        EventSubscription Subscribe(Action<TEvent> action, string key = null, int? allowedCount = null, int? priority = null);
        EventSubscription SubscribeOnce(Action<TEvent> action, string key = null, int? priority = null);
    }

    public interface IEventSubscriberSubscriber
    {
        EventEmitter<EventSubscription> OnSubscribe { get; }
    }

    public interface IEventSubscriberRoot : IEventUnsubscriber, IEventSubscriberSubscriber
    {
        int SubscriptionCount { get; }
        bool HasSubscription { get; }
        Action<IEventSubscriberRoot> OnHasSubscription { get; set; }
        Action<IEventSubscriberRoot> OnHasNoSubscription { get; set; }

        IEventSubscriberRoot Configure(Action<IEventSubscriberRoot> onHasSubscription,
            Action<IEventSubscriberRoot> onHasNoSubscription = null);
        EventEmitter<IEventSubscriberRoot> SubscriptionCountChanged { get; }
        EventEmitter<IEventSubscriberRoot> HasSubscriptionChanged { get; }
    }

    public interface IEventSubscriberBase : IEventSubscriberRoot
    {
        EventSubscription Subscribe(Action<object> propertyChangeEvent, string key = null, int? allowedCount = null, int? priority = null);
        EventSubscription SubscribeOnce(Action<object> propertyChangeEvent, string key = null, int? priority = null);
    }

    public interface IEventUnsubscriber : IDisposable
    {
        EventEmitter<EventSubscription> OnUnsubscribe { get; }
        void ClearBackfires();
        bool HasBackfires { get; }
        int BackfireCount { get; }
        void Unsubscribe(int subscription);
        void UnsubscribeAll(string key = null);
    }
}