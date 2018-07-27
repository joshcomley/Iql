using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Crud.Operations.Queued
{
    public class QueuedUpdateEntityOperation<T> : QueuedOperation<UpdateEntityOperation<T>, UpdateEntityResult<T>>
    {
        public QueuedUpdateEntityOperation(UpdateEntityOperation<T> operation, UpdateEntityResult<T> result = null) :
            base(QueuedOperationType.Update, operation, result ?? new UpdateEntityResult<T>(true, operation))
        {
        }
    }
}