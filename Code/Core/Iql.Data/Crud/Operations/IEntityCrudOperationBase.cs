using Iql.Data.Tracking.State;
using Iql.Entities;

namespace Iql.Data.Crud.Operations
{
    public interface IEntityCrudOperationBase : IEntitySetCrudOperationBase
    {
        CompositeKey KeyBeforeSave { get; set; }
        IEntityStateBase EntityState { get; set; }
    }
}