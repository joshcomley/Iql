using System;
using Iql.Data.Context;
using Iql.Data.Tracking.State;
using Iql.Entities;

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
        public CompositeKey KeyBeforeSave { get; set; }
        public IEntityState<T> EntityState { get; set; }

        IEntityStateBase IEntitySetCrudOperationBase.EntityState
        {
            get { return EntityState; }
            set { EntityState = (IEntityState<T>) value; }
        }
    }
}