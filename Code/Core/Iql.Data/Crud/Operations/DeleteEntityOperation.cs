using Iql.Data.Context;
using Iql.Entities;

namespace Iql.Data.Crud.Operations
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