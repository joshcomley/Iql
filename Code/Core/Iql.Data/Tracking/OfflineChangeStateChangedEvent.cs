using Iql.Data.Context;

namespace Iql.Data.Tracking
{
    public class OfflineChangeStateChangedEvent
    {
        public IDataContext DataContext { get; }
        public DataTracker Tracker { get; }
        public bool HasChanges { get; }

        public OfflineChangeStateChangedEvent(IDataContext dataContext, DataTracker tracker, bool hasChanges)
        {
            DataContext = dataContext;
            Tracker = tracker;
            HasChanges = hasChanges;
        }
    }
}