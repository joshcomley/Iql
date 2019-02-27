using Iql.Data.Crud.Operations.Results;
using Iql.Entities;

namespace Iql.Data.Crud.Operations.Queued
{
    public class QueuedDeleteEntityOperation<T> : QueuedOperation<DeleteEntityOperation<T>, DeleteEntityResult<T>>
    {
        public CompositeKey Key => Operation.Key ?? Operation.DataContext.GetEntityState(Operation.Entity).CurrentKey;
        public QueuedDeleteEntityOperation(DeleteEntityOperation<T> operation,
            DeleteEntityResult<T> result) : base(QueuedOperationKind.Delete, operation, result ?? new DeleteEntityResult<T>(false, operation))
        {
        }
    }
}