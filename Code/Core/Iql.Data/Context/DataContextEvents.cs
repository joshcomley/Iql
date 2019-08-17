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
        private IOperationEvents<IQueuedAddEntityOperation, IAddEntityResult> _addEvents;
        private DataContextSaveEvents _contextEvents;
        private IOperationEvents<IQueuedDeleteEntityOperation, IDeleteEntityResult> _deleteEvents;
        private IOperationEvents<IQueuedCrudOperation, IEntityCrudResult> _entityEvents;

        private bool _isGlobal;
        private IOperationEvents<IQueuedUpdateEntityOperation, IUpdateEntityResult> _updateEvents;

        public static DataContextEvents GlobalEvents =>
            _globalEvents = _globalEvents ?? new DataContextEvents().IsGlobal();

        public IOperationEvents<IQueuedAddEntityOperation, IAddEntityResult> AddEvents => _addEvents =
            _addEvents ??
            new OperationEvents<IQueuedAddEntityOperation, IAddEntityResult>(_isGlobal ? null : GlobalEvents.AddEvents);

        public IOperationEvents<IQueuedUpdateEntityOperation, IUpdateEntityResult> UpdateEvents => _updateEvents =
            _updateEvents ??
            new OperationEvents<IQueuedUpdateEntityOperation, IUpdateEntityResult>(_isGlobal
                ? null
                : GlobalEvents.UpdateEvents);

        public IOperationEvents<IQueuedDeleteEntityOperation, IDeleteEntityResult> DeleteEvents => _deleteEvents =
            _deleteEvents ??
            new OperationEvents<IQueuedDeleteEntityOperation, IDeleteEntityResult>(_isGlobal
                ? null
                : GlobalEvents.DeleteEvents);

        public IOperationEvents<IQueuedCrudOperation, IEntityCrudResult> EntityEvents => _entityEvents =
            _entityEvents ??
            new OperationEvents<IQueuedCrudOperation, IEntityCrudResult>(_isGlobal ? null : GlobalEvents.EntityEvents);

        public DataContextSaveEvents ContextEvents => _contextEvents =
            _contextEvents ?? new DataContextSaveEvents(_isGlobal ? null : GlobalEvents.ContextEvents);

        private DataContextEvents IsGlobal()
        {
            _isGlobal = true;
            return this;
        }
    }
}