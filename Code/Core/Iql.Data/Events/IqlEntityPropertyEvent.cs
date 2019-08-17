using Iql.Data.Context;
using Iql.Data.Tracking.State;
using Iql.Entities;

namespace Iql.Data.Events
{
    public class IqlEntityPropertyEvent<T> : IqlEntityEvent<T>, IEntityPropertyEvent 
        where T : class
    {
        public IqlEntityPropertyEvent(
            IPropertyContainer property,
            IEntityState<T> entity,
            IDataContext dataContext
            ) : base(entity, dataContext)
        {
            Property = property;
        }

        public IPropertyContainer Property { get; }
    }
}