using Iql.Data.Context;

namespace Iql.Data.Crud.Operations
{
    public class AddEntityOperation<T> : EntityCrudOperation<T>
    {
        public AddEntityOperation(T entity, IDataContext dataContext)
            : base(OperationType.Add, entity, dataContext)
        {
        }
    }
}