using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.Tracking.State;
using Iql.Entities;

namespace Iql.Data.Events
{
    public class IqlEntityPropertyEvent<T> : IqlEntityEvent<T>, IEntityPropertyEvent 
        where T : class
    {
        public IqlEntityPropertyEvent(
            ICrudOperation operation,
            IPropertyContainer property,
            IEntityState<T> entity,
            IPropertyState propertyState
            ) : base(entity, null)
        {
            Operation = operation;
            Property = property;
            PropertyState = propertyState;
        }

        public IPropertyState PropertyState { get; set; }
        public ICrudOperation Operation { get; }
        public IPropertyContainer Property { get; }
    }
}