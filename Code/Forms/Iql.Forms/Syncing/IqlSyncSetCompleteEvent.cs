using Iql.Entities;

namespace Iql.Forms.Syncing
{
    public class IqlSyncSetCompleteEvent
    {
        public IqlSyncService SyncService { get; }
        public IEntityConfiguration EntityConfiguration { get; }
        public int Count { get;  }

        public IqlSyncSetCompleteEvent(IqlSyncService syncService, IEntityConfiguration entityConfiguration, int count)
        {
            SyncService = syncService;
            EntityConfiguration = entityConfiguration;
            Count = count;
        }
    }
}