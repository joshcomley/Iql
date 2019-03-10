namespace Iql.Forms.Syncing
{
    public interface IIqlSyncEvent
    {
        IqlSyncService SyncService { get; }
    }
}