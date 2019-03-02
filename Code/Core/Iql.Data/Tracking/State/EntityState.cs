using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Iql.Conversion;
using Iql.Conversion.State;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.Events;
using Iql.Data.Extensions;
using Iql.Entities;
using Iql.Entities.Events;
using Iql.Entities.Extensions;
using Iql.Entities.Relationships;
using Newtonsoft.Json;

namespace Iql.Data.Tracking.State
{
    [DebuggerDisplay("{EntityType.Name}")]
    public class EntityState<T> : IEntityState<T>
    {
        private bool _exists;
        private bool _markedForDeletion;
        private bool _isNew = true;

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
                var state = Properties[i];
                state.AbandonChange();
            }
            MarkedForDeletion = false;
            MarkedForCascadeDeletion = false;
        }

        public void MarkForCascadeDeletion(object from, IRelationship relationship)
        {
            MarkedForCascadeDeletion = true;
            CascadeDeletedBy.Add(new CascadeDeletion(relationship, from));
        }

        public void HardReset()
        {
            IsNew = false;
            for (var i = 0; i < Properties.Count; i++)
            {
                var propertyState = Properties[i];
                propertyState.HardReset();
            }
            //_remoteKey = EntityConfiguration.GetCompositeKey(Entity);
        }

        public void SoftReset(bool markAsNotNew)
        {
            if (markAsNotNew)
            {
                IsNew = false;
            }
            for (var i = 0; i < Properties.Count; i++)
            {
                var propertyState = Properties[i];
                propertyState.SoftReset();
            }
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
                    GetPropertyState(property.Name).RemoteValue,
                    property.TypeDefinition);
            }
            return compositeKey;
        }

        public Guid? PersistenceKey { get; set; }
        public List<CascadeDeletion> CascadeDeletedBy { get; } = new List<CascadeDeletion>();
        public void Restore(SerializedEntityState state)
        {
            for (var i = 0; i < state.PropertyStates.Length; i++)
            {
                var deserializedPropertyState = state.PropertyStates[i];
                GetPropertyState(deserializedPropertyState.Property)
                    .Restore(deserializedPropertyState);
            }

            MarkedForDeletion = state.MarkedForDeletion;
            MarkedForCascadeDeletion = state.MarkedForCascadeDeletion;
            if (state.PersistenceKey != null)
            {
                PersistenceKey = state.PersistenceKey.EnsureGuid();
            }

            IsNew = state.IsNew;
            CurrentKey = state.CurrentKey.ToCompositeKey(EntityConfiguration);
            for (var i = 0; i < EntityConfiguration.Key.Properties.Length; i++)
            {
                var key = EntityConfiguration.Key.Properties[i];
                key.SetValue(Entity, CurrentKey.Keys.Single(_ => _.Name == key.PropertyName).Value);
            }
        }

        public bool Floating { get; set; }
        public DataTracker DataTracker { get; }
        public T Entity { get; }
        object IEntityStateBase.Entity => Entity;
        public Type EntityType { get; }
        //public IDataContext DataContext => DataTracker.DataContext;
        public IEntityConfiguration EntityConfiguration { get; }

        public EntityState(
            DataTracker dataTracker,
            T entity, 
            Type entityType, 
            IEntityConfiguration entityConfiguration)
        {
            DataTracker = dataTracker;
            Entity = entity;
            EntityType = entityType;
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

        public IPropertyState[] GetChangedProperties(IProperty[] properties = null)
        {
            var propertyStates = PropertyStates.Where(ps =>
                    ps.HasChanged && !ps.Property.Kind.HasFlag(PropertyKind.Relationship) && (properties == null || properties.Contains(ps.Property)))
                .ToArray();
            return propertyStates;
        }

        public bool HasRelationshipChanged(IProperty relationshipProperty)
        {
            if (relationshipProperty.Relationship.ThisIsTarget)
            {
                return false;
            }
            if (relationshipProperty.Kind.HasFlag(PropertyKind.Relationship))
            {
                var relationshipEntity = relationshipProperty.GetValue(Entity);
                if (relationshipEntity == null)
                {
                    return true;
                }
                var relationshipEntityState = DataTracker
                    .TrackingSetByType(relationshipProperty.Relationship.OtherEnd.Type)
                    .FindMatchingEntityState(relationshipEntity);

                if (!relationshipEntityState.IsNew)
                {
                    var thisEndConstraints = relationshipProperty.Relationship.ThisEnd.Constraints;
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

        public T EntityBeforeChanges()
        {
            if (IsNew)
            {
                return default(T);
            }
            var entity = Entity.Clone(EntityConfiguration.Builder, EntityType, RelationshipCloneMode.DoNotClone);
            foreach (var property in Properties)
            {
                property.Property.SetValue(entity, property.RemoteValue);
            }
            return (T) entity;
        }

        object IEntityStateBase.EntityBeforeChanges()
        {
            return EntityBeforeChanges();
        }

        public IPropertyState[] PropertyStates => Properties.ToArray();

        public IPropertyState GetPropertyState(string name)
        {
            var state = Properties.SingleOrDefault(p => p.Property.Name == name);
            return state;
        }

        public bool HasValidKey()
        {
            if (EntityConfiguration.Key.Properties.All(p => p.IsReadOnly) &&
                !IsNew)
            {
                return true;
            }

            for (var i = 0; i < EntityConfiguration.Key.Properties.Length; i++)
            {
                var property = EntityConfiguration.Key.Properties[i];
                if (property.IsReadOnly)
                {
                    continue;
                }

                if (property.GetValue(Entity).IsDefaultValue(
                    property.TypeDefinition))
                {
                    return false;
                }
            }

            return true;
        }

        public string SerializeToJson()
        {
            return this.ToJson();
        }

        public object PrepareForJson()
        {
            return new
            {
                CurrentKey = CurrentKey.PrepareForJson(),
                PersistenceKey,
                IsNew,
                MarkedForDeletion,
                MarkedForCascadeDeletion,
                PropertyStates = PropertyStates.Where(_ => IsNew || _.HasChanged).Select(_ => _.PrepareForJson()).Where(_ => _ != null)
            };
        }
    }
}