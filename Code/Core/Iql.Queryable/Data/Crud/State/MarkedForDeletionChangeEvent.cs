using System;

namespace Iql.Queryable.Data.Crud.State
{
    public class MarkedForDeletionChangeEvent
    {
        public EntityState EntityState { get; }
        public bool NewValue { get; }
        public MarkedForDeletionChangeEvent(EntityState entityState, bool newValue)
        {
            EntityState = entityState;
            NewValue = newValue;
        }
    }
}