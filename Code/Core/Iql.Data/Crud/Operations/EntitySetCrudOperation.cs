using System;
using Iql.Data.Context;
using Iql.Data.Tracking.State;
using Iql.Entities;

namespace Iql.Data.Crud.Operations
{
    public class EntitySetCrudOperation<T> : CrudOperation, IEntitySetCrudOperationBase
        where T : class
    {
        public EntitySetCrudOperation(IqlOperationKind kind, IDataContext dataContext)
            : base(kind, dataContext)
        {
            EntityType = typeof(T);
        }

        public Type EntityType { get; set; }
        public CompositeKey KeyBeforeSave { get; set; }
    }
}