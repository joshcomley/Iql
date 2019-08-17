using Iql.Data.Crud.Operations.Results;
using Iql.Data.Tracking.State;

namespace Iql.Data.Crud.Operations.Queued
{
    public class QueuedUpdateEntityOperation<T> : QueuedCrudOperation<UpdateEntityOperation<T>, UpdateEntityResult<T>>, IQueuedUpdateEntityOperation
        where T : class
    {
        public QueuedUpdateEntityOperation(
            SaveChangesOperation saveChangesOperation,
            UpdateEntityOperation<T> operation, UpdateEntityResult<T> result = null) :
            base(saveChangesOperation, QueuedOperationKind.Update, operation, result ?? new UpdateEntityResult<T>(true, operation))
        {
        }

        ICrudOperation IQueuedOperation.Operation => Operation;
        IEntitySetCrudOperationBase IQueuedCrudOperation.Operation => Operation;
        IEntityCrudOperationBase IQueuedEntityCrudOperation.Operation => Operation;
        IEntityStateBase IQueuedEntityCrudOperation.EntityState => Operation.EntityState;
        IUpdateEntityOperation IQueuedUpdateEntityOperation.Operation => Operation;
        IUpdateEntityResult IQueuedUpdateEntityOperation.Result => Result;
    }
}