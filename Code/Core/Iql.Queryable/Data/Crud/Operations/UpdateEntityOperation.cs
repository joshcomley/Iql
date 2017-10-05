namespace Iql.Queryable.Data.Crud.Operations
{
    public class UpdateEntityOperation<T> : EntityCrudOperation<T>
    {
        public PropertyChange[] ChangedProperties { get; }

        public UpdateEntityOperation(T entity, IDataContext dataContext, params PropertyChange[] changedProperties)
            : base(OperationType.Update, entity, dataContext)
        {
            ChangedProperties = changedProperties;
        }
    }
}