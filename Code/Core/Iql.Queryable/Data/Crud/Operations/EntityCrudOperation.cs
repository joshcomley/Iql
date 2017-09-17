namespace Iql.Queryable.Data.Crud.Operations
{
    public class EntityCrudOperation<T> : EntitySetCrudOperation<T>, IEntityCrudOperation<T>
    {
        public EntityCrudOperation(OperationType type, T entity, IDataContext dataContext)
            : base(type, dataContext)
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