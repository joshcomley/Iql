namespace Iql.Forms.Syncing
{
    public class IqlSyncStatusChangeEvent : IqlSyncEvent
    {
        public string Text { get; }

        public IqlSyncStatusChangeEvent(IqlSyncService syncService, string text) : base(syncService)
        {
            Text = text;
        }
    }
}