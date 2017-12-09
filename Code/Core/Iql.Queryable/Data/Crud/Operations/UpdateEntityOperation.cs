using Iql.Queryable.Data.Crud.State;

namespace Iql.Queryable.Data.Crud.Operations
{
    public class UpdateEntityOperation<T> : EntityCrudOperation<T>, IUpdateEntityOperation
    {
        public EntityState EntityState { get; }

        public UpdateEntityOperation(T entity, IDataContext dataContext, EntityState entityState = null)
            : base(OperationType.Update, entity, dataContext)
        {
            EntityState = entityState;
        }
    }
}