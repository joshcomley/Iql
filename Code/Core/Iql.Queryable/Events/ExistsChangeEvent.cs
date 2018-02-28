using Iql.Queryable.Data.Tracking.State;

namespace Iql.Queryable.Events
{
    public class ExistsChangeEvent
    {
        public IEntityStateBase EntityState { get; }
        public bool NewValue { get; }
        public ExistsChangeEvent(IEntityStateBase entityState, bool newValue)
        {
            EntityState = entityState;
            NewValue = newValue;
        }
    }
}