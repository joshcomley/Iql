using Iql.Data.Tracking.State;

namespace Iql.Data.Events
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