using Iql.Data.Crud;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Context
{
    public class DataContextEventsManager
    {
        private static DataContextEventsManager _globalEvents;
        public static DataContextEventsManager GlobalEvents => _globalEvents = _globalEvents ?? new DataContextEventsManager().IsGlobal();

        private DataContextEventsManager IsGlobal()
        {
            this._isGlobal = true;
            return this;
        }

        private ISaveEvents<IQueuedAddEntityOperation, IAddEntityResult> _addEvents;
        private ISaveEvents<IQueuedUpdateEntityOperation, IUpdateEntityResult> _updateEvents;
        private ISaveEvents<IQueuedDeleteEntityOperation, IDeleteEntityResult> _deleteEvents;
        private ISaveEvents<IQueuedCrudOperation, IEntityCrudResult> _entityEvents;
        private ISaveEvents<SaveChangesOperation, SaveChangesResult> _contextEvents;
        private bool _isGlobal;

        public ISaveEvents<IQueuedAddEntityOperation, IAddEntityResult> AddEvents => _addEvents = _addEvents ?? new SaveEvents<IQueuedAddEntityOperation, IAddEntityResult>(_isGlobal ? null : GlobalEvents.AddEvents);
        public ISaveEvents<IQueuedUpdateEntityOperation, IUpdateEntityResult> UpdateEvents => _updateEvents = _updateEvents ?? new SaveEvents<IQueuedUpdateEntityOperation, IUpdateEntityResult>(_isGlobal ? null : GlobalEvents.UpdateEvents);
        public ISaveEvents<IQueuedDeleteEntityOperation, IDeleteEntityResult> DeleteEvents => _deleteEvents = _deleteEvents ?? new SaveEvents<IQueuedDeleteEntityOperation, IDeleteEntityResult>(_isGlobal ? null : GlobalEvents.DeleteEvents);
        public ISaveEvents<IQueuedCrudOperation, IEntityCrudResult> EntityEvents => _entityEvents = _entityEvents ?? new SaveEvents<IQueuedCrudOperation, IEntityCrudResult>(_isGlobal ? null : GlobalEvents.EntityEvents);
        public ISaveEvents<SaveChangesOperation, SaveChangesResult> ContextEvents => _contextEvents = _contextEvents ?? new SaveEvents<SaveChangesOperation, SaveChangesResult>(_isGlobal ? null : GlobalEvents.ContextEvents);
    }
}