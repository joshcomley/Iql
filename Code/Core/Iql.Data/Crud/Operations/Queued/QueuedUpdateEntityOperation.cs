using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Crud.Operations.Queued
{
    public class QueuedUpdateEntityOperation<T> : QueuedOperation<UpdateEntityOperation<T>, UpdateEntityResult<T>>, IQueuedUpdateEntityOperation
    {
        public QueuedUpdateEntityOperation(UpdateEntityOperation<T> operation, UpdateEntityResult<T> result = null) :
            base(QueuedOperationKind.Update, operation, result ?? new UpdateEntityResult<T>(true, operation))
        {
        }

        IUpdateEntityOperation IQueuedUpdateEntityOperation.Operation => Operation;
        IUpdateEntityResult IQueuedUpdateEntityOperation.Result => Result;
    }
}