using Iql.Data.Context;

namespace Iql.Data.Crud.Operations
{
    public class QueuedCrudOperation<TOperation,
        TResult> : QueuedOperation<TOperation, TResult>, IQueuedCrudOperation
        where TOperation : IEntitySetCrudOperationBase
        where TResult : ICrudResult
    {
        private IOperationEvents<IQueuedCrudOperation, IEntityCrudResult> _events;

        public QueuedCrudOperation(SaveChangesOperation saveChangesOperation, QueuedOperationKind kind, TOperation operation, TResult result) : base(kind, operation, result)
        {
            SaveChangesOperation = saveChangesOperation;
        }

        public IOperationEvents<IQueuedCrudOperation, IEntityCrudResult> Events => _events = _events ?? new OperationEvents<IQueuedCrudOperation, IEntityCrudResult>();

        IEntitySetCrudOperationBase IQueuedCrudOperation.Operation => Operation;

        public SaveChangesOperation SaveChangesOperation { get; }

        internal QueuedCrudOperation<TOperation, TResult> ReplaceEventsWith(IOperationEvents<IQueuedCrudOperation, IEntityCrudResult> events)
        {
            _events = events;
            return this;
        }
    }
}