using Iql.Data.Context;

namespace Iql.Data.Crud.Operations
{
    public class AddEntityOperation<T> : EntityCrudOperation<T>, IAddEntityOperation
    {
        public AddEntityOperation(T entity, IDataContext dataContext)
            : base(IqlOperationKind.Add, entity, dataContext)
        {
        }
    }
}