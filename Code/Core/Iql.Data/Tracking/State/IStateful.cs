using Iql.Data.Context;

namespace Iql.Data.Tracking.State
{
    public interface IStateful
    {
        IOperationEventsBase StatefulSaveEvents { get; }
        IOperationEventsBase SaveEvents { get; }
        IOperationEventsBase AbandonEvents { get; }
        void AbandonChanges();
    }
}