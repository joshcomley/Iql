using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.Data.Crud.Operations
{
    public class DeleteEntityOperation<T> : EntityCrudOperation<T>
    {
        public CompositeKey Key { get; set; }
        public DeleteEntityOperation(CompositeKey key, T entity, IDataContext dataContext)
            : base(OperationType.Delete, entity, dataContext)
        {
            Key = key;
        }
    }
}