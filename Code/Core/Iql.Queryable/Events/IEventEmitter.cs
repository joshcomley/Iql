using System;
using System.Threading.Tasks;

namespace Iql.Queryable.Events
{
    public interface IAsyncEventManager<TEvent> : IAsyncEventEmitter<TEvent>, IAsyncEventSubscriber<TEvent>
    {

    }
    public interface IAsyncEventEmitter<TEvent>
    {
        Task<TEvent> EmitAsync(Func<TEvent> propertyChangeEvent, Func<TEvent, Task> afterEventAsync = null);
    }

    public interface IAsyncEventSubscriber<out TEvent> : IAsyncEventSubscriberBase
    {
        EventSubscription SubscribeAsync(Func<TEvent, Task> propertyChangeEvent);
    }

    public interface IAsyncEventSubscriberBase: IEventUnsubcriber
    {
        EventSubscription SubscribeAsync(Func<object, Task> propertyChangeEvent);
    }

    public interface IEventManager<TEvent> : IEventEmitter<TEvent>, IEventSubscriber<TEvent>
    {
        
    }
    public interface IEventEmitter<TEvent>
    {
        TEvent Emit(Func<TEvent> propertyChangeEvent, Action<TEvent> afterEvent = null);
    }

    public interface IEventSubscriber<out TEvent> : IEventSubscriberBase
    {
        EventSubscription Subscribe(Action<TEvent> propertyChangeEvent);
    }

    public interface IEventSubscriberBase: IEventUnsubcriber
    {
        EventSubscription Subscribe(Action<object> propertyChangeEvent);
    }

    public interface IEventUnsubcriber
    {
        void Unsubscribe(int subscription);
    }
}