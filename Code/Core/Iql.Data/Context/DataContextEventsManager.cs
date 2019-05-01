using Iql.Data.Crud;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Context
{
    public class DataContextEventsManager
    {
        private IDataContextSaveEvents<IQueuedAddEntityOperation, IAddEntityResult> _addEvents;
        private IDataContextSaveEvents<IQueuedUpdateEntityOperation, IUpdateEntityResult> _updateEvents;
        private IDataContextSaveEvents<IQueuedDeleteEntityOperation, IDeleteEntityResult> _deleteEvents;
        private IDataContextSaveEvents<IQueuedCrudOperation, IEntityCrudResult> _entityEvents;
        private IDataContextSaveEvents<SaveChangesOperation, SaveChangesResult> _contextEvents;

        public IDataContextSaveEvents<IQueuedAddEntityOperation, IAddEntityResult> AddEvents => _addEvents = _addEvents ?? new DataContextSaveEvents<IQueuedAddEntityOperation, IAddEntityResult>();
        public IDataContextSaveEvents<IQueuedUpdateEntityOperation, IUpdateEntityResult> UpdateEvents => _updateEvents = _updateEvents ?? new DataContextSaveEvents<IQueuedUpdateEntityOperation, IUpdateEntityResult>();
        public IDataContextSaveEvents<IQueuedDeleteEntityOperation, IDeleteEntityResult> DeleteEvents => _deleteEvents = _deleteEvents ?? new DataContextSaveEvents<IQueuedDeleteEntityOperation, IDeleteEntityResult>();
        public IDataContextSaveEvents<IQueuedCrudOperation, IEntityCrudResult> EntityEvents => _entityEvents = _entityEvents ?? new DataContextSaveEvents<IQueuedCrudOperation, IEntityCrudResult>();
        public IDataContextSaveEvents<SaveChangesOperation, SaveChangesResult> ContextEvents => _contextEvents = _contextEvents ?? new DataContextSaveEvents<SaveChangesOperation, SaveChangesResult>();
    }
}