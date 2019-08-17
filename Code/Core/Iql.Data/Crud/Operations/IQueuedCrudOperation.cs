using Iql.Data.Context;

namespace Iql.Data.Crud.Operations
{
    public interface IQueuedCrudOperation : IQueuedOperation
    {
        new IEntitySetCrudOperationBase Operation { get; }
        IOperationEvents<IQueuedCrudOperation, IEntityCrudResult> Events { get; }
        SaveChangesOperation SaveChangesOperation { get; }
    }
}