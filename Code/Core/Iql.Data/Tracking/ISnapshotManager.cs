using Iql.Entities;
using Iql.Entities.Events;
using Iql.Events;

namespace Iql.Data.Tracking
{
    public interface ISnapshotManager
    {
        TrackerSnapshot CurrentSnapshot { get; }
        void ClearSnapshots();
        TrackerSnapshot AddSnapshot();
        bool UndoChanges(object[] entities = null, IProperty[] properties = null);
        bool RevertToSnapshot();
        bool RemoveLastSnapshot(SnapshotRemoveKind? kind = null);
        bool RestoreNextAbandonedSnapshot();
        bool HasSnapshot { get; }
        bool HasRestorableSnapshot { get; }
        bool HasChangesSinceSnapshot { get; }
        bool HasChanges { get; }
        EventEmitter<ValueChangedEvent<bool>> HasChangesSinceSnapshotChanged { get; }
        EventEmitter<ValueChangedEvent<bool>> HasChangesChanged { get; }
    }
}