using Iql.Data.Context;

namespace Iql.Data.Tracking.State.Events
{
    public interface IEntityStateEventsBase
    {
        IOperationEventsBase Save { get; }
        IOperationEventsBase Abandon { get; }
    }
}