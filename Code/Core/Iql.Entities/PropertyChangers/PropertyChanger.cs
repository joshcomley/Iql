using System;
using Iql.Entities.Events;
using Iql.Events;

namespace Iql.Entities.PropertyChangers
{
    public class PropertyChanger
    {
        public void ChangeProperty<TEntity, TValue>(
            TEntity entity,
            string propertyName, 
            TValue oldValue, 
            TValue newValue, 
            EventEmitter<IPropertyChangeEvent> propertyChangingEvent, 
            EventEmitter<IPropertyChangeEvent> propertyChangedEvent,
            Action<TValue> committer)
        where TEntity : class
        {
            var changedSet = false;
            var changed = false;
            if (propertyChangingEvent != null)
            {
                changed = !AreEquivalent(newValue, oldValue);
                changedSet = true;
                if (changed)
                {
                    propertyChangingEvent.Emit(() => new PropertyChangeEvent<TEntity>(propertyName, entity, oldValue, newValue));
                }
            }

            committer(newValue);
            if (propertyChangedEvent != null)
            {
                if (changedSet ? changed : !AreEquivalent(newValue, oldValue))
                {
                    propertyChangedEvent.Emit(() => new PropertyChangeEvent<TEntity>(propertyName, entity, oldValue, newValue));
                }
            }
        }

        public virtual bool AreEquivalent<TValue>(TValue newValue, TValue oldValue)
        {
            return Equals(newValue, oldValue);
        }

        public virtual TValue CloneValue<TValue>(TValue value)
        {
            return value;
        }

        //public virtual void ApplyTo<TValue>(TValue source, TValue applyTo)
        //{

        //}
    }
}