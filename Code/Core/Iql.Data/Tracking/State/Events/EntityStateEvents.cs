using Iql.Data.Context;
using Iql.Data.Events;

namespace Iql.Data.Tracking.State.Events
{
    public class EntityStateEvents<T> : IEntityStateEvents<T>
        where T : class
    {
        public IOperationEvents<IEntityEvent<T>, IEntityEvent<T>> Save { get; }
        IOperationEventsBase IEntityStateEventsBase.Save => Save;
        public IOperationEvents<IEntityEvent<T>, IEntityEvent<T>> Abandon { get; }
        IOperationEventsBase IEntityStateEventsBase.Abandon => Abandon;
    }
}