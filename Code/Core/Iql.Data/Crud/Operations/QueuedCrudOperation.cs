namespace Iql.Data.Crud.Operations
{
    public class QueuedCrudOperation<TOperation,
        TResult> : QueuedOperation<TOperation, TResult>, IQueuedCrudOperation
        where TOperation : IEntitySetCrudOperationBase
        where TResult : ICrudResult
    {
        public QueuedCrudOperation(SaveChangesOperation saveChangesOperation, QueuedOperationKind kind, TOperation operation, TResult result) : base(kind, operation, result)
        {
            SaveChangesOperation = saveChangesOperation;
        }

        public SaveChangesOperation SaveChangesOperation { get; }
    }
}