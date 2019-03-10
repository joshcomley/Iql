namespace Iql.Forms.Syncing
{
    public abstract class IqlSyncEvent : IIqlSyncEvent
    {
        public IqlSyncService SyncService { get; }

        public IqlSyncEvent(IqlSyncService syncService)
        {
            SyncService = syncService;
        }
    }
}