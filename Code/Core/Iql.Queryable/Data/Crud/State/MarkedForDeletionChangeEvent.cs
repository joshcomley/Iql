using System;

namespace Iql.Queryable.Data.Crud.State
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