using Iql.Queryable.Data.Context;

namespace Iql.Queryable.Data.Crud.Operations
{
    public class AddEntityOperation<T> : EntityCrudOperation<T>
    {
        public AddEntityOperation(T entity, IDataContext dataContext)
            : base(OperationType.Add, entity, dataContext)
        {
        }
    }
}