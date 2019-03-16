using Iql.Data.Context;

namespace Iql.Data.Crud.Operations
{
    public class EntityCrudOperation<T> : EntitySetCrudOperation<T>, IEntityCrudOperation<T>
    {
        public EntityCrudOperation(IqlOperationKind kind, T entity, IDataContext dataContext)
            : base(kind, dataContext)
        {
            Entity = entity;
        }

        object IEntityCrudOperationBase.Entity
        {
            get => Entity;
            set => Entity = (T) value;
        }

        public T Entity { get; set; }
    }
}