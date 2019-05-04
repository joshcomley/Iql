using Iql.Data.Crud;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;
using Iql.Entities;

namespace Iql.Data.Context
{
    public class DataContextEvents
    {
        private static DataContextEvents _globalEvents;
        private ISaveEvents<IQueuedAddEntityOperation, IAddEntityResult> _addEvents;
        private DataContextSaveEvents _contextEvents;
        private ISaveEvents<IQueuedDeleteEntityOperation, IDeleteEntityResult> _deleteEvents;
        private ISaveEvents<IQueuedCrudOperation, IEntityCrudResult> _entityEvents;

        private bool _isGlobal;
        private ISaveEvents<IQueuedUpdateEntityOperation, IUpdateEntityResult> _updateEvents;

        public static DataContextEvents GlobalEvents =>
            _globalEvents = _globalEvents ?? new DataContextEvents().IsGlobal();

        public ISaveEvents<IQueuedAddEntityOperation, IAddEntityResult> AddEvents => _addEvents =
            _addEvents ??
            new SaveEvents<IQueuedAddEntityOperation, IAddEntityResult>(_isGlobal ? null : GlobalEvents.AddEvents);

        public ISaveEvents<IQueuedUpdateEntityOperation, IUpdateEntityResult> UpdateEvents => _updateEvents =
            _updateEvents ??
            new SaveEvents<IQueuedUpdateEntityOperation, IUpdateEntityResult>(_isGlobal
                ? null
                : GlobalEvents.UpdateEvents);

        public ISaveEvents<IQueuedDeleteEntityOperation, IDeleteEntityResult> DeleteEvents => _deleteEvents =
            _deleteEvents ??
            new SaveEvents<IQueuedDeleteEntityOperation, IDeleteEntityResult>(_isGlobal
                ? null
                : GlobalEvents.DeleteEvents);

        public ISaveEvents<IQueuedCrudOperation, IEntityCrudResult> EntityEvents => _entityEvents =
            _entityEvents ??
            new SaveEvents<IQueuedCrudOperation, IEntityCrudResult>(_isGlobal ? null : GlobalEvents.EntityEvents);

        public DataContextSaveEvents ContextEvents => _contextEvents =
            _contextEvents ?? new DataContextSaveEvents(_isGlobal ? null : GlobalEvents.ContextEvents);

        private DataContextEvents IsGlobal()
        {
            _isGlobal = true;
            return this;
        }
    }
}