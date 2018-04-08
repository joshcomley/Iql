using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.Relationships;
using Iql.Queryable.Data.Tracking.State;
using Iql.Queryable.Events;
using Iql.Queryable.Exceptions;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data.Tracking
{
    public class TrackingSet<T> : TrackingSetBase, ITrackingSet
        where T : class
    {
        private readonly ChangeIgnorer _changeIgnorer = new ChangeIgnorer();
        private readonly PropertyChangeIgnorer _keyChangeAllower = new PropertyChangeIgnorer();
        private readonly Dictionary<T, EntityObserver> _entityObservers = new Dictionary<T, EntityObserver>();
        public IDataContext DataContext => DataStore.DataContext;
        public IDataStore DataStore { get; }
        public TrackingSetCollection TrackingSetCollection { get; }

        public bool EntityWithSameKeyIsTracked(T entity)
        {
            var key = EntityConfiguration.GetCompositeKey(entity);
            var state = GetEntityStateByKey(key);
            if (state == null)
            {
                return false;
            }
            if (state.Entity == entity)
            {
                return false;
            }
            return true;
        }

        bool ITrackingSet.EntityWithSameKeyIsTracked(object entity)
        {
            return EntityWithSameKeyIsTracked((T)entity);
        }

        public EntityConfiguration<T> EntityConfiguration { get; }
        IEntityConfiguration ITrackingSet.EntityConfiguration => EntityConfiguration;
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

        public void SetKey(object entity, Action action)
        {
            _keyChangeAllower.IgnoreAndRunEvenIfAlreadyIgnored(action,
                EntityConfiguration.Key.Properties.ToArray(),
                entity);
        }

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
                if (// ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    !Equals(persistenceKey, null) && !Equals(persistenceKey, Guid.Empty) && EntitiesByPersistenceKey.ContainsKey(persistenceKey))
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

        public bool KeyIsTracked(CompositeKey key)
        {
            return GetEntityStateByKey(key) != null;
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

        public List<IEntityStateBase> TrackEntities(IList data, bool isNew = true, bool allowNew = true, bool onlyMergeWithExisting = false)
        {
            var result = new List<IEntityStateBase>();
            var typed = (List<T>)data;
            for (var i = 0; i < typed.Count; i++)
            {
                var item = typed[i];
                if (allowNew || (!isNew && IsTracked(item)))
                {
                    result.Add(TrackEntity(item, null, isNew, onlyMergeWithExisting));
                }
            }

            return result;
        }

        public IEntityStateBase TrackEntity(object entity, object mergeWith = null, bool isNew = true, bool onlyMergeWithExisting = false)
        {
            return TrackEntityInternal(entity, mergeWith, isNew, onlyMergeWithExisting);
        }

        public void RemoveEntity(T entity)
        {
            var state = GetEntityState(entity);
            entity = (T)state.Entity;
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

        private void RelatedListChanged(IRelatedListChangeEvent changeEvent)
        {
            if (TrackingSetCollection.TrackEntities)
            {
                _changeIgnorer.IgnoreAndRunIfNotAlreadyIgnored(() =>
                {
                    switch (changeEvent.Kind)
                    {
                        case RelatedListChangeKind.Add:
                            if (changeEvent.Item != null)
                            {
                                // Make sure we prep the constraints so we can compare to see if
                                // an item with a matching key already exists, and if so then 
                                // reject this change
                                var relationship =
                                    EntityConfiguration.Builder
                                    .GetEntityByType(changeEvent.OwnerType)
                                    .FindProperty(changeEvent.List.PropertyName)
                                    .Relationship;
                                var sourceConstraints = relationship.OtherEnd.Constraints();
                                var targetConstraints = relationship.ThisEnd.Constraints();
                                for (var i = 0; i < sourceConstraints.Length; i++)
                                {
                                    var sourceConstraint = sourceConstraints[i];
                                    var targetConstraint = targetConstraints[i];
                                    if (sourceConstraint.Relationship.OtherEnd == relationship.ThisEnd)
                                    {
                                        sourceConstraint.PropertySetter(
                                            changeEvent.Item,
                                            targetConstraint.PropertyGetter(changeEvent.Owner));
                                    }
                                }
                                var state = DataStore.Add(changeEvent.Item);
                                if (state.Entity != changeEvent.Item)
                                {
                                    changeEvent.Disallow = true;
                                }
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
        }

        private void EntityPropertyChanged(IPropertyChangeEvent propertyChange)
        {
            DataStore.RelationshipObserver.RunIfNotIgnored(() =>
                {
                    _changeIgnorer.IgnoreAndRunIfNotAlreadyIgnored(() =>
                    {
                        if (Equals(propertyChange.OldValue, propertyChange.NewValue))
                        {
                            return;
                        }

                        IEntityStateBase entityState = null;
                        var property = EntityConfiguration.FindProperty(propertyChange.PropertyName);
                        if (property.Kind.HasFlag(PropertyKind.Key))
                        {
                            if (!_keyChangeAllower.AreAnyIgnored(new[] { property }, propertyChange.Entity))
                            {
                                entityState = GetEntityState(propertyChange.Entity);
                                if (entityState.IsNew)
                                {
                                    var key = EntityConfiguration.GetCompositeKey(propertyChange.Entity);
                                    if (!key.HasDefaultValue())
                                    {
                                        throw new AttemptingToAssignKeyToEntityWhoseKeysAreGeneratedRemotelyException();
                                    }
                                }
                            }
                        }

                        if (property.Kind.HasFlag(PropertyKind.Key))
                        {
                            Reindex(propertyChange.Entity);
                        }
                    }, propertyChange.Entity);
                },
                EntityConfiguration.FindProperty(propertyChange.PropertyName),
                propertyChange.Entity
            );
        }

        private void EntityPropertyChanging(IPropertyChangeEvent propertyChange)
        {
            var property = EntityConfiguration.FindProperty(propertyChange.PropertyName);
            if (property.Kind.HasFlag(PropertyKind.Key) ||
               property.Kind.HasFlag(PropertyKind.RelationshipKey) ||
               property.Kind.HasFlag(PropertyKind.Primitive))
            {
                var entityState = GetEntityState(propertyChange.Entity);
                var propertyState = entityState.GetPropertyState(property.Name);
                propertyState.NewValue = propertyChange.NewValue;
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

        internal override IEntityStateBase TrackEntityInternal(object entity, object mergeWith = null,
            bool isNew = true, bool onlyMergeWithExisting = false)
        {
            if (!IsEntityTracked(entity))
            {
                TrackedEntities.Add(entity, this);
            }
            else if (TrackedEntities[entity] != this && !onlyMergeWithExisting)
            {
                throw new Exception("This entity is already tracked by another context.");
            }
            var isAlreadyTracked = _tracking.ContainsKey(entity);
            if (!isAlreadyTracked)
            {
                _tracking.Add(entity, entity);
            }
            var hadEntityState = HasEntityState(entity);
            if (!hadEntityState &&
                isNew &&
                !EntityConfiguration.GetCompositeKey(entity).HasDefaultValue() &&
                EntityConfiguration.Key.IsGeneratedRemotely &&
                !EntityConfiguration.Key.IsPivot())
            {

                //if (nonRelationshipKey)
                //{
                //    // TODO: Check if entity key is marked as remotely generated
                //}
                throw new AttemptingToAssignKeyToEntityWhoseKeysAreGeneratedRemotelyException();
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
                var newKey = EntityConfiguration.GetCompositeKey(entity);
                var oldKeyString = state.Key.AsKeyString();
                var newKeyString = newKey.AsKeyString();
                if (newKeyString != oldKeyString)
                {
                    if (EntitiesByKey.ContainsKey(oldKeyString))
                    {
                        EntitiesByKey.Remove(oldKeyString);
                    }
                    EntitiesByKey.Add(newKeyString, state);
                    state.Key = newKey;
                }
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