using System;

namespace Iql.Queryable.Events
{
    public class PropertyChangeEvent<T> : IPropertyChangeEvent
    {
        public string PropertyName { get; }
        public object Entity { get; }
        public Type EntityType => typeof(T);

        public PropertyChangeEvent(string propertyName, object entity)
        {
            PropertyName = propertyName;
            Entity = entity;
        }
    }
}