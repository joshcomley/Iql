namespace Iql.Data.Crud.Operations
{
    public interface IQueuedOperation
    {
        ICrudOperation Operation { get; }
        ICrudResult Result { get; }
        QueuedOperationKind Kind { get; }
    }
}