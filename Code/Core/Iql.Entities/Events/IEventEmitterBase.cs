using System;

namespace Iql.Data.Configuration.Events
{
    public interface IEventEmitterBase
    {
        void Emit(object propertyChangeEvent);
        int Subscribe(Action<object> propertyChangeEvent);
        void Unsubscribe(int subscription);
    }
}