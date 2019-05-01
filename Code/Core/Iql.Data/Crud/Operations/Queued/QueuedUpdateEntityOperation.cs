using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Crud.Operations.Queued
{
    public class QueuedUpdateEntityOperation<T> : QueuedCrudOperation<UpdateEntityOperation<T>, UpdateEntityResult<T>>, IQueuedUpdateEntityOperation
    {
        public QueuedUpdateEntityOperation(
            SaveChangesOperation saveChangesOperation,
            UpdateEntityOperation<T> operation, UpdateEntityResult<T> result = null) :
            base(saveChangesOperation, QueuedOperationKind.Update, operation, result ?? new UpdateEntityResult<T>(true, operation))
        {
        }

        IUpdateEntityOperation IQueuedUpdateEntityOperation.Operation => Operation;
        IUpdateEntityResult IQueuedUpdateEntityOperation.Result => Result;
    }
}