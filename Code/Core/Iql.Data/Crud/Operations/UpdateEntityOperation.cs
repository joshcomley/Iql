using Iql.Data.Context;
using Iql.Data.Tracking.State;
using Iql.Entities;

namespace Iql.Data.Crud.Operations
{
    public class UpdateEntityOperation<T> : EntityCrudOperation<T>, IUpdateEntityOperation, IEntitySaveOperation
        where T : class
    {
        public IProperty[] Properties { get; }

        public IPropertyState[] GetChangedProperties()
        {
            return EntityState.GetChangedProperties(Properties);
        }

        public UpdateEntityOperation(IEntityState<T> entityState, IDataContext dataContext, IProperty[] properties = null)
            : base(IqlOperationKind.Update, entityState, dataContext)
        {
            Properties = properties;
        }
    }
}