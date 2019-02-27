namespace Iql.Data.Crud.Operations
{
    public interface IQueuedOperation
    {
        ICrudResult Result { get; }
        IEntitySetCrudOperationBase Operation { get; }
        QueuedOperationKind Kind { get; }
    }
}