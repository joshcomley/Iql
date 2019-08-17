using Iql.Data.Crud.Operations.Results;
using Iql.Data.Tracking.State;
using Iql.Entities;

namespace Iql.Data.Crud.Operations.Queued
{
    public class QueuedDeleteEntityOperation<T> : QueuedCrudOperation<DeleteEntityOperation<T>, DeleteEntityResult<T>>, IQueuedDeleteEntityOperation
        where T : class
    {
        public CompositeKey Key => Operation.Key ?? Operation.EntityState.LocalKey;
        public QueuedDeleteEntityOperation(
            SaveChangesOperation saveChangesOperation,
            DeleteEntityOperation<T> operation,
            DeleteEntityResult<T> result) : base(saveChangesOperation, QueuedOperationKind.Delete, operation, result ?? new DeleteEntityResult<T>(false, operation))
        {
        }

        ICrudOperation IQueuedOperation.Operation => Operation;
        IEntitySetCrudOperationBase IQueuedCrudOperation.Operation => Operation;
        IEntityCrudOperationBase IQueuedEntityCrudOperation.Operation => Operation;
        IEntityStateBase IQueuedEntityCrudOperation.EntityState => Operation.EntityState;
        IDeleteEntityOperation IQueuedDeleteEntityOperation.Operation => Operation;
        IDeleteEntityResult IQueuedDeleteEntityOperation.Result => Result;
    }
}