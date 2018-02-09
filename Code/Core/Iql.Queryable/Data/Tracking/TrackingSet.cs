using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.State;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Events;
using Iql.Queryable.Exceptions;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.Tracking
{
    public class TrackingSet<T> : TrackingSetBase, ITrackingSet
        where T : class
    {
        public IDataContext DataContext => DataStore.DataContext;
        public IDataStore DataStore { get; }
        public TrackingSetCollection TrackingSetCollection { get; }
        public EntityConfiguration<T> EntityConfiguration { get; }
        public SimplePropertyMerger SimplePropertyMerger { get; }

        private Dictionary<Guid, IEntityStateBase> EntitiesByPersistenceKey { get; }
        private Dictionary<object, IEntityStateBase> EntitiesByObject { get; }
        private Dictionary<string, IEntityStateBase> EntitiesByKey { get; }

        public TrackingSet(IDataStore dataStore, TrackingSetCollection trackingSetCollection)
        {
            DataStore = dataStore;
            TrackingSetCollection = trackingSetCollection;
            EntityConfiguration = DataContext.EntityConfigurationContext.GetEntity<T>();
            SimplePropertyMerger = new SimplePropertyMerger(EntityConfiguration);
            EntitiesByPersistenceKey = new Dictionary<Guid, IEntityStateBase>();
            EntitiesByObject = new Dictionary<object, IEntityStateBase>();
            EntitiesByKey = new Dictionary<string, IEntityStateBase>();
            PersistenceKey = EntityConfiguration.Properties.SingleOrDefault(p => p.Name == "PersistenceKey");
        }

        protected IProperty PersistenceKey { get; set; }

        public IEntityStateBase GetEntityState(object entity)
        {
            if (entity is CompositeKey)
            {
                return GetEntityStateByKey((CompositeKey)entity);
            }
            Guid persistenceKey;
            CompositeKey key;
            var existingEntityState = TryGetEntityState(
                entity,
                out persistenceKey,
                out key);
            if (existingEntityState != null)
            {
                return existingEntityState;
            }
            var entityState = new EntityState<T>(
                (T)entity,
                typeof(T),
                DataContext,
                EntityConfiguration);
            EntitiesByObject.Add(entity, entityState);
            if (!key.HasDefaultValue())
            {
                EntitiesByKey.Add(key.AsKeyString(), entityState);
            }
            if (PersistenceKey != null)
            {
                persistenceKey = EnsurePersistenceKey(entity).Value;
                EntitiesByPersistenceKey.Add(persistenceKey, entityState);
            }

            Watch((T)entity);
            return entityState;
        }

        private bool HasEntityState(object entity)
        {
            Guid persistenceKey;
            CompositeKey key;
            var existingEntityState = TryGetEntityState(
                entity,
                out persistenceKey,
                out key);
            return existingEntityState != null;
        }

        private IEntityStateBase TryGetEntityState(object entity, out Guid persistenceKey, out CompositeKey key)
        {
            persistenceKey = Guid.Empty;

            if (EntitiesByObject.ContainsKey(entity))
            {
                key = null;
                return EntitiesByObject[entity];
            }

            if (PersistenceKey != null)
            {
                persistenceKey = entity.GetPropertyValueAs<Guid>(PersistenceKey);
                if (!Equals(persistenceKey, Guid.Empty) && EntitiesByPersistenceKey.ContainsKey(persistenceKey))
                {
                    key = null;
                    return EntitiesByPersistenceKey[persistenceKey];
                }
            }

            key = EntityConfiguration.GetCompositeKey(entity);
            var entityStateByKey = GetEntityStateByKey(key);
            return entityStateByKey;
        }
        public IEntityStateBase GetEntityStateByKey(CompositeKey key)
        {
            var keyString = key.AsKeyString();
            return EntitiesByKey.ContainsKey(keyString)
                ? EntitiesByKey[keyString]
                : null;
        }

        public void MarkForDelete(object entity)
        {
            var entityState = GetEntityState(entity);
            entityState.MarkedForDeletion = true;
            //if (entityState.MarkedForDeletion || entityState.MarkedForCascadeDeletion || entityState.IsNew)
            //{
            //    if (!entityState.MarkedForCascadeDeletion)
            //    {
            //        entityState.MarkedForDeletion = true;
            //    }
            //}
            //else
            //{
            //    entityState.MarkedForDeletion = true;
            //}
            //DataStore.RelationshipObserver.Unobserve(entity, typeof(T));
        }

        public List<IEntityStateBase> TrackEntities(IList data, bool isNew = true)
        {
            var result = new List<IEntityStateBase>();
            var typed = (List<T>)data;
            for (var i = 0; i < typed.Count; i++)
            {
                var item = typed[i];
                result.Add(TrackEntity(item, null, isNew));
            }

            return result;
        }

        public IEntityStateBase TrackEntity(object entity, object mergeWith = null, bool isNew = true)
        {
            return TrackEntityInternal(entity, mergeWith, isNew);
        }

        public void ResetEntity(object entity)
        {
            Reset(GetEntityState(entity));
        }

        public void Reset(IEntityStateBase state)
        {
            state.Reset();
        }

        public void ResetAll(List<IEntityStateBase> states)
        {
            for (var i = 0; i < states.Count; i++)
            {
                var state = states[i];
                Reset(state);
            }
        }

        private readonly Dictionary<T, int> _entityStateChangedSubscriptions =
            new Dictionary<T, int>();
        private readonly Dictionary<T, int> _propertyChangingSubscriptions =
            new Dictionary<T, int>();
        private readonly Dictionary<T, int> _propertyChangedSubscriptions =
            new Dictionary<T, int>();
        private readonly Dictionary<T, List<int>> _relatedListChangedSubscriptions =
            new Dictionary<T, List<int>>();

        internal void Watch(T sourceEntity)
        {
            var entity = sourceEntity as IEntity;
            if (entity == null)
            {
                return;
            }
            if (!_entityStateChangedSubscriptions.ContainsKey(sourceEntity))
            {
                GetEntityState(entity).MarkedForDeletionChanged.Subscribe(MarkedForDeletionChanged);
            }
            if (!_propertyChangingSubscriptions.ContainsKey(sourceEntity))
            {
                if (entity.PropertyChanging == null)
                {
                    entity.PropertyChanging = new EventEmitter<IPropertyChangeEvent>();
                }
                var propertyChangingSubscriptionId = entity.PropertyChanging.Subscribe(EntityPropertyChanging);
                _propertyChangingSubscriptions.Add(sourceEntity, propertyChangingSubscriptionId);
            }
            if (!_propertyChangedSubscriptions.ContainsKey(sourceEntity))
            {
                if (entity.PropertyChanged == null)
                {
                    entity.PropertyChanged = new EventEmitter<IPropertyChangeEvent>();
                }
                var propertyChangedSubscriptionId = entity.PropertyChanged.Subscribe(EntityPropertyChanged);
                _propertyChangedSubscriptions.Add(sourceEntity, propertyChangedSubscriptionId);
            }

            if (!_relatedListChangedSubscriptions.ContainsKey(sourceEntity))
            {
                var list = EntityConfiguration.AllRelationships();
                for (var i = 0; i < list.Count; i++)
                {
                    var relationship = list[i];
                    if (relationship.ThisEnd.IsCollection)
                    {
                        var relatedList = relationship.ThisEnd.Property.PropertyGetter(entity) as IRelatedList;
                        var targetTracking = TrackingSetCollection.TrackingSetByType(relationship.OtherEnd.Type);
                        relatedList.Changed.Subscribe(e => RelatedListChanged(e, relationship.OtherEnd, targetTracking));
                    }
                }
            }
        }

        private void RelatedListChanged(
            IRelatedListChangedEvent changeEvent,
            IRelationshipDetail relationship,
            ITrackingSet targetTracking)
        {
            switch (changeEvent.Kind)
            {
                case RelatedListChangeKind.Assign:
                    if (changeEvent.Item != null)
                    {
                        DataStore.Add(changeEvent.Item);
                    }
                    break;
                case RelatedListChangeKind.Remove:
                    if (changeEvent.Item != null)
                    {
                        if (!DataStore.RelationshipObserver.IsAssignedToAnyRelationship(changeEvent.Item,
                            changeEvent.ItemType))
                        {
                            DataStore.Delete(changeEvent.Item);
                        }
                    }
                    break;
            }
        }

        private readonly Dictionary<object, object> _silentEntities = new Dictionary<object, object>();
        private void EntityPropertyChanged(IPropertyChangeEvent propertyChange)
        {
            if (_silentEntities.ContainsKey(propertyChange.Entity))
            {
                return;
            }
            if (Equals(propertyChange.OldValue, propertyChange.NewValue))
            {
                return;
            }

            IEntityStateBase entityState = null;
            var property = EntityConfiguration.FindProperty(propertyChange.PropertyName);
            if (property.Kind == PropertyKind.Key)
            {
                entityState = GetEntityState(propertyChange.Entity);
                if (entityState.IsNew)
                {
                    var key = EntityConfiguration.GetCompositeKey(propertyChange.Entity);
                    if (!key.HasDefaultValue())
                    {
                        throw new AttemptingToAssignKeyToEntityWhoseKeysAreGeneratedRemotelException();
                    }
                }
            }

            if (property.Kind == PropertyKind.Key)
            {
                Reindex(propertyChange.Entity);
            }
        }

        private void EntityPropertyChanging(IPropertyChangeEvent propertyChange)
        {
            var property = EntityConfiguration.FindProperty(propertyChange.PropertyName);
            switch (property.Kind)
            {
                case PropertyKind.Key:
                case PropertyKind.RelationshipKey:
                case PropertyKind.Primitive:
                    var entityState = GetEntityState(propertyChange.Entity);
                    var propertyState = entityState.GetPropertyState(property.Name);
                    propertyState.NewValue = propertyChange.NewValue;
                    break;
            }
        }

        private void MarkedForDeletionChanged(MarkedForDeletionChangeEvent markedForDeletionChangeEvent)
        {
            //if (!markedForDeletionChangeEvent.NewValue)
            //{
            //    DataContext.DataStore.RemoveQueuedOperationsOfTypeForEntity(
            //        markedForDeletionChangeEvent.EntityState.Entity,
            //        QueuedOperationType.Delete);
            //}
        }

        private readonly Dictionary<object, object> _tracking = new Dictionary<object, object>();

        internal override IEntityStateBase TrackEntityInternal(object entity, object mergeWith = null, bool isNew = true)
        {
            var isAlreadyTracked = _tracking.ContainsKey(entity);
            if (!isAlreadyTracked)
            {
                _tracking.Add(entity, entity);
            }
            var hadEntityState = HasEntityState(entity);
            if (!hadEntityState && isNew && !EntityConfiguration.GetCompositeKey(entity).HasDefaultValue())
            {
                // TODO: Check if entity key is marked as remotely generated
                throw new AttemptingToAssignKeyToEntityWhoseKeysAreGeneratedRemotelException();
            }
            var entityState = GetEntityState(entity);
            if (!hadEntityState && isNew)
            {
                entityState.IsNew = true;
            }
            else if (!isNew)
            {
                entityState.IsNew = false;
            }

            if (mergeWith != null)
            {
                SilentlyMerge(entity, mergeWith);
            }

            if (!isAlreadyTracked)
            {
                Watch((T)entityState.Entity);
            }

            if (entityState.Entity != entity)
            {
                SilentlyMerge(entityState.Entity, entity);
            }

            return entityState;
            //trackingSet.ChangeEntity(localEntity, () =>
            //{
            //foreach (var keyProperty in EntityConfiguration.Key.Properties)
            //{
            //    localEntity.SetPropertyValue(keyProperty, remoteEntity.GetPropertyValue(keyProperty));
            //}
            //}, ChangeEntityMode.NoKeyChecks);
        }

        private void SilentlyMerge(object entity, object with)
        {
            var markSilent = false;
            if (!_silentEntities.ContainsKey(entity))
            {
                markSilent = true;
                _silentEntities.Add(entity, entity);
            }
            SimplePropertyMerger.Merge(
                entity,
                with
            );
            Reindex(entity);
            if (markSilent)
            {
                _silentEntities.Remove(entity);
            }
        }

        private void Reindex(object entity)
        {
            if (EntitiesByObject.ContainsKey(entity))
            {
                var state = EntitiesByObject[entity];
                var asKeyString = state.Key.AsKeyString();
                if (EntitiesByKey.ContainsKey(asKeyString))
                {
                    EntitiesByKey.Remove(asKeyString);
                }

                state.Key = EntityConfiguration.GetCompositeKey(entity);
                EntitiesByKey.Add(state.Key.AsKeyString(), state);
            }
        }

        private Guid? EnsurePersistenceKey(object entity)
        {
            if (PersistenceKey != null)
            {
                var value = entity.GetPropertyValueAs<Guid>(PersistenceKey);
                if (Equals(value, Guid.Empty))
                {
                    var guid = Guid.NewGuid();
                    entity.SetPropertyValue(PersistenceKey, guid);
                    return guid;
                }

                return value;
            }

            return null;
        }

        public IEnumerable<IEntityCrudOperationBase> GetInserts()
        {
            var inserts = new List<AddEntityOperation<T>>();
            foreach (var entity in EntitiesByObject.Keys)
            {
                var entityState = GetEntityState(entity);
                if (entityState.IsNew && !entityState.MarkedForAnyDeletion)
                {
                    inserts.Add(new AddEntityOperation<T>((T)entity, DataContext));
                }
            }
            return inserts;
        }

        public IEnumerable<IEntityCrudOperationBase> GetDeletions()
        {
            var deletions = new List<DeleteEntityOperation<T>>();
            foreach (var entity in EntitiesByObject.Keys)
            {
                var entityState = GetEntityState(entity);
                if (entityState.MarkedForAnyDeletion && !entityState.IsNew)
                {
                    deletions.Add(new DeleteEntityOperation<T>((T)entity, DataContext));
                }
            }
            return deletions;
        }

        public IEnumerable<IUpdateEntityOperation> GetUpdates()
        {
            var updates = new List<UpdateEntityOperation<T>>();
            foreach (var entity in EntitiesByObject.Keys)
            {
                var entityState = GetEntityState(entity);
                if (!entityState.IsNew && !entityState.MarkedForAnyDeletion &&
                    entityState.ChangedProperties.Any())
                {
                    updates.Add(new UpdateEntityOperation<T>((T)entity, DataContext));
                }
            }
            return updates;
        }
    }
}