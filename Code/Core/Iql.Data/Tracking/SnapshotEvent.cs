using Iql.Data.Context;

namespace Iql.Data.Tracking
{
    public class SnapshotReplacedEvent : SnapshotEvent
    {
        public TrackerSnapshot ReplacedSnapshot { get; }

        public SnapshotReplacedEvent(DataTracker dataTracker, TrackerSnapshot snapshot, TrackerSnapshot replacedSnapshot) : base(dataTracker, snapshot)
        {
            ReplacedSnapshot = replacedSnapshot;
        }
    }

    public class SnapshotEvent
    {
        public DataTracker DataTracker { get; }
        public TrackerSnapshot Snapshot { get; }

        public SnapshotEvent(DataTracker dataTracker, TrackerSnapshot snapshot)
        {
            DataTracker = dataTracker;
            Snapshot = snapshot;
        }
    }
}