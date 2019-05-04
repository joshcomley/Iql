using Iql.Data.Context;
using Iql.Events;

namespace Iql.Data.Tracking.State
{
    public interface IAbandonChanges
    {
        EventEmitter<AbandonChangeEvent> AbandonedChanges { get; }
        EventEmitter<AbandonChangeEvent> AbandoningChanges { get; }
    }
}