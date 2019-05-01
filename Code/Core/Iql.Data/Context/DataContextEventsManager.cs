using Iql.Data.Crud;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Context
{
    public class DataContextEventsManager
    {
        private ISaveEvents<IQueuedAddEntityOperation, IAddEntityResult> _addEvents;
        private ISaveEvents<IQueuedUpdateEntityOperation, IUpdateEntityResult> _updateEvents;
        private ISaveEvents<IQueuedDeleteEntityOperation, IDeleteEntityResult> _deleteEvents;
        private ISaveEvents<IQueuedCrudOperation, IEntityCrudResult> _entityEvents;
        private ISaveEvents<SaveChangesOperation, SaveChangesResult> _contextEvents;

        public ISaveEvents<IQueuedAddEntityOperation, IAddEntityResult> AddEvents => _addEvents = _addEvents ?? new SaveEvents<IQueuedAddEntityOperation, IAddEntityResult>();
        public ISaveEvents<IQueuedUpdateEntityOperation, IUpdateEntityResult> UpdateEvents => _updateEvents = _updateEvents ?? new SaveEvents<IQueuedUpdateEntityOperation, IUpdateEntityResult>();
        public ISaveEvents<IQueuedDeleteEntityOperation, IDeleteEntityResult> DeleteEvents => _deleteEvents = _deleteEvents ?? new SaveEvents<IQueuedDeleteEntityOperation, IDeleteEntityResult>();
        public ISaveEvents<IQueuedCrudOperation, IEntityCrudResult> EntityEvents => _entityEvents = _entityEvents ?? new SaveEvents<IQueuedCrudOperation, IEntityCrudResult>();
        public ISaveEvents<SaveChangesOperation, SaveChangesResult> ContextEvents => _contextEvents = _contextEvents ?? new SaveEvents<SaveChangesOperation, SaveChangesResult>();
    }
}