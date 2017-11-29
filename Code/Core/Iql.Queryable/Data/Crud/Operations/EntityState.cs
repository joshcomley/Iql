using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.Data.Crud.Operations
{
    [DebuggerDisplay("{EntityType.Name}")]
    public class EntityState
    {
        public object Entity { get; }
        public Type EntityType { get; }
        public IEntityConfiguration EntityConfiguration { get; }

        public EntityState(object entity, Type entityType, IEntityConfiguration entityConfiguration)
        {
            Entity = entity;
            EntityType = entityType;
            EntityConfiguration = entityConfiguration;
            ChangedProperties = new List<PropertyChange>();
            Properties = new List<PropertyChange>();
        }

        public List<PropertyChange> ChangedProperties { get; }
        private List<PropertyChange> Properties { get; }

        public PropertyChange GetPropertyState(string name)
        {
            return Properties.SingleOrDefault(p => p.Property.Name == name);
        }

        public void SetPropertyState(string name, object oldValue, object newValue)
        {
            var propertyState = GetPropertyState(name);
            if (propertyState == null)
            {
                propertyState = new PropertyChange(
                    EntityConfiguration.FindProperty(name),
                    oldValue,
                    newValue,
                    this);
                Properties.Add(propertyState);
            }
            else
            {
                propertyState.NewValue = newValue;
                propertyState.OldValue = oldValue;
            }
            if (Equals(propertyState.OldValue, propertyState.NewValue))
            {
                ChangedProperties.Remove(propertyState);
            }
            else
            {
                if (!ChangedProperties.Contains(propertyState))
                {
                    ChangedProperties.Add(propertyState);
                }
            }
        }
    }
}