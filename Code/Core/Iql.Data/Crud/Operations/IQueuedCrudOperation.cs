namespace Iql.Data.Crud.Operations
{
    public interface IQueuedCrudOperation : IQueuedOperation
    {
        SaveChangesOperation SaveChangesOperation { get; }
    }
}