using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Crud.Operations.Queued
{
    public class QueuedAddEntityOperation<T> : QueuedOperation<AddEntityOperation<T>, AddEntityResult<T>>, IQueuedAddEntityOperation
    {
        public QueuedAddEntityOperation(AddEntityOperation<T> operation, AddEntityResult<T> result) : base(
            QueuedOperationKind.Add,
            operation,
            result ?? new AddEntityResult<T>(false, operation))
        {
        }

        IAddEntityOperation IQueuedAddEntityOperation.Operation => Operation;
        IAddEntityResult IQueuedAddEntityOperation.Result => Result;
    }
}