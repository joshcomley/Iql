using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Events;

namespace Iql.Queryable.Data.Crud.State
{
    [DebuggerDisplay("{EntityType.Name}")]
    public class EntityState
    {
        private bool _markedForDeletion;
        public bool IsNew { get; set; }

        public EventEmitter<MarkedForDeletionChangeEvent> MarkedForDeletionChanged { get; } = new EventEmitter<MarkedForDeletionChangeEvent>();

        public bool MarkedForDeletion
        {
            get { return _markedForDeletion; }
            set
            {
                var changed = _markedForDeletion != value;
                _markedForDeletion = value;
                if (changed)
                {
                    MarkedForDeletionChanged.Emit(new MarkedForDeletionChangeEvent(this, value));
                }
            }
        }

        public bool MarkedForCascadeDeletion { get; private set; }

        public bool MarkedForAnyDeletion
        {
            get { return MarkedForDeletion || MarkedForCascadeDeletion; }
        }

        public void UnmarkForDeletion()
        {
            MarkedForDeletion = false;
            MarkedForCascadeDeletion = false;
        }

        public void MarkForCascadeDeletion(object from, IRelationship relationship)
        {
            MarkedForCascadeDeletion = true;
            CascadeDeletedBy.Add(new CascadeDeletion(relationship, from));
        }

        public List<CascadeDeletion> CascadeDeletedBy { get; } = new List<CascadeDeletion>();
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