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
using Iql.Queryable.Native;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.Tracking
{
    public class TrackingSet<T> : TrackingSetBase, ITrackingSet
        where T : class
    {
        private readonly ChangeIgnorer _changeIgnorer = new ChangeIgnorer();
        private readonly Dictionary<T, EntityObserver> _entityObservers = new Dictionary<T, EntityObserver>();
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

        public bool IsTracked(object entity)
        {
            return HasEntityState(entity);
        }

        public IEntityStateBase GetEntityState(object entity)
        {
            if (entity is CompositeKey)
            {
                return GetEntityStateByKey((CompositeKey)entity);
            }
            var existingEntityState = TryGetEntityState(entity);
            if (existingEntityState != null && existingEntityState.State != null)
            {
                return existingEntityState.State;
            }
            var entityState = new EntityState<T>(
                (T)entity,
                typeof(T),
                DataContext,
                EntityConfiguration);
            EntitiesByObject.Add(entity, entityState);
            if (!existingEntityState.CompositeKey.HasDefaultValue())
            {
                EntitiesByKey.Add(existingEntityState.CompositeKey.AsKeyString(), entityState);
            }
            if (PersistenceKey != null)
            {
                var persistenceKey = EnsurePersistenceKey(entity).Value;
                EntitiesByPersistenceKey.Add(persistenceKey, entityState);
            }

            Watch((T)entity);
            return entityState;
        }

        private bool HasEntityState(object entity)
        {
            var existingEntityState = TryGetEntityState(entity);
            return existingEntityState != null && existingEntityState.State != null;
        }

        class TryGetEntityStateResult
        {
            public Guid PersistenceKey { get; set; }
            public CompositeKey CompositeKey { get; set; }
            public IEntityStateBase State { get; set; }
        }
        private TryGetEntityStateResult TryGetEntityState(object entity)
        {
            var result = new TryGetEntityStateResult();
            result.PersistenceKey = Guid.Empty;

            if (EntitiesByObject.ContainsKey(entity))
            {
                result.State = EntitiesByObject[entity];
                return result;
            }

            if (PersistenceKey != null)
            {
                var persistenceKey = entity.GetPropertyValueAs<Guid>(PersistenceKey);
                if (!Equals(persistenceKey, Guid.Empty) && EntitiesByPersistenceKey.ContainsKey(persistenceKey))
                {
                    result.State = EntitiesByPersistenceKey[persistenceKey];
                    return result;
                }
            }

            result.CompositeKey = EntityConfiguration.GetCompositeKey(entity);
            var entityStateByKey = GetEntityStateByKey(result.CompositeKey);
            result.State = entityStateByKey;
            return result;
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

        public void RemoveEntity(T entity)
        {
            var state = GetEntityState(entity);
            entity = (T) state.Entity;
            var iEntity = entity as IEntity;
            if (_entityObservers.ContainsKey(entity))
            {
                _entityObservers[entity].Unobserve();
                _entityObservers.Remove(entity);
            }
            EntitiesByKey.Remove(state.Key.AsKeyString());
            EntitiesByObject.Remove(entity);
            if (state.PersistenceKey.HasValue)
            {
                EntitiesByPersistenceKey.Remove(state.PersistenceKey.Value);
            }
        }

        void ITrackingSet.RemoveEntity(object entity)
        {
            RemoveEntity((T)entity);
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

        internal void Watch(T sourceEntity)
        {
            var entity = sourceEntity as IEntity;
            if (entity == null)
            {
                return;
            }

            if (!_entityObservers.ContainsKey(sourceEntity))
            {
                var observer = new EntityObserver(GetEntityState(entity));
                _entityObservers.Add(sourceEntity, observer);
                observer.RegisterMarkForDeletionChanged(MarkedForDeletionChanged);
                observer.RegisterPropertyChanging(EntityPropertyChanging);
                observer.RegisterPropertyChanged(EntityPropertyChanged);
                observer.RegisterRelatedListChanged(RelatedListChanged);
            }
        }

        private void RelatedListChanged(IRelatedListChangedEvent changeEvent)
        {
            _changeIgnorer.IgnoreAndRunIfNotAlreadyIgnored(() =>
            {
                switch (changeEvent.Kind)
                {
                    case RelatedListChangeKind.Add:
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
            }, changeEvent.Item, changeEvent.List.Owner);
        }

        private void EntityPropertyChanged(IPropertyChangeEvent propertyChange)
        {
            _changeIgnorer.IgnoreAndRunIfNotAlreadyIgnored(() =>
            {
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
            }, propertyChange.Entity);
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

        private void SilentlyMerge(object entity, object mergeWith)
        {
            _changeIgnorer.IgnoreAndRunIfNotAlreadyIgnored(() =>
            {
                SimplePropertyMerger.Merge(
                    entity,
                    mergeWith
                );
                Reindex(entity);

            }, entity);
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