namespace Iql.Data.Crud.Operations
{
    public abstract class QueuedOperation<TOperation,
        TResult> : IQueuedOperation
        where TOperation : IEntitySetCrudOperationBase
        where TResult : ICrudResult
    {
        protected QueuedOperation(
            QueuedOperationKind kind, 
            TOperation operation, 
            TResult result)
        {
            Kind = kind;
            Operation = operation;
            Result = result;
        }

        public TOperation Operation { get; set; }
        public QueuedOperationKind Kind { get; }
        public TResult Result { get; }
        ICrudResult IQueuedOperation.Result => Result;
        ICrudOperation IQueuedOperation.Operation => Operation;
    }
}