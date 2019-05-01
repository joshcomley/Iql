using Iql.Data.Context;
using Iql.Data.Tracking.State;
using Iql.Entities;

namespace Iql.Data.Crud.Operations
{
    public class UpdateEntityOperation<T> : EntityCrudOperation<T>, IUpdateEntityOperation
    {
        public IProperty[] Properties { get; }

        public IPropertyState[] GetChangedProperties()
        {
            return EntityState.GetChangedProperties(Properties);
        }

        public UpdateEntityOperation(T entity, IDataContext dataContext, IEntityState<T> entityState = null, IProperty[] properties = null)
            : base(IqlOperationKind.Update, entity, dataContext, entityState)
        {
            Properties = properties;
        }
    }
}