using System;
using System.Threading.Tasks;

namespace Iql.Events
{
    public interface IAsyncEventManager<TEvent> : IAsyncEventEmitter<TEvent>, IAsyncEventSubscriber<TEvent>
    {

    }
    public interface IAsyncEventEmitter<TEvent>
    {
        Task<TEvent> EmitAsync(Func<TEvent> eventObjectFactory, Func<TEvent, Task> afterEventAsync = null);
    }

    public interface IAsyncEventSubscriber<out TEvent> : IAsyncEventSubscriberBase
    {
        EventSubscription SubscribeAsync(Func<TEvent, Task> action);
    }

    public interface IAsyncEventSubscriberBase: IEventUnsubcriber
    {
        EventSubscription SubscribeAsync(Func<object, Task> propertyChangeEvent);
    }

    public interface IEventManager<TEvent> : IEventEmitter<TEvent>, IEventSubscriber<TEvent>
    {
        BackfireMode BackfireMode { get; set; }
    }
    public interface IEventEmitter<TEvent>
    {
        TEvent Emit(Func<TEvent> eventObjectFactory, Action<TEvent> afterEvent = null);
    }

    public interface IEventSubscriber<out TEvent> : IEventSubscriberBase
    {
        EventSubscription Subscribe(Action<TEvent> action);
    }

    public interface IEventSubscriberBase: IEventUnsubcriber
    {
        EventSubscription Subscribe(Action<object> propertyChangeEvent);
    }

    public interface IEventUnsubcriber : IDisposable
    {
        void Unsubscribe(int subscription);
        void UnsubscribeAll();
    }
}