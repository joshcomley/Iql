using Iql.Data.Tracking.State;
using Iql.Entities;

namespace Iql.Data.Crud.Operations
{
    public interface IDeleteEntityOperation : IEntityCrudOperationBase
    {
        IEntityStateBase EntityState { get; }
        CompositeKey Key { get; }
    }
}