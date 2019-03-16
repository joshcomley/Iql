using Iql.Data.Context;
using Iql.Data.Tracking.State;
using Iql.Entities;

namespace Iql.Data.Crud.Operations
{
    public class DeleteEntityOperation<T> : EntityCrudOperation<T>, IDeleteEntityOperation
    {
        public IEntityState<T> EntityState => (IEntityState<T>)DataContext.GetEntityState(Entity);
        public CompositeKey Key { get; set; }
        public DeleteEntityOperation(CompositeKey key, T entity, IDataContext dataContext)
            : base(IqlOperationKind.Delete, entity, dataContext)
        {
            Key = key;
        }

        IEntityStateBase IDeleteEntityOperation.EntityState => EntityState;
    }
}