using System;

namespace Iql.Queryable.Events
{
    public interface IEventEmitter<TEvent> : IEventEmitterBase
    {
        void Emit(TEvent propertyChangeEvent);
        int Subscribe(Action<TEvent> propertyChangeEvent);
    }
}