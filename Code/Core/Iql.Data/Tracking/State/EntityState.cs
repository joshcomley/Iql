using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Iql.Data.Configuration;
using Iql.Data.Configuration.Events;
using Iql.Data.Configuration.Extensions;
using Iql.Data.Configuration.Relationships;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.Events;

namespace Iql.Data.Tracking.State
{
    [DebuggerDisplay("{EntityType.Name}")]
    public class EntityState<T> : IEntityState<T>
    {
        private bool _exists;
        private bool _markedForDeletion;
        private bool _isNew;

        //private CompositeKey _remoteKey;
        public bool IsNew
        {
            get => _isNew;
            set
            {
                _isNew = value;
                if (value)
                {
                    //_remoteKey = null;
                    MarkedForDeletion = false;
                }
            }
        }

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

        public bool MarkedForAnyDeletion => MarkedForDeletion || MarkedForCascadeDeletion;

        public void UnmarkForDeletion()
        {
            MarkedForDeletion = false;
            MarkedForCascadeDeletion = false;
        }

        public void AbandonChanges()
        {
            for (var i = 0; i < Properties.Count; i++)
            {
                var property = Properties[i];
                property.AbandonChange();
            }
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
            //_remoteKey = EntityConfiguration.GetCompositeKey(Entity);
        }

        public CompositeKey CurrentKey { get; set; }

        //public CompositeKey RemoteKey => _remoteKey ?? CurrentKey;

        public CompositeKey KeyBeforeChanges()
        {
            var compositeKey = new CompositeKey(EntityConfiguration.Key.Properties.Length);
            for (var i = 0; i < EntityConfiguration.Key.Properties.Length; i++)
            {
                var property = EntityConfiguration.Key.Properties[i];
                compositeKey.Keys[i] = new KeyValue(
                    property.Name,
                    GetPropertyState(property.Name).OldValue,
                    property.TypeDefinition);
            }
            return compositeKey;
        }

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
            Properties = new List<IPropertyState>();
            foreach (var property in EntityConfiguration.Properties)
            {
                Properties.Add(new PropertyState(property, this));
            }
            CurrentKey = entityConfiguration.GetCompositeKey(entity);
        }

        public static IEntityStateBase New(object entity, Type entityType, IEntityConfiguration entityConfiguration)
        {
            return (IEntityStateBase)
                Activator.CreateInstance(typeof(EntityState<>).MakeGenericType(entityType),
                new object[] { entity, entityType, entityConfiguration });
        }

        public IPropertyState[] GetChangedProperties()
        {
            var propertyStates = PropertyStates.Where(ps =>
                    ps.HasChanged && (!ps.Property.Kind.HasFlag(PropertyKind.Relationship) ||
                                      HasRelationshipChanged(ps.Property)))
                .ToArray();
            return propertyStates;
        }

        public bool HasRelationshipChanged(IProperty relationshipProperty)
        {
            if (relationshipProperty.Kind.HasFlag(PropertyKind.Relationship))
            {
                var relationshipEntity = relationshipProperty.PropertyGetter(Entity);
                if (relationshipEntity == null)
                {
                    return true;
                }
                var relationshipEntityState = DataContext.DataStore.Tracking
                    .TrackingSetByType(relationshipProperty.Relationship.OtherEnd.Type)
                    .GetEntityState(relationshipEntity);

                if (!relationshipEntityState.IsNew)
                {
                    var thisEndConstraints = relationshipProperty.Relationship.ThisEnd.Constraints();
                    for (var i = 0; i < thisEndConstraints.Length; i++)
                    {
                        var constraint = thisEndConstraints[i];
                        var constraintState = GetPropertyState(constraint.Name);
                        if (constraintState.HasChanged)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        private List<IPropertyState> Properties { get; }

        public bool IsInsertable()
        {
            if (EntityConfiguration.Key.Properties.All(p => p.ReadOnly))
            {
                return true;
            }

            var isInsertable = true;
            for (var i = 0; i < EntityConfiguration.Key.Properties.Length; i++)
            {
                var property = EntityConfiguration.Key.Properties[i];
                if (property.ReadOnly)
                {
                    continue;
                }

                var value = property.PropertyGetter(Entity);
                if (value.IsDefaultValue(property.TypeDefinition) && property.Kind.HasFlag(PropertyKind.RelationshipKey) &&
                    Equals(null, property.Relationship.ThisEnd.Property.PropertyGetter(Entity)))
                {
                    isInsertable = false;
                    break;
                }
            }

            return isInsertable;
        }

        public IPropertyState[] PropertyStates => Properties.ToArray();

        public IPropertyState GetPropertyState(string name)
        {
            var state = Properties.SingleOrDefault(p => p.Property.Name == name);
            return state;
        }

        public bool HasValidKey()
        {
            if (EntityConfiguration.Key.Properties.All(p => p.ReadOnly) &&
                !IsNew)
            {
                return true;
            }

            for (var i = 0; i < EntityConfiguration.Key.Properties.Length; i++)
            {
                var property = EntityConfiguration.Key.Properties[i];
                if (property.ReadOnly)
                {
                    continue;
                }

                if (property.PropertyGetter(Entity).IsDefaultValue(
                    property.TypeDefinition))
                {
                    return false;
                }
            }

            return true;
        }
    }
}