using System;
using Iql.Data.Context;

namespace Iql.Data.Crud.Operations
{
    public class EntitySetCrudOperation<T> : CrudOperation, IEntitySetCrudOperationBase
    {
        public EntitySetCrudOperation(IqlOperationKind kind, IDataContext dataContext)
            : base(kind, dataContext)
        {
            EntityType = typeof(T);
        }

        public Type EntityType { get; set; }
    }
}