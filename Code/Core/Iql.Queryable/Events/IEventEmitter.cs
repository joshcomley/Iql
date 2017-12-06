using System;

namespace Iql.Queryable.Events
{
    public interface IEventManager<TEvent> : IEventEmitter<TEvent>, IEventSubscriber<TEvent>
    {
        
    }
    public interface IEventEmitter<in TEvent>
    {
        void Emit(TEvent propertyChangeEvent);
    }

    public interface IEventSubscriber<out TEvent> : IEventSubscriberBase
    {
        int Subscribe(Action<TEvent> propertyChangeEvent);
    }

    public interface IEventSubscriberBase
    {
        int Subscribe(Action<object> propertyChangeEvent);
        void Unsubscribe(int subscription);
    }
}