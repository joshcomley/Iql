namespace Iql.Queryable.Data.Crud.Operations
{
    public class QueuedOperation<TOperation,
        TResult> : IQueuedOperation
        where TOperation : IEntitySetCrudOperationBase
        where TResult : ICrudResult
    {
        public QueuedOperation(QueuedOperationType type, TOperation operation, TResult result)
        {
            Type = type;
            Operation = operation;
            Result = result;
        }

        public TOperation Operation { get; set; }
        public QueuedOperationType Type { get; }
        public TResult Result { get; }
        ICrudResult IQueuedOperation.Result => Result;
        IEntitySetCrudOperationBase IQueuedOperation.Operation => Operation;
    }
}