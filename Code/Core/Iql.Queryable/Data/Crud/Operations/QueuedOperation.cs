namespace Iql.Queryable.Data.Crud.Operations
{
    public class QueuedOperation<TOperation,
        TResult> : IQueuedOperation
        where TOperation : IEntitySetCrudOperationBase
        where TResult : ICrudResult
    {
        public QueuedOperation(TOperation operation, TResult result)
        {
            Operation = operation;
            Result = result;
        }

        public TOperation Operation { get; set; }
        public TResult Result { get; }
        IEntitySetCrudOperationBase IQueuedOperation.Operation => Operation;
    }
}