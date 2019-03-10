namespace Iql.Forms.Syncing
{
    public class IqlIsSyncingChangeEvent
    {
        public bool IsSyncing { get; }

        public IqlIsSyncingChangeEvent(bool isSyncing)
        {
            IsSyncing = isSyncing;
        }
    }
}