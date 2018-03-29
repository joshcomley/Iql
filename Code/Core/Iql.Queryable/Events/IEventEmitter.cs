using System;

namespace Iql.Queryable.Events
{
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

    public interface IEventSubscriberBase
    {
        EventSubscription Subscribe(Action<object> propertyChangeEvent);
        void Unsubscribe(int subscription);
    }
}