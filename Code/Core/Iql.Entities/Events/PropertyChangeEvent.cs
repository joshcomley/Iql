using System;

namespace Iql.Data.Configuration.Events
{
    public class PropertyChangeEvent<T> : IPropertyChangeEvent
    {
        public string PropertyName { get; }
        public object Entity { get; }
        public Type EntityType => typeof(T);
        public object OldValue { get; }
        public object NewValue { get; }

        public PropertyChangeEvent(string propertyName, object entity, object oldValue, object newValue)
        {
            PropertyName = propertyName;
            Entity = entity;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}