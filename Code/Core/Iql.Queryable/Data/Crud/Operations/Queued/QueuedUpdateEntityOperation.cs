using Iql.Queryable.Data.Crud.Operations.Results;

namespace Iql.Queryable.Data.Crud.Operations.Queued
{
    public class QueuedUpdateEntityOperation<T> : QueuedOperation<UpdateEntityOperation<T>, UpdateEntityResult<T>>
    {
        public QueuedUpdateEntityOperation(UpdateEntityOperation<T> operation, UpdateEntityResult<T> result = null) :
            base(operation, result ?? new UpdateEntityResult<T>(true, operation))
        {
        }
    }
}