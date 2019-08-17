using Iql.Data.Tracking.State;

namespace Iql.Data.Crud.Operations
{
    public interface IQueuedEntityCrudOperation : IQueuedCrudOperation
    {
        new IEntityCrudOperationBase Operation { get; }
        IEntityStateBase EntityState { get; }
    }
}