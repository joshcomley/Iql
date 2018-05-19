using Iql.Data.Tracking.State;

namespace Iql.Data.Crud.Operations
{
    public interface IUpdateEntityOperation : IEntityCrudOperationBase
    {
        IEntityStateBase EntityState { get; }
    }
}