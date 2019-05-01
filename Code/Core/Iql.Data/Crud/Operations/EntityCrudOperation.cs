using Iql.Data.Context;
using Iql.Data.Tracking.State;

namespace Iql.Data.Crud.Operations
{
    public class EntityCrudOperation<T> : EntitySetCrudOperation<T>, IEntityCrudOperation<T>
    {
        public EntityCrudOperation(IqlOperationKind kind, T entity, IDataContext dataContext, IEntityState<T> entityState = null)
            : base(kind, dataContext)
        {
            Entity = entity;
            EntityState = entityState ?? (EntityState<T>)dataContext?.GetEntityState(entity, typeof(T));
            KeyBeforeSave = EntityState?.KeyBeforeChanges();
        }

        object IEntityCrudOperationBase.Entity
        {
            get => Entity;
            set => Entity = (T)value;
        }

        public T Entity { get; set; }
    }
}