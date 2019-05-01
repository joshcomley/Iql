using Iql.Data.Context;

namespace Iql.Data.Crud.Operations
{
    public interface IQueuedCrudOperation : IQueuedOperation
    {
        ISaveEvents<IQueuedCrudOperation, IEntityCrudResult> Events { get; }
        SaveChangesOperation SaveChangesOperation { get; }
    }
}