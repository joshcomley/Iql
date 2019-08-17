using Iql.Data.Context;
using Iql.Data.Tracking.State;

namespace Iql.Data.Crud.Operations
{
    public class EntityCrudOperation<T> : EntitySetCrudOperation<T>, IEntityCrudOperation<T>
        where T : class
    {
        public EntityCrudOperation(IqlOperationKind kind, IEntityState<T> entityState, IDataContext dataContext)
            : base(kind, dataContext)
        {
            EntityState = entityState;
            KeyBeforeSave = EntityState?.KeyBeforeChanges();
            if (EntityState == null)
            {
                int a = 0;
            }
        }

        public IEntityState<T> EntityState { get; set; }
        IEntityStateBase IEntityCrudOperationBase.EntityState
        {
            get { return EntityState; }
            set { EntityState = (IEntityState<T>)value; }
        }
    }
}