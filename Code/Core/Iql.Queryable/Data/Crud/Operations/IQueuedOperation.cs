namespace Iql.Queryable.Data.Crud.Operations
{
    public interface IQueuedOperation
    {
        IEntitySetCrudOperationBase Operation { get; }
        QueuedOperationType Type { get; }
    }
}