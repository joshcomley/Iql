using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Events;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data.Crud.State
{
    [DebuggerDisplay("{EntityType.Name}")]
    public class EntityState<T> : IEntityState<T>
    {
        private bool _markedForDeletion;
        private bool _isNew;

        public bool IsNew
        {
            get { return _isNew; }
            set { _isNew = value; }
        }

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
        public T Entity { get; }
        object IEntityStateBase.Entity => Entity;
        public Type EntityType { get; }
        public IDataContext DataContext { get; }
        public IEntityConfiguration EntityConfiguration { get; }

        public EntityState(T entity, Type entityType, IDataContext dataContext, IEntityConfiguration entityConfiguration)
        {
            Entity = entity;
            EntityType = entityType;
            DataContext = dataContext;
            EntityConfiguration = entityConfiguration;
            ChangedProperties = new List<PropertyChange>();
            Properties = new List<PropertyChange>();
        }

        public static IEntityStateBase New(object entity, Type entityType, IEntityConfiguration entityConfiguration)
        {
            return (IEntityStateBase)
                Activator.CreateInstance(typeof(EntityState<>).MakeGenericType(entityType),
                new object[]{entity, entityType, entityConfiguration});
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