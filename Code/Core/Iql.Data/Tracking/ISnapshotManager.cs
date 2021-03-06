using Iql.Entities.Events;
using Iql.Events;

namespace Iql.Data.Tracking
{
    public interface ISnapshotManager
    {
        TrackerSnapshot CurrentSnapshot { get; }
        void ClearSnapshots();
        TrackerSnapshot AddSnapshot(bool? nullIfEmpty = null);
        bool UndoChanges(object[] entities = null, object[] properties = null);
        bool RevertToSnapshot();
        bool RemoveLastSnapshot(SnapshotRemoveKind? kind = null);
        bool RestoreNextAbandonedSnapshot();
        TrackerSnapshot ReplaceLastSnapshot();
        bool HasSnapshot { get; }
        bool HasRestorableSnapshot { get; }
        bool HasChangesSinceSnapshot { get; }
        bool HasChanges { get; }
        EventEmitter<ValueChangedEvent<bool, DataTrackerState>> HasChangesSinceSnapshotChanged { get; }
        EventEmitter<ValueChangedEvent<bool, DataTrackerState>> HasChangesChanged { get; }
        TrackerSnapshot[] Snapshots { get; }
        int SnapshotsCount { get; }
        TrackerSnapshot[] RestorableSnapshots { get; }
        int RestorableSnapshotsCount { get; }

    }
}