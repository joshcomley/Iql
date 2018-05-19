using System;

namespace Iql.Entities.Events
{
    public interface IEventEmitterBase
    {
        void Emit(object propertyChangeEvent);
        int Subscribe(Action<object> propertyChangeEvent);
        void Unsubscribe(int subscription);
    }
}