using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.Data.Crud.Operations.Queued
{
    public class QueuedDeleteEntityOperation<T> : QueuedOperation<DeleteEntityOperation<T>, DeleteEntityResult<T>>
    {
        public CompositeKey Key => Operation.Key ?? Operation.DataContext.GetEntityState(Operation.Entity).CurrentKey;
        public QueuedDeleteEntityOperation(DeleteEntityOperation<T> operation,
            DeleteEntityResult<T> result) : base(QueuedOperationType.Delete, operation, result ?? new DeleteEntityResult<T>(false, operation))
        {
        }
    }
}