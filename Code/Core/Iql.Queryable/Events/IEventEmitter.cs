using System;
using System.Threading.Tasks;

namespace Iql.Queryable.Events
{
    public interface IAsyncEventManager<TEvent> : IAsyncEventEmitter<TEvent>, IAsyncEventSubscriber<TEvent>
    {

    }
    public interface IAsyncEventEmitter<in TEvent>
    {
        Task EmitAsync(Func<TEvent> propertyChangeEvent);
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
    public interface IEventEmitter<in TEvent>
    {
        void Emit(Func<TEvent> propertyChangeEvent);
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