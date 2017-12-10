using Iql.Queryable.Data.Crud.Operations.Results;

namespace Iql.Queryable.Data.Crud.Operations.Queued
{
    public class QueuedDeleteEntityOperation<T> : QueuedOperation<DeleteEntityOperation<T>, DeleteEntityResult<T>>
    {
        public QueuedDeleteEntityOperation(DeleteEntityOperation<T> operation,
            DeleteEntityResult<T> result) : base(QueuedOperationType.Delete, operation, result ?? new DeleteEntityResult<T>(false, operation))
        {
        }
    }
}