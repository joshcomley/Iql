using Iql.Data.Crud.Operations.Results;
using Iql.Data.Tracking.State;

namespace Iql.Data.Crud.Operations.Queued
{
    public class QueuedAddEntityOperation<T> : QueuedCrudOperation<AddEntityOperation<T>, AddEntityResult<T>>, IQueuedAddEntityOperation
        where T : class
    {
        public QueuedAddEntityOperation(
            SaveChangesOperation saveChangesOperation,
            AddEntityOperation<T> operation, AddEntityResult<T> result) : base(
            saveChangesOperation,
            QueuedOperationKind.Add,
            operation,
            result ?? new AddEntityResult<T>(false, operation))
        {
        }

        ICrudOperation IQueuedOperation.Operation => Operation;
        IEntitySetCrudOperationBase IQueuedCrudOperation.Operation => Operation;
        IEntityCrudOperationBase IQueuedEntityCrudOperation.Operation => Operation;
        IEntityStateBase IQueuedEntityCrudOperation.EntityState => Operation.EntityState;
        IAddEntityOperation IQueuedAddEntityOperation.Operation => Operation;
        IAddEntityResult IQueuedAddEntityOperation.Result => Result;
    }
}