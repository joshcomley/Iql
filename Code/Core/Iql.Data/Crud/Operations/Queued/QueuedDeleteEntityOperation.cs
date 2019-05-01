using Iql.Data.Crud.Operations.Results;
using Iql.Entities;

namespace Iql.Data.Crud.Operations.Queued
{
    public class QueuedDeleteEntityOperation<T> : QueuedCrudOperation<DeleteEntityOperation<T>, DeleteEntityResult<T>>, IQueuedDeleteEntityOperation
    {
        public CompositeKey Key => Operation.Key ?? Operation.DataContext.GetEntityState(Operation.Entity).CurrentKey;
        public QueuedDeleteEntityOperation(
            SaveChangesOperation saveChangesOperation,
            DeleteEntityOperation<T> operation,
            DeleteEntityResult<T> result) : base(saveChangesOperation, QueuedOperationKind.Delete, operation, result ?? new DeleteEntityResult<T>(false, operation))
        {
        }

        IDeleteEntityOperation IQueuedDeleteEntityOperation.Operation => Operation;
        IDeleteEntityResult IQueuedDeleteEntityOperation.Result => Result;
    }
}