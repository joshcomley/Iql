using Iql.Data.Crud;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Results;
using Iql.Events;

namespace Iql.Data.Context
{
    public class DataContextEventsManager
    {
        private IDataContextSaveEvents<IAddEntityOperation, IAddEntityResult> _addEvents;
        private IDataContextSaveEvents<IUpdateEntityOperation, IUpdateEntityResult> _updateEvents;
        private IDataContextSaveEvents<IDeleteEntityOperation, IDeleteEntityResult> _deleteEvents;
        private EventEmitter<ICrudResult> _entityChangesSaved;
        private AsyncEventEmitter<ICrudResult> _entityChangesSavedAsync;
        private EventEmitter<SaveChangesResult> _changesSaved;
        private AsyncEventEmitter<SaveChangesResult> _changesSavedAsync;

        public IDataContextSaveEvents<IAddEntityOperation, IAddEntityResult> AddEvents => _addEvents = _addEvents ?? new DataContextSaveEvents<IAddEntityOperation, IAddEntityResult>();
        public IDataContextSaveEvents<IUpdateEntityOperation, IUpdateEntityResult> UpdateEvents => _updateEvents = _updateEvents ?? new DataContextSaveEvents<IUpdateEntityOperation, IUpdateEntityResult>();
        public IDataContextSaveEvents<IDeleteEntityOperation, IDeleteEntityResult> DeleteEvents => _deleteEvents = _deleteEvents ?? new DataContextSaveEvents<IDeleteEntityOperation, IDeleteEntityResult>();
        public EventEmitter<ICrudResult> EntityChangesSaved => _entityChangesSaved = _entityChangesSaved ?? new EventEmitter<ICrudResult>();
        public AsyncEventEmitter<ICrudResult> EntityChangesSavedAsync => _entityChangesSavedAsync = _entityChangesSavedAsync ?? new AsyncEventEmitter<ICrudResult>();
        public EventEmitter<SaveChangesResult> ChangesSaved => _changesSaved = _changesSaved ?? new EventEmitter<SaveChangesResult>();
        public AsyncEventEmitter<SaveChangesResult> ChangesSavedAsync => _changesSavedAsync = _changesSavedAsync ?? new AsyncEventEmitter<SaveChangesResult>();
    }
}