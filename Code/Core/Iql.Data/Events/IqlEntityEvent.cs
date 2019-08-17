using Iql.Data.Context;
using Iql.Data.Tracking.State;

namespace Iql.Data.Events
{
    public class IqlEntityEvent<T> : IEntityEvent<T>
        where T : class
    {
        public IDataContext DataContext { get; }
        public IEntityState<T> Entity { get; }
        IEntityStateBase IEntityEventBase.Entity => Entity;

        public IqlEntityEvent(IEntityState<T> entity, IDataContext dataContext)
        {
            Entity = entity;
            DataContext = dataContext;
        }
    }
}