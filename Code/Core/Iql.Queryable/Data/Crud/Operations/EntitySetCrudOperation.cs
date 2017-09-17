using System;

namespace Iql.Queryable.Data.Crud.Operations
{
    public class EntitySetCrudOperation<T> : CrudOperation, IEntitySetCrudOperationBase
    {
        public EntitySetCrudOperation(OperationType type, IDataContext dataContext)
            : base(type, dataContext)
        {
            EntityType = typeof(T);
        }

        public Type EntityType { get; set; }
    }
}