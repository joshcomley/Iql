using Iql.Queryable.Data.Tracking.State;

namespace Iql.Queryable.Events
{
    public class MarkedForDeletionChangeEvent
    {
        public IEntityStateBase EntityState { get; }
        public bool NewValue { get; }
        public MarkedForDeletionChangeEvent(IEntityStateBase entityState, bool newValue)
        {
            EntityState = entityState;
            NewValue = newValue;
        }
    }
}