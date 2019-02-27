namespace Iql.Data.Crud.Operations
{
    public class QueuedOperation<TOperation,
        TResult> : IQueuedOperation
        where TOperation : IEntitySetCrudOperationBase
        where TResult : ICrudResult
    {
        public QueuedOperation(QueuedOperationKind kind, TOperation operation, TResult result)
        {
            Kind = kind;
            Operation = operation;
            Result = result;
        }

        public TOperation Operation { get; set; }
        public QueuedOperationKind Kind { get; }
        public TResult Result { get; }
        ICrudResult IQueuedOperation.Result => Result;
        IEntitySetCrudOperationBase IQueuedOperation.Operation => Operation;
    }
}