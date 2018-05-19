namespace Iql.Data.Crud.Operations
{
    public interface IQueuedOperation
    {
        ICrudResult Result { get; }
        IEntitySetCrudOperationBase Operation { get; }
        QueuedOperationType Type { get; }
    }
}