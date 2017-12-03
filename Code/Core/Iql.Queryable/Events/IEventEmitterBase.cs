using System;

namespace Iql.Queryable.Events
{
    public interface IEventEmitterBase
    {
        void Emit(object propertyChangeEvent);
        int Subscribe(Action<object> propertyChangeEvent);
        void Unsubscribe(int subscription);
    }
}