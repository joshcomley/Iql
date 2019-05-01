using Iql.Data.Context;

namespace Iql.Data.Crud.Operations
{
    public class QueuedCrudOperation<TOperation,
        TResult> : QueuedOperation<TOperation, TResult>, IQueuedCrudOperation
        where TOperation : IEntitySetCrudOperationBase
        where TResult : ICrudResult
    {
        private ISaveEvents<IQueuedCrudOperation, IEntityCrudResult> _events;

        public QueuedCrudOperation(SaveChangesOperation saveChangesOperation, QueuedOperationKind kind, TOperation operation, TResult result) : base(kind, operation, result)
        {
            SaveChangesOperation = saveChangesOperation;
        }

        public ISaveEvents<IQueuedCrudOperation, IEntityCrudResult> Events => _events = _events ?? new SaveEvents<IQueuedCrudOperation, IEntityCrudResult>();

        public SaveChangesOperation SaveChangesOperation { get; }

        internal QueuedCrudOperation<TOperation, TResult> ReplaceEventsWith(ISaveEvents<IQueuedCrudOperation, IEntityCrudResult> events)
        {
            _events = events;
            return this;
        }
    }
}