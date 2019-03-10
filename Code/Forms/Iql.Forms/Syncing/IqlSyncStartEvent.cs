using Iql.Data.Lists;

namespace Iql.Forms.Syncing
{
    public class IqlSyncStartEvent : IqlSyncEvent
    {
        public IDbQueryable[] Queries { get; }

        public IqlSyncStartEvent(IqlSyncService syncService, IDbQueryable[] queries) : base(syncService)
        {
            Queries = queries;
        }
    }
}