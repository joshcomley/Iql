using Iql.Data.Lists;

namespace Iql.Forms.Syncing
{
    public class IqlSyncCompleteEvent : IqlSyncStartEvent
    {
        public IqlSyncCompleteEvent(IqlSyncService syncService, IDbQueryable[] queries) : base(syncService, queries)
        {
        }
    }
}