using Iql.Data.Tracking.State;

namespace Iql.Data.Crud.Operations
{
    public interface IEntityCrudOperation<T> : IEntityCrudOperationBase
        where T : class
    {
        new IEntityState<T> EntityState { get; set; }
    }
}