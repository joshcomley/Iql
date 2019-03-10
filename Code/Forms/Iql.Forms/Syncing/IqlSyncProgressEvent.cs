using Iql.Queryable;

namespace Iql.Forms.Syncing
{
    public class IqlSyncProgressEvent : ProgressNotifierEvent, IIqlSyncEvent
    {
        public IqlSyncService SyncService { get; }

        public IqlSyncProgressEvent(IqlSyncService syncService, double progress, bool isFinalNotification):base(progress, isFinalNotification)
        {
            SyncService = syncService;
        }
    }
}