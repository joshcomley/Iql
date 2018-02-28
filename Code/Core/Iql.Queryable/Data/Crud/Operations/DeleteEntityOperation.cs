using Iql.Queryable.Data.Context;

namespace Iql.Queryable.Data.Crud.Operations
{
    public class DeleteEntityOperation<T> : EntityCrudOperation<T>
    {
        public DeleteEntityOperation(T entity, IDataContext dataContext)
            : base(OperationType.Delete, entity, dataContext)
        {
        }
    }
}