namespace Iql.Queryable.Data.Crud.Operations
{
    public class UpdateEntityOperation<T> : EntityCrudOperation<T>
    {
        public UpdateEntityOperation(T entity, IDataContext dataContext)
            : base(OperationType.Update, entity, dataContext)
        {
        }
    }
}