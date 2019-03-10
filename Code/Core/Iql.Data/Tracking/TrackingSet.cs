using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Conversion;
using Iql.Conversion.State;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.DataStores.InMemory;
using Iql.Data.Events;
using Iql.Data.Extensions;
using Iql.Data.Relationships;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Events;
using Iql.Entities.Exceptions;
using Iql.Entities.Extensions;
using Iql.Events;
using Newtonsoft.Json;

namespace Iql.Data.Tracking
{
    public class TrackingSet<T> : TrackingSetBase, ITrackingSet
        where T : class
    {
        public Type EntityType => typeof(T);
        private int _idCount = 0;
        private readonly ChangeIgnorer _changeIgnorer = new ChangeIgnorer();
        private readonly Dictionary<T, EntityObserver> _entityObservers = new Dictionary<T, EntityObserver>();
        private readonly PropertyChangeIgnorer _keyChangeAllower = new PropertyChangeIgnorer();

        private readonly Dictionary<object, object> _tracking = new Dictionary<object, object>();
        private EntityConfiguration<T> _entityConfiguration;

        public AutoIntegerIdStrategy AutoIntegerIdStrategy { get; set; } = AutoIntegerIdStrategy.Positive;

        public TrackingSet(DataTracker dataTracker)
        {
            DataTracker = dataTracker;
            SimplePropertyMerger = new SimplePropertyMerger(EntityConfiguration);
            EntitiesByPersistenceKey = new Dictionary<Guid, IEntityStateBase>();
            EntitiesByObject = new Dictionary<object, IEntityStateBase>();
            EntitiesByKey = new Dictionary<string, IEntityStateBase>();
            EntitiesByRemoteKey = new Dictionary<string, RemoteKeyMap>();
            PersistenceKey = EntityConfiguration.Properties.SingleOrDefault(p => p.Name == "PersistenceKey");
        }

        public IEntityState<T> AddEntity(T entity)
        {
            var rootTrackingSet = DataTracker.TrackingSet<T>();
            if (rootTrackingSet.IsMatchingEntityTracked(entity))
            {
                var state = rootTrackingSet.FindMatchingEntityState(entity);
                state.MarkedForDeletion = false;
                return (IEntityState<T>)state;
            }
            var entityType = typeof(T);
            var flattened = DataTracker.EntityConfigurationBuilder.FlattenObjectGraph(entity, entityType);
            IEntityStateBase entityState = null;
            foreach (var group in flattened)
            {
                foreach (var item in group.Value)
                {
                    var thisTrackingSet = DataTracker.TrackingSetByType(group.Key);
                    var state = thisTrackingSet.AttachEntity(item, true);
                    state.UnmarkForDeletion();
                    if (item == (object)entity)
                    {
                        entityState = state;
                        if (entityState.Entity != (object)entity)
                        {
                            throw new Exception("An item with the same key is already being tracked.");
                        }
                    }
                }
            }
            DataTracker.RelationshipObserver.ObserveAll(flattened);
            return (EntityState<T>)entityState;
        }

        IEntityStateBase ITrackingSet.AddEntity(object entity)
        {
            return AddEntity((T)entity);
        }
        public IEntityState<T> AttachEntity(T entity, bool isLocal)
        {
            IEntityStateBase entityState = null;
            var flattened = EntityConfiguration.Builder.FlattenObjectGraph(entity, typeof(T));
            foreach (var pair in flattened)
            {
                foreach (var item in pair.Value)
                {
                    var es = (DataTracker.TrackingSetByType(pair.Key) as TrackingSetBase).AttachEntityInternal(item, isLocal);
                    if (item == entity)
                    {
                        entityState = es;
                    }
                }
            }
            DataTracker.RelationshipObserver.ObserveAll(flattened);
            return (IEntityState<T>)entityState;
        }

        internal override IEntityStateBase AttachEntityInternal(object entity, bool isLocal)
        {
            if (!GlobalTracking.IsEntityTracked(entity))
            {
                GlobalTracking.RegisterAsTracked(entity, this);
            }
            else if (GlobalTracking.GetTrackingSet(entity) != this)
            {
                throw new Exception("This entity is already tracked by another context.");
            }

            var isAlreadyTracked = _tracking.ContainsKey(entity);
            if (!isAlreadyTracked)
            {
                _tracking.Add(entity, entity);
                if (isLocal &&
                    !EntityConfiguration.GetCompositeKey(entity).HasDefaultValue())
                {
                    for (var i = 0; i < EntityConfiguration.Key.Properties.Length; i++)
                    {
                        var property = EntityConfiguration.Key.Properties[i];
                        if (!DataTracker.AllowLocalKeyGeneration &&
                            property.IsReadOnly &&
                            !property.GetValue(entity).IsDefaultValue(property.TypeDefinition))
                        {
                            throw new AttemptingToAssignRemotelyGeneratedKeyException();
                        }
                    }
                }

                var entityState = CreateEntityState(entity);
                entityState.IsNew = isLocal;
                Watch(entityState.Entity);
                TrackRemoteKey(entityState, null);
                if (!isLocal)
                {
                    entityState.HardReset();
                }

                return entityState;
            }

            return (IEntityState<T>)GetEntityState(entity);
        }

        internal override void RelationshipChanged(RelationshipChangedEvent relationshipChangedEvent)
        {
            //RemoveIfNecessary(relationshipChangedEvent.Source, relationshipChangedEvent.Relationship.Source.Type);
        }

        IEntityStateBase ITrackingSet.AttachEntity(object entity, bool isLocal)
        {
            return AttachEntity((T)entity, isLocal);
        }

        public TrackingSet<T> Merge(T localEntity, T remoteEntity, bool overrideChanges, bool isRemote)
        {
            var entityState = FindMatchingEntityState(localEntity);
            if (entityState != null)
            {
                if (isRemote)
                {
                    entityState.IsNew = false;
                }
                SilentlyMerge(entityState.Entity, remoteEntity, overrideChanges);
                if (overrideChanges)
                {
                    entityState.HardReset();
                }
                else
                {
                    entityState.SoftReset(isRemote);
                }
            }
            return this;
        }

        ITrackingSet ITrackingSet.Merge(object localEntity, object remoteEntity, bool overrideChanges, bool isRemote)
        {
            return Merge((T)localEntity, (T)remoteEntity, overrideChanges, isRemote);
        }

        public IEntityState<T> Synchronise(T remoteEntity, bool overrideChanges, bool isRemote, T existingEntity)
        {
            var state = (IEntityState<T>)FindMatchingEntityState(existingEntity ?? remoteEntity);
            if (state == null)
            {
                var clone = remoteEntity.CloneAs(EntityConfiguration.Builder, EntityConfiguration.Type,
                    RelationshipCloneMode.DoNotClone);
                state = AttachEntity(
                    clone, !isRemote);
                return state;
            }
            if (state.Entity != remoteEntity)
            {
                Merge(state.Entity, remoteEntity, overrideChanges, isRemote);
            }

            return state;
        }

        IEntityStateBase ITrackingSet.Synchronise(object remoteEntity, bool overrideChanges, bool isRemote, object existingEntity)
        {
            return Synchronise((T)remoteEntity, overrideChanges, isRemote, (T)existingEntity);
        }

        public EntityConfiguration<T> EntityConfiguration => _entityConfiguration =
            _entityConfiguration ?? DataTracker.EntityConfigurationBuilder.EntityType<T>();

        public SimplePropertyMerger SimplePropertyMerger { get; }

        private Dictionary<Guid, IEntityStateBase> EntitiesByPersistenceKey { get; }
        private Dictionary<object, IEntityStateBase> EntitiesByObject { get; }
        private Dictionary<string, RemoteKeyMap> EntitiesByRemoteKey { get; }
        private Dictionary<string, IEntityStateBase> EntitiesByKey { get; }

        protected IProperty PersistenceKey { get; set; }
        //public IDataContext DataContext => DataTracker.DataContext;

        bool ITrackingSet.DifferentEntityWithSameKeyIsTracked(object entity)
        {
            return DifferentEntityWithSameKeyIsTracked((T)entity);
        }

        IEntityConfiguration ITrackingSet.EntityConfiguration => EntityConfiguration;
        public DataTracker DataTracker { get; }

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
            //if (EntityConfiguration.SpecialTypeDefinition != null && 
            //    EntityConfiguration.SpecialTypeDefinition.EntityConfiguration.Type != EntityConfiguration.Type && 
            //    entity.GetType() != EntityConfiguration.SpecialTypeDefinition.EntityConfiguration.Type)
            //{
            //    var specialTypeTrackingSet = DataTracker
            //        .TrackingSetByType(EntityConfiguration.SpecialTypeDefinition.EntityConfiguration.Type);
            //    return specialTypeTrackingSet
            //        .GetEntityStateByKey(EntityConfiguration.GetCompositeKey(entity));
            //}
            var result = TryGetEntityState(entity, false);
            return result?.State;
        }

        public IEntityStateBase GetEntityState(object entity)
        {
            var result = TryGetEntityState(entity, true);
            return result?.State;
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
            var entityState = GetEntityState(entity);
            entityState.MarkedForDeletion = true;
        }

        void ITrackingSet.RemoveEntity(object entity)
        {
            RemoveEntity((T)entity);
        }

        public void HardResetEntity(object entity)
        {
            HardReset(GetEntityState(entity));
        }

        public void HardReset(IEntityStateBase state)
        {
            state.HardReset();
        }

        public void SoftResetEntity(object entity, bool markAsNotNew)
        {
            SoftReset(GetEntityState(entity), markAsNotNew);
        }

        public void SoftReset(IEntityStateBase state, bool markAsNotNew)
        {
            state.SoftReset(markAsNotNew);
        }

        public void HardResetAll(List<IEntityStateBase> states)
        {
            for (var i = 0; i < states.Count; i++)
            {
                var state = states[i];
                HardReset(state);
            }
        }

        public void SoftResetAll(List<IEntityStateBase> states, bool markAsNotNew)
        {
            for (var i = 0; i < states.Count; i++)
            {
                var state = states[i];
                SoftReset(state, markAsNotNew);
            }
        }

        List<IEntityCrudOperationBase> IDataChangeProvider.GetInserts(IDataContext dataContext, object[] entities = null)
        {
            return GetInserts(dataContext, entities).Select(_ => (IEntityCrudOperationBase)_).ToList();
        }

        public List<AddEntityOperation<T>> GetInserts(IDataContext dataContext, object[] entities = null)
        {
            var inserts = new List<AddEntityOperation<T>>();
            foreach (var entity in EntitiesByObject.Keys)
            {
                if (ShouldIgnoreEntity(entity, entities))
                {
                    continue;
                }

                var entityState = GetEntityState(entity);
                if (entityState.IsNew && !entityState.MarkedForAnyDeletion)
                {
                    inserts.Add(new AddEntityOperation<T>((T)entity, dataContext));
                }
            }

            return inserts;
        }

        List<IEntityCrudOperationBase> IDataChangeProvider.GetDeletions(IDataContext dataContext, object[] entities = null)
        {
            return GetDeletions(dataContext, entities).Select(_ => (IEntityCrudOperationBase)_).ToList();
        }

        public List<DeleteEntityOperation<T>> GetDeletions(IDataContext dataContext, object[] entities = null)
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
                    dataContext));
            }

            foreach (var entity in EntitiesByObject.Keys)
            {
                if (ShouldIgnoreEntity(entity, entities))
                {
                    continue;
                }

                if (deletions.All(_ => _.Entity != entity))
                {
                    var entityState = GetEntityState(entity);
                    if (entityState.MarkedForAnyDeletion && !entityState.IsNew)
                    {
                        deletions.Add(new DeleteEntityOperation<T>(entityState.CurrentKey, (T)entity, dataContext));
                    }
                }
            }

            return deletions;
        }

        List<IUpdateEntityOperation> IDataChangeProvider.GetUpdates(IDataContext dataContext, object[] entities = null, IProperty[] properties = null)
        {
            return GetUpdates(dataContext, entities, properties).Select(_ => (IUpdateEntityOperation)_).ToList();
        }

        public List<UpdateEntityOperation<T>> GetUpdates(IDataContext dataContext, object[] entities = null, IProperty[] properties = null)
        {
            var updates = new List<UpdateEntityOperation<T>>();
            foreach (var entity in EntitiesByObject.Keys)
            {
                if (ShouldIgnoreEntity(entity, entities))
                {
                    continue;
                }

                var entityState = GetEntityState(entity);
                if (!entityState.IsNew &&
                    !entityState.MarkedForAnyDeletion &&
                    entityState.GetChangedProperties().Any())
                {
                    updates.Add(new UpdateEntityOperation<T>((T)entity, dataContext, entityState,
                        properties?.Where(p => p.EntityConfiguration == EntityConfiguration).ToArray()));
                }
            }

            return updates;
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

        public void Clear()
        {
            AbandonChanges();
            foreach (var observer in _entityObservers)
            {
                observer.Value.Unobserve();
            }

            _entityObservers.Clear();
            EntitiesByKey.Clear();
            EntitiesByObject.Clear();
            EntitiesByPersistenceKey.Clear();
            EntitiesByRemoteKey.Clear();
            _tracking.Clear();
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
            return this.ToJson();
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
                EntityStates = WithChanges(allStates).Select(_ => _.PrepareForJson())
                //EntitiesByKey = WithChanges(EntitiesByKey.Values).Select(_ => _.PrepareForJson()),
                //EntitiesByObject = WithChanges(EntitiesByObject.Values).Select(_ => _.PrepareForJson()),
                //EntitiesByPersistenceKey = WithChanges(EntitiesByPersistenceKey.Values).Select(_ => _.PrepareForJson()),
                //EntitiesByRemoteKey = EntitiesByRemoteKey.Values.Select(_ => _.PrepareForJson()),
            };
        }

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

        private IEntityState<T> CreateEntityState(object entity)
        {
            //if (entity is CompositeKey)
            //{
            //    return GetEntityStateByKey((CompositeKey)entity);
            //}

            //var existingEntityState = TryGetEntityState(entity, false);
            //if (existingEntityState != null && existingEntityState.State != null)
            //{
            //    return existingEntityState.State;
            //}

            var entityState = new EntityState<T>(
                DataTracker,
                (T)entity,
                typeof(T),
                EntityConfiguration);
            EntitiesByObject.Add(entity, entityState);
            if (!entityState.CurrentKey.HasDefaultValue())
            {
                EntitiesByKey.Add(entityState.CurrentKey.AsKeyString(), entityState);
            }

            if (PersistenceKey != null)
            {
                var persistenceKey = EnsurePersistenceKey(entity).Value;
                EntitiesByPersistenceKey.Add(persistenceKey, entityState);
            }

            return entityState;
        }

        private bool HasEntityState(object entity, bool entityOnly)
        {
            var result = entityOnly ? GetEntityState(entity) : FindMatchingEntityState(entity);
            return result != null;
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

            if (PersistenceKey != null && entity.GetType() == EntityType)
            {
                var persistenceKey = entity.GetPropertyValueAs<Guid>(PersistenceKey);
                if ( // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    !Equals(persistenceKey, null) && !Equals(persistenceKey, Guid.Empty) &&
                    EntitiesByPersistenceKey.ContainsKey(persistenceKey))
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

        public IEntityStateBase Restore(SerializedEntityState entityState)
        {
            var compositeKey = entityState.CurrentKey.ToCompositeKey(EntityConfiguration);

            IEntityStateBase state = null;
            var keyString = compositeKey.AsKeyString();
            if (EntitiesByKey.ContainsKey(keyString))
            {
                state = EntitiesByKey[keyString];
            }
            if (entityState.PersistenceKey != null)
            {
                var guid = entityState.PersistenceKey.EnsureGuid();
                state = guid.HasValue ? EntitiesByPersistenceKey[guid.Value] : null;
            }

            if (state == null)
            {
                var entity = Activator.CreateInstance(EntityConfiguration.Type);
                for (var i = 0; i < EntityConfiguration.Key.Properties.Length; i++)
                {
                    var key = EntityConfiguration.Key.Properties[i];
                    key.SetValue(entity, compositeKey.Keys.Single(_ => _.Name == key.PropertyName).Value);
                }
                state = CreateEntityState(entity);
            }

            state.Restore(entityState);
            return state;
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
            var state = GetEntityState(entity);
            if (state == null)
            {
                return;
            }
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

        internal void Watch(T sourceEntity)
        {
            var entity = (IEntity)sourceEntity;
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
                            var compositeKey =
                                relationship.OtherEnd.EntityConfiguration.GetCompositeKey(changeEvent.Item);

                            // If this relationship also defines some key values, make sure they are checked in the
                            // composite key
                            var sourceConstraints = relationship.OtherEnd.Constraints;
                            var targetConstraints = relationship.ThisEnd.Constraints;
                            for (var i = 0; i < sourceConstraints.Length; i++)
                            {
                                var sourceConstraint = sourceConstraints[i];
                                var targetConstraint = targetConstraints[i];
                                if (sourceConstraint.Kind.HasFlag(PropertyKind.Key) &&
                                    sourceConstraint.Relationship.OtherEnd == relationship.ThisEnd)
                                {
                                    compositeKey.Keys.Single(key => key.Name == sourceConstraint.Name)
                                        .Value = targetConstraint.GetValue(changeEvent.Owner);
                                }
                            }

                            var trackingSet = DataTracker.TrackingSetByType(relationship.OtherEnd.Type);
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
                            var state = DataTracker.AddEntity(changeEvent.Item);
                            if (state.Entity != changeEvent.Item)
                            {
                                changeEvent.ObservableListChangeEvent.Disallow = true;
                            }
                        }

                        break;
                    case RelatedListChangeKind.Removed:
                        if (changeEvent.Item != null &&
                            !DataTracker.RelationshipObserver.IsAttachedToAnotherEntity(changeEvent.Item,
                                changeEvent.ItemType))
                        {
                            DataTracker.DeleteEntity(changeEvent.Item);
                        }

                        break;
                }
            }, changeEvent.Item, changeEvent.List.Owner);
        }

        private void RemoveIfNecessary(object entity, Type entityType)
        {
            if (!DataTracker.RelationshipObserver.IsAttachedToAnotherEntity(entity,
                    entityType) ||
                DataTracker.RelationshipObserver.IsDetachedPivot(entity, entityType)
            )
            {
                var state = GetEntityState(entity);
                if (state != null)
                {
                    state.MarkedForDeletion = true;
                }
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
                    DataTracker.RelationshipObserver.RunIfNotIgnored(() =>
                        {
                            if (!_keyChangeAllower.AreAnyIgnored(new[] { property }, propertyChange.Entity))
                            {
                                var entityState = GetEntityState(propertyChange.Entity);
                                if (entityState.IsNew && !DataTracker.AllowLocalKeyGeneration)
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
                var entityState = GetEntityState(propertyChange.Entity);
                if (entityState != null)
                {
                    var propertyState = entityState.GetPropertyState(property.Name);
                    propertyState.LocalValue = propertyChange.NewValue;
                }
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

        private void SilentlyMerge(object entity, object mergeWith, bool overrideChanges)
        {
            _changeIgnorer.IgnoreAndRunIfNotAlreadyIgnored(() =>
            {
                var entityState = GetEntityState(entity);
                if (entityState == null || overrideChanges)
                {
                    SimplePropertyMerger.MergeAllProperties(
                        entity,
                        mergeWith
                    );
                }
                else
                {
                    SimplePropertyMerger.MergeUnchangedProperties(
                        entityState,
                        mergeWith
                    );
                }
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
                    !state.IsNew && !EntitiesByKey.ContainsKey(newKeyString))
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

        private static bool ShouldIgnoreEntity(object entity, object[] entities)
        {
            if (entities == null || entities.Length == 0)
            {
                return false;
            }

            return !entities.Contains(entity);
        }

        public void AbandonChangesForEntity(T entity)
        {
            var state = FindMatchingEntityState(entity);
            if (state != null)
            {
                AbandonChangesForEntityState((IEntityState<T>)state);
            }
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

        public void AbandonChangesForEntityState(IEntityState<T> state)
        {
            state.AbandonChanges();
            if (state.IsNew)
            {
                RemoveEntity(state.Entity);
            }
        }

        private IEnumerable<IEntityStateBase> WithChanges(IEnumerable<IEntityStateBase> entityStates)
        {
            return entityStates.Where(_ => _.IsNew || _.MarkedForAnyDeletion || _.GetChangedProperties().Length > 0);
        }

        private class TryGetEntityStateResult
        {
            public Guid PersistenceKey { get; set; }
            public CompositeKey CompositeKey { get; set; }
            public IEntityStateBase State { get; set; }
        }

        public int NextIdInteger(IList data, IProperty property)
        {
            if (AutoIntegerIdStrategy == AutoIntegerIdStrategy.Negative)
            {
                int max = _idCount;
                foreach (var existingEntity in data)
                {
                    var value = (int)existingEntity.GetPropertyValue(property);
                    if (value > max)
                    {
                        max = value;
                    }
                }

                _idCount++;
                return _idCount;
            }

            var min = _idCount;
            foreach (var existingEntity in data)
            {
                var value = (int)existingEntity.GetPropertyValue(property);
                if (value < min)
                {
                    min = value;
                }
            }

            _idCount--;
            return _idCount;
        }
    }
}