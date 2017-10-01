using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.Data.Crud.Operations
{
    public class UpdateEntityOperation<T> : EntityCrudOperation<T>
    {
        public IKeyProperty[] ChangedProperties { get; }

        public UpdateEntityOperation(T entity, IDataContext dataContext, params IKeyProperty[] changedProperties)
            : base(OperationType.Update, entity, dataContext)
        {
            ChangedProperties = changedProperties;
        }
    }
}