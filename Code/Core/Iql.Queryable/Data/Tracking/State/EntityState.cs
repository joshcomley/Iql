using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Events;

namespace Iql.Queryable.Data.Tracking.State
{
    [DebuggerDisplay("{EntityType.Name}")]
    public class EntityState<T> : IEntityState<T>, IEntityStateInternal
    {
        private bool _exists;
        private bool _markedForDeletion;
        private readonly List<IPropertyState> _changedProperties;

        public bool IsNew { get; set; }

        public EventEmitter<ExistsChangeEvent> ExistsChanged { get; } = new EventEmitter<ExistsChangeEvent>();
        public bool Exists
        {
            get => _exists;
            set
            {
                var changed = _exists != value;
                _exists = value;
                if (changed)
                {
                    ExistsChanged.Emit(() => new ExistsChangeEvent(this, value));
                }
            }
        }

        public EventEmitter<MarkedForDeletionChangeEvent> MarkedForDeletionChanged { get; } = new EventEmitter<MarkedForDeletionChangeEvent>();

        public bool MarkedForDeletion
        {
            get => _markedForDeletion;
            set
            {
                var changed = _markedForDeletion != value;
                _markedForDeletion = value;
                if (changed)
                {
                    MarkedForDeletionChanged.Emit(() => new MarkedForDeletionChangeEvent(this, value));
                }
            }
        }

        public bool MarkedForCascadeDeletion { get; set; }

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

        public void Reset()
        {
            IsNew = false;
            for (var i = 0; i < Properties.Count; i++)
            {
                var propertyState = Properties[i];
                propertyState.Reset();
            }
        }

        public CompositeKey Key { get; set; }
        public Guid? PersistenceKey { get; set; }
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
            _changedProperties = new List<IPropertyState>();
            Properties = new List<IPropertyState>();
            foreach (var property in EntityConfiguration.Properties)
            {
                Properties.Add(new PropertyState(property, this));
            }
            Key = entityConfiguration.GetCompositeKey(entity);
        }

        public static IEntityStateBase New(object entity, Type entityType, IEntityConfiguration entityConfiguration)
        {
            return (IEntityStateBase)
                Activator.CreateInstance(typeof(EntityState<>).MakeGenericType(entityType),
                new object[] { entity, entityType, entityConfiguration });
        }

        public List<IPropertyState> ChangedProperties => _changedProperties;

        private List<IPropertyState> Properties { get; }

        public IPropertyState GetPropertyState(string name)
        {
            var state = Properties.SingleOrDefault(p => p.Property.Name == name);
            return state;
        }

        public void SetPropertyState(string name, object oldValue, object newValue)
        {
            var propertyState = GetPropertyState(name);
            propertyState.NewValue = newValue;
            //propertyState.OldValue = oldValue;
            UpdateChanged(propertyState);
        }

        public void UpdateChanged(IPropertyState propertyState)
        {
            if (!propertyState.HasChanged)
            {
                _changedProperties.Remove(propertyState);
            }
            else
            {
                if (!_changedProperties.Contains(propertyState))
                {
                    _changedProperties.Add(propertyState);
                }
            }
        }
    }

    internal interface IEntityStateInternal
    {
        void UpdateChanged(IPropertyState state);
    }
}