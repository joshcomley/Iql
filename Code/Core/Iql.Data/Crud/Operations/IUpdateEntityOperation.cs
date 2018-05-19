using Iql.Queryable.Data.Tracking.State;

namespace Iql.Queryable.Data.Crud.Operations
{
    public interface IUpdateEntityOperation : IEntityCrudOperationBase
    {
        IEntityStateBase EntityState { get; }
    }
}