using Iql.Data.Context;
using Iql.Data.Tracking.State;
using Iql.Entities;

namespace Iql.Data.Crud.Operations
{
    public class UpdateEntityOperation<T> : EntityCrudOperation<T>, IUpdateEntityOperation
    {
        public IProperty[] Properties { get; }
        public IEntityStateBase EntityState { get; set; }

        public IPropertyState[] GetChangedProperties()
        {
            return EntityState.GetChangedProperties(Properties);
        }

        public UpdateEntityOperation(T entity, IDataContext dataContext, IEntityStateBase entityState = null, IProperty[] properties = null)
            : base(OperationType.Update, entity, dataContext)
        {
            Properties = properties;
            EntityState = entityState ?? dataContext.GetEntityState(entity
#if TypeScript
            , typeof(T)
#endif
                );
        }
    }
}