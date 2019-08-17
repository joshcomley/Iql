using Iql.Data.Context;
using Iql.Data.Events;

namespace Iql.Data.Tracking.State.Events
{
    public interface IEntityStateEvents<T> : IEntityStateEventsBase
        where T : class
    {
        new IOperationEvents<IEntityEvent<T>, IEntityEvent<T>> Save { get; }
        new IOperationEvents<IEntityEvent<T>, IEntityEvent<T>> Abandon { get; }
    }
}