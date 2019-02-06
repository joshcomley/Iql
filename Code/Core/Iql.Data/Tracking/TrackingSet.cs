using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.DataStores;
using Iql.Data.Events;
using Iql.Data.Exceptions;
using Iql.Data.Relationships;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Events;
using Iql.Entities.Exceptions;
using Iql.Entities.Extensions;
using Newtonsoft.Json;

namespace Iql.Data.Tracking
{
    public class TrackingSet<T> : TrackingSetBase, ITrackingSet
        where T : class
    {
        private readonly ChangeIgnorer _changeIgnorer = new ChangeIgnorer();
        private readonly PropertyChangeIgnorer _keyChangeAllower = new PropertyChangeIgnorer();
        private readonly Dictionary<T, EntityObserver> _entityObservers = new Dictionary<T, EntityObserver>();
        public DataTracker DataTracker => DataStore.DataTracker;

        public IDataContext DataContext => DataStore.DataContext;
        public IDataStore DataStore { get; }
        public TrackingSetCollection TrackingSetCollection { get; }

        public bool DifferentEntityWithSameKeyIsTracked(T entity)
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

        bool ITrackingSet.DifferentEntityWithSameKeyIsTracked(object entity)
        {
            return DifferentEntityWithSameKeyIsTracked((T)entity);
        }

        public EntityConfiguration<T> EntityConfiguration { get; }
        IEntityConfiguration ITrackingSet.EntityConfiguration => EntityConfiguration;
        public SimplePropertyMerger SimplePropertyMerger { get; }

        private Dictionary<Guid, IEntityStateBase> EntitiesByPersistenceKey { get; }
        private Dictionary<object, IEntityStateBase> EntitiesByObject { get; }
        private Dictionary<string, RemoteKeyMap> EntitiesByRemoteKey { get; }
        private Dictionary<string, IEntityStateBase> EntitiesByKey { get; }

        public TrackingSet(IDataStore dataStore, TrackingSetCollection trackingSetCollection)
        {
            DataStore = dataStore;
            TrackingSetCollection = trackingSetCollection;
            EntityConfiguration = DataContext.EntityConfigurationContext.EntityType<T>();
            SimplePropertyMerger = new SimplePropertyMerger(EntityConfiguration);
            EntitiesByPersistenceKey = new Dictionary<Guid, IEntityStateBase>();
            EntitiesByObject = new Dictionary<object, IEntityStateBase>();
            EntitiesByKey = new Dictionary<string, IEntityStateBase>();
            EntitiesByRemoteKey = new Dictionary<string, RemoteKeyMap>();
            PersistenceKey = EntityConfiguration.Properties.SingleOrDefault(p => p.Name == "PersistenceKey");
        }

        protected IProperty PersistenceKey { get; set; }

        public void SetKey(object entity, Action action)
        {
            _keyChangeAllower.IgnoreAndRunEvenIfAlreadyIgnored(action,
                EntityConfiguration.Key.Properties.ToArray(),
                entity);
        }

        public bool IsMatchingEntityTracked(object entity)
        {
            return HasEntityState(entity, false);
        }

        public bool IsTracked(object entity)
        {
            return HasEntityState(entity, true);
        }

        public IEntityStateBase FindMatchingEntityState(object entity)
        {
            var result = TryGetEntityState(entity, false);
            return result?.State;
        }

        public IEntityStateBase GetEntityState(object entity)
        {
            var result = TryGetEntityState(entity, true);
            return result?.State;
        }

        private IEntityStateBase GetOrSetEntityState(object entity)
        {
            if (entity is CompositeKey)
            {
                return GetEntityStateByKey((CompositeKey)entity);
            }
            var existingEntityState = TryGetEntityState(entity, false);
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

        private bool HasEntityState(object entity, bool entityOnly)
        {
            var result = entityOnly ? GetEntityState(entity) : FindMatchingEntityState(entity);
            return result != null;
        }

        class TryGetEntityStateResult
        {
            public Guid PersistenceKey { get; set; }
            public CompositeKey CompositeKey { get; set; }
            public IEntityStateBase State { get; set; }
        }

        private TryGetEntityStateResult TryGetEntityState(object entity, bool entityOnly)
        {
            var result = new TryGetEntityStateResult();
            result.PersistenceKey = Guid.Empty;

            if (EntitiesByObject.ContainsKey(entity))
            {
                result.State = EntitiesByObject[entity];
                return result;
            }

            if (entityOnly)
            {
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
            //var keyString = key.AsKeyString();
            //var state = EntitiesByKey.ContainsKey(keyString)
            //    ? EntitiesByKey[keyString]
            //    : null;
            //state = state ??
            //        (EntitiesByRemoteKey.ContainsKey(keyString) ? EntitiesByRemoteKey[keyString].State : null);
            //return state;
        }

        public bool KeyIsTracked(CompositeKey key)
        {
            return GetEntityStateByKey(key) != null;
        }

        public void MarkForDelete(object entity)
        {
            var entityState = GetOrSetEntityState(entity);
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
                if (allowNew || (!isNew && IsMatchingEntityTracked(item)))
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

        public void RemoveEntityByKey(CompositeKey compositeKey)
        {
            var keyString = compositeKey.AsKeyString();
            if (EntitiesByRemoteKey.ContainsKey(keyString))
            {
                var mapping = EntitiesByRemoteKey[keyString];
                EntitiesByRemoteKey.Remove(keyString);
                if (mapping.State != null)
                {
                    RemoveEntity((T)mapping.State.Entity);
                }
            }
        }

        public void RemoveEntity(T entity)
        {
            var state = GetOrSetEntityState(entity);
            var stateKey = state.KeyBeforeChanges();
            RemoveEntityByKey(stateKey);
            entity = (T)state.Entity;
            if (_entityObservers.ContainsKey(entity))
            {
                _entityObservers[entity].Unobserve();
                _entityObservers.Remove(entity);
            }
            EntitiesByKey.Remove(state.CurrentKey.AsKeyString());
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
            Reset(GetOrSetEntityState(entity));
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
            var entity = (IEntity)sourceEntity;
            if (entity == null)
            {
                return;
            }

            if (!_entityObservers.ContainsKey(sourceEntity))
            {
                var observer = new EntityObserver(GetOrSetEntityState(entity));
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
                        case RelatedListChangeKind.Adding:
                            if (changeEvent.Item != null)
                            {
                                // We need to build a CompositeKey, get the tracked entity, if any
                                // and if it is not ours, then disallow

                                // Make sure we prep the constraints so we can compare to see if
                                // an item with a matching key already exists, and if so then 
                                // reject this change
                                var relationship =
                                    EntityConfiguration.Builder
                                        .GetEntityByType(changeEvent.OwnerType)
                                        .FindProperty(changeEvent.List.PropertyName)
                                        .Relationship;
                                var compositeKey = relationship.OtherEnd.EntityConfiguration.GetCompositeKey(changeEvent.Item);

                                // If this relationship also defines some key values, make sure they are checked in the
                                // composite key
                                var sourceConstraints = relationship.OtherEnd.Constraints;
                                var targetConstraints = relationship.ThisEnd.Constraints;
                                for (var i = 0; i < sourceConstraints.Length; i++)
                                {
                                    var sourceConstraint = sourceConstraints[i];
                                    var targetConstraint = targetConstraints[i];
                                    if (sourceConstraint.Kind.HasFlag(PropertyKind.Key) && sourceConstraint.Relationship.OtherEnd == relationship.ThisEnd)
                                    {
                                        compositeKey.Keys.Single(key => key.Name == sourceConstraint.Name)
                                            .Value = targetConstraint.GetValue(changeEvent.Owner);
                                    }
                                }

                                var trackingSet = DataContext.DataStore.Tracking.TrackingSetByType(relationship.OtherEnd.Type);
                                var trackedItem = trackingSet.GetEntityStateByKey(compositeKey);
                                if (trackedItem != null && trackedItem.Entity != changeEvent.Item)
                                {
                                    if (changeEvent.List.Contains(trackedItem.Entity))
                                    {
                                        changeEvent.ObservableListChangeEvent.Disallow = true;
                                    }
                                    //else
                                    //{
                                    //    for (var i = 0; i < relationship.OtherEnd.Configuration.Properties.Count; i++)
                                    //    {
                                    //        var property = relationship.OtherEnd.Configuration.Properties[i];
                                    //        property.SetValue(trackedItem.Entity,
                                    //            property.GetValue(changeEvent.Item));
                                    //    }
                                    //    changeEvent.ObservableListChangeEvent.Item = trackedItem.Entity;
                                    //}
                                }
                            }
                            break;
                        case RelatedListChangeKind.Added:
                            if (changeEvent.Item != null)
                            {
                                var state = DataStore.Add(changeEvent.Item);
                                if (state.Entity != changeEvent.Item)
                                {
                                    changeEvent.ObservableListChangeEvent.Disallow = true;
                                }
                            }
                            break;
                        case RelatedListChangeKind.Removed:
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
            _changeIgnorer.IgnoreAndRunIfNotAlreadyIgnored(() =>
            {
                if (Equals(propertyChange.OldValue, propertyChange.NewValue))
                {
                    return;
                }

                var property = EntityConfiguration.FindProperty(propertyChange.PropertyName);
                if (property.Kind.HasFlag(PropertyKind.Key) && property.IsReadOnly)
                {
                    DataStore.RelationshipObserver.RunIfNotIgnored(() =>
                        {
                            if (!_keyChangeAllower.AreAnyIgnored(new[] { property }, propertyChange.Entity))
                            {
                                var entityState = GetOrSetEntityState(propertyChange.Entity);
                                if (entityState.IsNew)
                                {
                                    var key = EntityConfiguration.GetCompositeKey(propertyChange.Entity);
                                    if (!key.HasDefaultValue())
                                    {
                                        throw new AttemptingToAssignRemotelyGeneratedKeyException();
                                    }
                                }
                            }
                        },
                        property,
                        propertyChange.Entity
                    );
                }

                if (property.Kind.HasFlag(PropertyKind.Key))
                {
                    Reindex(propertyChange.Entity);
                }
            }, propertyChange.Entity);
        }

        private void EntityPropertyChanging(IPropertyChangeEvent propertyChange)
        {
            var property = EntityConfiguration.FindProperty(propertyChange.PropertyName);
            if (property.Kind.HasFlag(PropertyKind.Key) ||
               property.Kind.HasFlag(PropertyKind.Relationship) ||
               property.Kind.HasFlag(PropertyKind.RelationshipKey) ||
               property.Kind.HasFlag(PropertyKind.Primitive))
            {
                var entityState = GetOrSetEntityState(propertyChange.Entity);
                var propertyState = entityState.GetPropertyState(property.Name);
                propertyState.LocalValue = propertyChange.NewValue;
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
            if (!GlobalTracking.IsEntityTracked(entity))
            {
                GlobalTracking.RegisterAsTracked(entity, this);
            }
            else if (GlobalTracking.GetTrackingSet(entity) != this && !onlyMergeWithExisting)
            {
                throw new Exception("This entity is already tracked by another context.");
            }
            var isAlreadyTracked = _tracking.ContainsKey(entity);
            if (!isAlreadyTracked)
            {
                _tracking.Add(entity, entity);
            }

            var hadEntityState = HasEntityState(entity, true);
            if (!hadEntityState &&
                isNew &&
                !EntityConfiguration.GetCompositeKey(entity).HasDefaultValue())
            {
                for (var i = 0; i < EntityConfiguration.Key.Properties.Length; i++)
                {
                    var property = EntityConfiguration.Key.Properties[i];
                    if (property.IsReadOnly && !property.GetValue(entity).IsDefaultValue(property.TypeDefinition))
                    {
                        throw new AttemptingToAssignRemotelyGeneratedKeyException();
                    }
                }

                //if (nonRelationshipKey)
                //{
                //    // TODO: Check if entity key is marked as remotely generated
                //}
            }
            var entityState = GetOrSetEntityState(entity);
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

            TrackRemoteKey(entityState, null);

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
                var oldKey = state.CurrentKey;
                var oldKeyString = oldKey.AsKeyString();
                var newKeyString = newKey.AsKeyString();
                if (newKeyString != oldKeyString ||
                    (!state.IsNew && !EntitiesByKey.ContainsKey(newKeyString)))
                {
                    if (EntitiesByKey.ContainsKey(oldKeyString))
                    {
                        EntitiesByKey.Remove(oldKeyString);
                    }
                    if (state.HasValidKey())
                    {
                        EntitiesByKey.Add(newKeyString, state);
                    }
                    state.CurrentKey = newKey;
                }
                TrackRemoteKey(state, oldKey);
            }
        }

        private void TrackRemoteKey(IEntityStateBase state, CompositeKey oldKey)
        {
            var keyString = state.CurrentKey.AsKeyString();
            if (!state.IsNew && state.HasValidKey())
            {
                if (EntitiesByRemoteKey.ContainsKey(keyString))
                {
                    var map = EntitiesByRemoteKey[keyString];
                    if (map.State == null)
                    {
                        map.State = state;
                    }
                    else if (map.State != state)
                    {
                        throw new DuplicateKeyException();
                    }
                }
                else
                {
                    EntitiesByRemoteKey.Add(keyString, new RemoteKeyMap(state, state.CurrentKey));
                }

                if (oldKey != null)
                {
                    var oldKeyString = oldKey.AsKeyString();
                    if (oldKeyString != keyString && EntitiesByRemoteKey.ContainsKey(oldKeyString))
                    {
                        EntitiesByRemoteKey.Remove(oldKeyString);
                    }
                }
            }
            else if (oldKey != null)
            {
                var oldKeyString = oldKey.AsKeyString();
                if (oldKeyString != keyString && EntitiesByRemoteKey.ContainsKey(oldKeyString))
                {
                    var remoteKeyMap = EntitiesByRemoteKey[oldKeyString];
                    remoteKeyMap.OldPropertyValues = remoteKeyMap.State.PropertyStates.Select(p => p.Copy()).ToArray();
                    //remoteKeyMap.State = null;
                }
            }

            //if (!state.IsNew && state.CurrentKey.HasDefaultValue())
            //{
            //    state.IsNew = true;
            //}
            if (state.IsNew)
            {
                if (EntitiesByRemoteKey.ContainsKey(keyString))
                {
                    var map = EntitiesByRemoteKey[keyString];
                    if (map.State == null)
                    {
                        state.IsNew = false;
                        foreach (var propertyState in map.OldPropertyValues)
                        {
                            var newPropertyState = state.GetPropertyState(propertyState.Property.Name);
                            newPropertyState.RemoteValue = propertyState.RemoteValue;
                            newPropertyState.LocalValue = propertyState.Property.GetValue(state.Entity);
                        }
                        map.OldPropertyValues = null;
                        map.State = state;
                    }
                    else if (map.State != state)
                    {
                        throw new DuplicateKeyException();
                    }
                }
            }
            //var entity = state.Entity;
            //var hasDefaultValue = false;
            //for (var i = 0; i < EntityConfiguration.Key.Properties.Length; i++)
            //{
            //    var property = EntityConfiguration.Key.Properties[i];
            //    if (property.Kind.HasFlag(PropertyKind.RelationshipKey) &&
            //        property.GetValue(entity).IsDefaultValue(property.TypeDefinition))
            //    {
            //        hasDefaultValue = true;
            //    }
            //}

            //if (!hasDefaultValue && state.IsNew)
            //{
            //    var remoteKeyString = state.RemoteKey.AsKeyString();
            //    if (EntitiesByRemoteKey.ContainsKey(remoteKeyString))
            //    {
            //        state.IsNew = false;
            //    }
            //}

            //if (!state.IsNew)
            //{
            //    var remoteKeyString = state.RemoteKey.AsKeyString();
            //    if (!EntitiesByRemoteKey.ContainsKey(remoteKeyString))
            //    {
            //        EntitiesByRemoteKey.Add(remoteKeyString, new RemoteKeyMap(state, state.RemoteKey));
            //    }
            //    else if (EntitiesByRemoteKey[remoteKeyString].State != state)
            //    {
            //        var oldStateMap = EntitiesByRemoteKey[remoteKeyString];
            //        if (oldStateMap.State != null)
            //        {
            //            oldStateMap.State.IsNew = true;
            //            state.Reset();
            //            for (var i = 0; i < oldStateMap.State.PropertyStates.Length; i++)
            //            {
            //                var oldPropertyState = oldStateMap.State.PropertyStates[i];
            //                var newPropertyState = state.GetPropertyState(oldPropertyState.Property.Name);
            //                if (oldPropertyState.Property.Kind.HasFlag(PropertyKind.Key))
            //                {
            //                    newPropertyState.OldValue = newPropertyState.NewValue;
            //                }
            //                else
            //                {
            //                    newPropertyState.OldValue = oldPropertyState.OldValue;
            //                }
            //            }
            //            oldStateMap.State = state;
            //            oldStateMap.Key = state.RemoteKey;
            //        }
            //    }
            //}
        }

        private Guid? EnsurePersistenceKey(object entity)
        {
            if (PersistenceKey != null)
            {
                var value = entity.GetPropertyValueAs<Guid>(PersistenceKey);
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (value == null || Equals(value, Guid.Empty))
                {
                    var guid = PersistenceKeyGenerator.New();
                    entity.SetPropertyValue(PersistenceKey, guid);
                    return guid;
                }

                return value;
            }

            return null;
        }

        public IEnumerable<IEntityCrudOperationBase> GetInserts(object[] entities = null)
        {
            var inserts = new List<AddEntityOperation<T>>();
            foreach (var entity in EntitiesByObject.Keys)
            {
                if (ShouldIgnoreEntity(entity, entities))
                {
                    continue;
                }
                var entityState = GetOrSetEntityState(entity);
                if (entityState.IsNew && !entityState.MarkedForAnyDeletion)
                {
                    inserts.Add(new AddEntityOperation<T>((T)entity, DataContext));
                }
            }
            return inserts;
        }

        private static bool ShouldIgnoreEntity(object entity, object[] entities)
        {
            if (entities == null || entities.Length == 0)
            {
                return false;
            }
            return !entities.Contains(entity);
        }

        public IEnumerable<IEntityCrudOperationBase> GetDeletions(object[] entities = null)
        {
            var deletions = new List<DeleteEntityOperation<T>>();
            foreach (var key in EntitiesByRemoteKey)
            {
                if (!key.Value.State.MarkedForAnyDeletion ||
                    ShouldIgnoreEntity(key.Value.State.Entity, entities))
                {
                    continue;
                }
                deletions.Add(new DeleteEntityOperation<T>(
                    key.Value.Key,
                    (T)key.Value.State.Entity,
                    DataContext));
            }
            foreach (var entity in EntitiesByObject.Keys)
            {
                if (ShouldIgnoreEntity(entity, entities))
                {
                    continue;
                }

                if (deletions.All(_ => _.Entity != entity))
                {
                    var entityState = GetOrSetEntityState(entity);
                    if (entityState.MarkedForAnyDeletion && !entityState.IsNew)
                    {
                        deletions.Add(new DeleteEntityOperation<T>(entityState.CurrentKey, (T)entity, DataContext));
                    }
                }
            }
            return deletions;
        }

        public IEnumerable<IUpdateEntityOperation> GetUpdates(object[] entities = null, IProperty[] properties = null)
        {
            var updates = new List<UpdateEntityOperation<T>>();
            foreach (var entity in EntitiesByObject.Keys)
            {
                if (ShouldIgnoreEntity(entity, entities))
                {
                    continue;
                }
                var entityState = GetOrSetEntityState(entity);
                if (!entityState.IsNew &&
                    !entityState.MarkedForAnyDeletion &&
                    entityState.GetChangedProperties().Any())
                {
                    updates.Add(new UpdateEntityOperation<T>((T)entity, DataContext, entityState, properties?.Where(p => p.EntityConfiguration == EntityConfiguration).ToArray()));
                }
            }
            return updates;
        }

        public void AbandonChangesForEntity(T entity)
        {
            var state = FindMatchingEntityState(entity);
            if (state != null)
            {
                AbandonChangesForEntityState((IEntityState<T>)state);
            }
        }

        void ITrackingSet.AbandonChangesForEntity(object entity)
        {
            if (entity is T)
            {
                AbandonChangesForEntity((T)entity);
            }
        }

        public void AbandonChanges()
        {
            var allStates = new List<IEntityState<T>>();
            foreach (var entity in EntitiesByObject)
            {
                allStates.Add((IEntityState<T>)entity.Value);
            }
            AbandonChangesForEntityStates(allStates);
        }

        public void AbandonChangesForEntities(IEnumerable<object> entities)
        {
            var allStates = new List<IEntityState<T>>();
            foreach (var entity in entities)
            {
                var state = FindMatchingEntityState(entity);
                if (state != null)
                {
                    allStates.Add((IEntityState<T>)state);
                }
            }

            AbandonChangesForEntityStates(allStates);
        }

        public void AbandonChangesForEntityStates(IEnumerable<IEntityState<T>> allStates)
        {
            var removedStates = new List<IEntityState<T>>();
            var allStatesArr = allStates.ToList();
            for (var i = 0; i < allStatesArr.Count; i++)
            {
                var state = allStatesArr[i];
                state.AbandonChanges();
                if (state.IsNew)
                {
                    removedStates.Add(state);
                }
            }

            for (var i = 0; i < removedStates.Count; i++)
            {
                var removedState = removedStates[i];
                RemoveEntity(removedState.Entity);
            }
        }

        void ITrackingSet.AbandonChangesForEntityStates(IEnumerable<IEntityStateBase> states)
        {
            foreach (var state in states)
            {
                if (state != null && state.Entity is T)
                {
                    state.AbandonChanges();
                    if (state.IsNew)
                    {
                        RemoveEntity((T)state.Entity);
                    }
                }
            }
        }

        public void AbandonChangesForEntityState(IEntityState<T> state)
        {
            state.AbandonChanges();
            if (state.IsNew)
            {
                RemoveEntity(state.Entity);
            }
        }

        void ITrackingSet.AbandonChangesForEntityState(IEntityStateBase state)
        {
            if (state.Entity is T)
            {
                AbandonChangesForEntityState((IEntityState<T>)state);
            }
        }

        public string SerializeToJson()
        {
            return JsonConvert.SerializeObject(PrepareForJson());
        }

        public object PrepareForJson()
        {
            var allStates = EntitiesByKey.Values
                .Concat(EntitiesByObject.Values)
                .Concat(EntitiesByPersistenceKey.Values)
                .Distinct();
            return new
            {
                Type = EntityConfiguration.Name,
                EntityStates = WithChanges(allStates).Select(_=>_.PrepareForJson())
                //EntitiesByKey = WithChanges(EntitiesByKey.Values).Select(_ => _.PrepareForJson()),
                //EntitiesByObject = WithChanges(EntitiesByObject.Values).Select(_ => _.PrepareForJson()),
                //EntitiesByPersistenceKey = WithChanges(EntitiesByPersistenceKey.Values).Select(_ => _.PrepareForJson()),
                //EntitiesByRemoteKey = EntitiesByRemoteKey.Values.Select(_ => _.PrepareForJson()),
            };
        }

        private IEnumerable<IEntityStateBase> WithChanges(IEnumerable<IEntityStateBase> entityStates)
        {
            return entityStates.Where(_ => _.IsNew || _.MarkedForAnyDeletion || _.GetChangedProperties().Length > 0);
        }
    }
}