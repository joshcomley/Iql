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
using Iql.Extensions;

namespace Iql.Data.Tracking
{
    public class TrackingSet<T> : TrackingSetBase, ITrackingSet
        where T : class
    {
        public IEntityStateBase[] AllEntityStates()
        {
            var states = new List<IEntityStateBase>();
            foreach (var stateFn in EntitiesByPersistenceKey)
            {
                if (!_entityStateQueueInverse.ContainsKey(stateFn.Value))
                {
                    var state = stateFn.Value();
                    if (!states.Contains(state))
                    {
                        states.Add(state);
                    }
                }
            }
            foreach (var stateFn in EntitiesByObject)
            {
                if (!_entityStateQueueInverse.ContainsKey(stateFn.Value))
                {
                    var state = stateFn.Value();
                    if (!states.Contains(state))
                    {
                        states.Add(state);
                    }
                }
            }
            foreach (var stateFn in EntitiesByKey)
            {
                if (!_entityStateQueueInverse.ContainsKey(stateFn.Value))
                {
                    var state = stateFn.Value();
                    if (!states.Contains(state))
                    {
                        states.Add(state);
                    }
                }
            }
            //foreach (var state in EntitiesByRemoteKey)
            //{
            //    if (!states.Contains(state.Value.State))
            //    {
            //        states.Add(state.Value.State);
            //    }
            //}

            return states.ToArray();
        }

        public Type EntityType => typeof(T);
        private int _idCount = 0;
        private bool _changeIgnorerDelayedInitialized;
        private ChangeIgnorer _changeIgnorerDelayed;
        private ChangeIgnorer _changeIgnorer { get { if (!_changeIgnorerDelayedInitialized) { _changeIgnorerDelayedInitialized = true; _changeIgnorerDelayed = new ChangeIgnorer(); } return _changeIgnorerDelayed; } set { _changeIgnorerDelayedInitialized = true; _changeIgnorerDelayed = value; } }
        private bool _entityObserversDelayedInitialized;
        private Dictionary<T, EntityObserver> _entityObserversDelayed;
        private Dictionary<T, EntityObserver> _entityObservers { get { if (!_entityObserversDelayedInitialized) { _entityObserversDelayedInitialized = true; _entityObserversDelayed = new Dictionary<T, EntityObserver>(); } return _entityObserversDelayed; } set { _entityObserversDelayedInitialized = true; _entityObserversDelayed = value; } }
        private bool _keyChangeAllowerDelayedInitialized;
        private PropertyChangeIgnorer _keyChangeAllowerDelayed;
        private PropertyChangeIgnorer _keyChangeAllower { get { if (!_keyChangeAllowerDelayedInitialized) { _keyChangeAllowerDelayedInitialized = true; _keyChangeAllowerDelayed = new PropertyChangeIgnorer(); } return _keyChangeAllowerDelayed; } set { _keyChangeAllowerDelayedInitialized = true; _keyChangeAllowerDelayed = value; } }
        private bool _trackingDelayedInitialized;
        private Dictionary<object, object> _trackingDelayed;

        private Dictionary<object, object> _tracking { get { if (!_trackingDelayedInitialized) { _trackingDelayedInitialized = true; _trackingDelayed = new Dictionary<object, object>(); } return _trackingDelayed; } set { _trackingDelayedInitialized = true; _trackingDelayed = value; } }
        private EntityConfiguration<T> _entityConfiguration;

        public AutoIntegerIdStrategy AutoIntegerIdStrategy { get; set; } = AutoIntegerIdStrategy.Positive;

        public TrackingSet(DataTracker dataTracker)
        {
            DataTracker = dataTracker;
            SimplePropertyMerger = new SimplePropertyMerger(EntityConfiguration);
            EntitiesByPersistenceKey = new Dictionary<Guid, Func<IEntityState<T>>>();
            EntitiesByObject = new Dictionary<object, Func<IEntityState<T>>>();
            EntitiesByStateId = new Dictionary<string, Func<IEntityState<T>>>();
            EntitiesByKey = new Dictionary<string, Func<IEntityState<T>>>();
            EntitiesByRemoteKey = new Dictionary<string, RemoteKeyMap>();
        }

        public IEntityState<T> AddEntity(T entity)
        {
            var rootTrackingSet = DataTracker.TrackingSet<T>();
            if (rootTrackingSet.IsEntityTracked(entity))
            {
                var state = rootTrackingSet.FindMatchingEntityState(entity);
                state.UnmarkForDeletion();
                DataTracker.RelationshipObserver.Observe(entity, typeof(T));
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
                            throw new DuplicateKeyException("An item with the same key is already being tracked.");
                        }
                    }
                }
            }
            DataTracker.RelationshipObserver.ObserveAll(flattened);
            entityState.AttachedToTracker = true;
            return (IEntityState<T>)entityState;
        }

        IEntityStateBase ITrackingSet.AddEntity(object entity)
        {
            return AddEntity((T)entity);
        }
        public IEntityState<T> AttachEntity(T entity, bool isLocal)
        {
            //return (IEntityState<T>) AttachEntityInternal(entity, isLocal);
            IEntityStateBase entityState = null;
            var flattened = EntityConfiguration.Builder.FlattenObjectGraph(entity, typeof(T));
            foreach (var pair in flattened)
            {
                foreach (var item in pair.Value)
                {
                    var trackingSetByType = (DataTracker.TrackingSetByType(pair.Key) as TrackingSetBase);
                    var es = trackingSetByType.AttachEntityInternal(item, isLocal);
                    if (item == entity)
                    {
                        entityState = es();
                    }
                }
            }
            DataTracker.RelationshipObserver.ObserveAll(flattened);
            entityState.AttachedToTracker = true;
            return (IEntityState<T>)entityState;
        }

        internal override Func<IEntityStateBase> AttachEntityInternal(object entity, bool isLocal)
        {
            if (!GlobalTracking.IsEntityTracked(entity))
            {
                GlobalTracking.RegisterAsTracked(entity, this);
            }
            else if (GlobalTracking.GetTrackingSet(entity) != this)
            {
                throw new Exception("This entity is already tracked by another context.");
            }

            if (!_tracking.ContainsKey(entity))
            {
                _tracking.Add(entity, entity);
                if (isLocal &&
                    !EntityConfiguration.GetCompositeKey(entity).HasDefaultValue())
                {
                    for (var i = 0; i < EntityConfiguration.Key.Properties.Length; i++)
                    {
                        var property = EntityConfiguration.Key.Properties[i];
                        if (!DataTracker.AllowLocalKeyGeneration &&
                            !property.CanWrite &&
                            !property.GetValue(entity).IsDefaultValue(property.TypeDefinition))
                        {
                            throw new AttemptingToAssignRemotelyGeneratedKeyException();
                        }
                    }
                }

                var entityState = CreateEntityState(entity, isLocal);
                Watch((T)entity);
                //if (!isLocal)
                //{
                //    entityState.HardReset();
                //}
                TrackRemoteKey((T)entity, isLocal, null, !isLocal);

                return entityState;
            }

            return () => GetEntityState(entity);
        }

        internal override void RelationshipChanged(RelationshipChangedEvent relationshipChangedEvent)
        {
            var entityState = GetEntityState(relationshipChangedEvent.Source);
            if (entityState != null)
            {
                var propertyStates =
                    relationshipChangedEvent.Relationship.Source.AllProperties
                        .Select(_ => entityState.GetPropertyState(_.Name))
                        .ToArray();
                foreach (var propertyState in propertyStates)
                {
                    propertyState.PauseEvents();
                    propertyState.LocalValue = entityState.Entity.GetPropertyValueByName(propertyState.Property.Name);
                }
                foreach (var propertyState in propertyStates)
                {
                    propertyState.ResumeEvents();
                }
            }
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
                var clone = remoteEntity.CloneAs(EntityConfiguration.Builder, EntityConfiguration.Type);
                state = AttachEntity(clone, !isRemote);
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

        private Dictionary<Guid, Func<IEntityState<T>>> EntitiesByPersistenceKey { get; }
        private Dictionary<object, Func<IEntityState<T>>> EntitiesByObject { get; }
        private Dictionary<string, RemoteKeyMap> EntitiesByRemoteKey { get; }
        private Dictionary<string, Func<IEntityState<T>>> EntitiesByKey { get; }
        private Dictionary<string, Func<IEntityState<T>>> EntitiesByStateId { get; }

        protected IProperty PersistenceKey => EntityConfiguration.PersistenceKeyProperty;
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

        public bool IsEntityTracked(object entity)
        {
            return HasEntityState(entity, true);
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
            return TryGetEntityState(entity, false);
        }

        public IEntityStateBase GetEntityState(object entity)
        {
            return TryGetEntityState(entity, true);
        }

        public IEntityStateBase GetEntityStateByKey(CompositeKey key)
        {
            //var keyString = key.AsLegacyKeyString();
            //return EntitiesByKey.ContainsKey(keyString)
            //    ? EntitiesByKey[keyString]
            //    : null;
            var keyString = key.AsLegacyKeyString();
            var state = EntitiesByKey.ContainsKey(keyString)
                ? EntitiesByKey[keyString]()
                : null;
            state = (IEntityState<T>)(state ??
                                       (EntitiesByRemoteKey.ContainsKey(keyString)
                                           ? GetEntityState(EntitiesByRemoteKey[keyString].Entity)
                                           : null));
            return state;
        }

        public bool KeyIsTracked(CompositeKey key)
        {
            return GetEntityStateByKey(key) != null;
        }

        public void MarkForDelete(object entity)
        {
            var entityState = GetEntityState(entity);
            if (entityState != null)
            {
                entityState.MarkedForDeletion = true;
            }
        }

        void ITrackingSet.RemoveEntity(object entity)
        {
            UntrackEntity((T)entity);
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
            var sanitizedEntities = SanitizeEntities(entities);
            var inserts = new List<AddEntityOperation<T>>();
            for (var i = 0; i < _markedForAddition.Count; i++)
            {
                var entityState = _markedForAddition[i];
                if (CanPerformOperation(sanitizedEntities, entityState))
                {
                    inserts.Add(new AddEntityOperation<T>(entityState, dataContext));
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
            var sanitizedEntities = SanitizeEntities(entities);
            var deletions = new List<DeleteEntityOperation<T>>();
            for (var i = 0; i < _markedForDeletion.Count; i++)
            {
                var entityState = _markedForDeletion[i];
                if (CanPerformOperation(sanitizedEntities, entityState))
                {
                    deletions.Add(new DeleteEntityOperation<T>(
                        entityState.RemoteKey,
                        entityState,
                        dataContext));
                }
            }

            return deletions;
        }

        private bool CanPerformOperation(List<SanitizedEntity> sanitizedEntities, IEntityState<T> entity)
        {
            if(sanitizedEntities == null)
            {
                return true;
            }

            for (var i = 0; i < sanitizedEntities.Count; i++)
            {
                var item = sanitizedEntities[i];
                if (item.UseKey)
                {
                    if (entity.RemoteKey.Matches(item.Key))
                    {
                        return true;
                    }
                }
                else if (item.EntityState == entity)
                {
                    return true;
                }
            }

            return false;
        }

        class SanitizedEntity
        {
            public bool UseKey { get; }
            public CompositeKey Key { get; }
            public IEntityStateBase EntityState { get; }

            public SanitizedEntity(bool useKey, CompositeKey key, IEntityStateBase entityState)
            {
                UseKey = useKey;
                Key = key;
                EntityState = entityState;
            }
        }
        private List<SanitizedEntity> SanitizeEntities(object[] entities)
        {
            if(entities == null)
            {
                return null;
            }
            var list = new List<SanitizedEntity>();
            for (var i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                if (entity is CompositeKey key)
                {
                    list.Add(new SanitizedEntity(true, key, null));
                }
                else if (entity is IEntityStateBase state)
                {
                    list.Add(new SanitizedEntity(false, state.RemoteKey, state));
                }
                else
                {
                    var foundState = GetEntityState(entity);
                    if (foundState != null)
                    {
                        list.Add(new SanitizedEntity(false, foundState.RemoteKey, foundState));
                    }
                }
            }

            return list;
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
                    updates.Add(new UpdateEntityOperation<T>((IEntityState<T>)entityState, dataContext,
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

        public void AbandonChanges(object[] entities = null, IProperty[] properties = null)
        {
            var allStates = new List<IEntityState<T>>();
            foreach (var entity in EntitiesByObject)
            {
                if (!_entityStateQueueInverse.ContainsKey(entity.Value))
                {
                    var state = entity.Value();
                    if (entities != null && !entities.Contains(state.Entity))
                    {
                        continue;
                    }
                    allStates.Add(state);
                }
            }

            AbandonChangesForEntityStates(allStates, properties);
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

        void ITrackingSet.AbandonChangesForEntityStates(IEnumerable<IEntityStateBase> states, IProperty[] properties)
        {
            foreach (var state in states)
            {
                if (state != null && state.Entity is T)
                {
                    state.AbandonPropertyChanges(properties);
                    if (state.IsNew)
                    {
                        UntrackEntity((T)state.Entity);
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
            EntitiesByStateId.Clear();
            EntitiesByPersistenceKey.Clear();
            EntitiesByRemoteKey.Clear();
            _tracking.Clear();
        }

        public void NotifySaveApplied(object[] entities, IProperty[] properties, List<IEntityStateBase> failedEntitySaves)
        {

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
            return new
            {
                Type = EntityConfiguration.Name,
                EntityStates = GetChangedStates().Select(_ => _.PrepareForJson())
                //EntitiesByKey = WithChanges(EntitiesByKey.Values).Select(_ => _.PrepareForJson()),
                //EntitiesByObject = WithChanges(EntitiesByObject.Values).Select(_ => _.PrepareForJson()),
                //EntitiesByPersistenceKey = WithChanges(EntitiesByPersistenceKey.Values).Select(_ => _.PrepareForJson()),
                //EntitiesByRemoteKey = EntitiesByRemoteKey.Values.Select(_ => _.PrepareForJson()),
            };
        }

        public bool LiveTracking => DataTracker.LiveTracking;

        private List<IEntityState<T>> _markedForDeletion = new List<IEntityState<T>>();
        public ITrackingSet SetMarkedForDeletion(IEntityStateBase state, bool isMarkedForDeletion)
        {
            if (isMarkedForDeletion)
            {
                if (!_markedForDeletion.Contains(state))
                {
                    _markedForDeletion.Add((IEntityState<T>)state);
                }
            }
            else
            {
                _markedForDeletion.Remove((IEntityState<T>)state);
            }

            return this;
        }

        private List<IEntityState<T>> _markedForAddition = new List<IEntityState<T>>();
        public ITrackingSet SetMarkedForAddition(IEntityStateBase state, bool isMarkedForAddition)
        {
            if (isMarkedForAddition)
            {
                if (!_markedForAddition.Contains(state))
                {
                    _markedForAddition.Add((IEntityState<T>)state);
                }
            }
            else
            {
                _markedForAddition.Remove((IEntityState<T>)state);
            }

            return this;
        }

        public IEntityStateBase[] GetChangedStates()
        {
            var allStates = AllEntityStates()
              .Distinct();
            return WithChanges(allStates).ToArray();
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

        private readonly Dictionary<object, Func<IEntityState<T>>> _entityStateQueue = new Dictionary<object, Func<IEntityState<T>>>();
        private readonly Dictionary<Func<IEntityState<T>>, object> _entityStateQueueInverse = new Dictionary<Func<IEntityState<T>>, object>();
        private Func<IEntityState<T>> CreateEntityState(object entity, bool isNew)
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
            var id = Guid.NewGuid();
            IEntityState<T> entityState = null;
            Func<IEntityState<T>> fn = null;
            fn = () =>
            {
                if (_entityStateQueue.ContainsKey(entity))
                {
                    _entityStateQueue.Remove(entity);
                    _entityStateQueueInverse.Remove(fn);
                    entityState = new EntityState<T>(
                        this,
                        (T)entity,
                        typeof(T),
                        EntityConfiguration,
                        isNew,
                        LiveTracking,
                        id);
                    return entityState;
                }
                return entityState;
            };
            TrackState(fn, entity, id);
            _entityStateQueueInverse.Add(fn, entity);
            _entityStateQueue.Add(entity, fn);
            return fn;
        }

        private void TrackState(Func<IEntityState<T>> entityState, object entity, Guid id)
        {
            if (!EntitiesByObject.ContainsKey(entity))
            {
                EntitiesByObject.Add(entity, entityState);
            }
            var stateId = id.ToString();
            if (!EntitiesByStateId.ContainsKey(stateId))
            {
                EntitiesByStateId.Add(stateId, entityState);
            }

            var localKey = EntityConfiguration.GetCompositeKey(entity);
            var hasKey = !localKey.HasDefaultValue();
            if (hasKey)
            {
                var key = localKey.AsLegacyKeyString();
                if (!EntitiesByKey.ContainsKey(key))
                {
                    EntitiesByKey.Add(key, entityState);
                }
            }
            else if (PersistenceKey != null)
            {
                var persistenceKey = EnsurePersistenceKey(entity).Value;
                if (!EntitiesByPersistenceKey.ContainsKey(persistenceKey))
                {
                    EntitiesByPersistenceKey.Add(persistenceKey, entityState);
                }
            }
        }

        private bool HasEntityState(object entity, bool entityOnly)
        {
            var result = entityOnly ? GetEntityState(entity) : FindMatchingEntityState(entity);
            return result != null;
        }

        private bool IsEntityNew(object entity)
        {
            var state = DataTracker.GetEntityState(entity);
            if (state != null)
            {
                return state.IsNew;
            }

            return false;
        }

        private IEntityState<T> TryGetEntityState(object entity, bool entityOnly)
        {
            if (_entityStateQueue.ContainsKey(entity))
            {
                return _entityStateQueue[entity]();
            }

            if (EntitiesByObject.ContainsKey(entity))
            {
                return EntitiesByObject[entity]();
            }

            if (entityOnly)
            {
                return null;
            }

            if (PersistenceKey != null && entity.GetType() == EntityType)
            {
                var persistenceKey = entity.GetPropertyValueAs<Guid>(PersistenceKey);
                if ( // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    !Equals(persistenceKey, null) && !Equals(persistenceKey, Guid.Empty) &&
                    EntitiesByPersistenceKey.ContainsKey(persistenceKey))
                {
                    return EntitiesByPersistenceKey[persistenceKey]();
                }
            }

            return (IEntityState<T>)GetEntityStateByKey(EntityConfiguration.GetCompositeKey(entity));
        }

        public IEntityStateBase Restore(SerializedEntityState entityState)
        {
            var compositeKey = entityState.CurrentKey.ToCompositeKey(EntityConfiguration);

            IEntityStateBase state = null;
            var stateId = entityState.Id == null ? Guid.NewGuid().ToString() : entityState.Id.ToString();
            if (EntitiesByStateId.ContainsKey(stateId))
            {
                state = EntitiesByStateId[stateId]();
            }
            var keyString = compositeKey.AsLegacyKeyString();
            if (state == null && EntitiesByKey.ContainsKey(keyString))
            {
                state = EntitiesByKey[keyString]();
            }
            if (state == null && entityState.PersistenceKey != null)
            {
                var guid = entityState.PersistenceKey.EnsureGuid();
                state = guid.HasValue ? EntitiesByPersistenceKey[guid.Value]() : null;
            }

            if (state == null)
            {
                var entity = Activator.CreateInstance(EntityConfiguration.Type);
                for (var i = 0; i < EntityConfiguration.Key.Properties.Length; i++)
                {
                    var key = EntityConfiguration.Key.Properties[i];
                    key.SetValue(entity, compositeKey.Keys.Single(_ => _.Name == key.Name).Value);
                }
                state = CreateEntityState(entity, true)();
            }
            else
            {
                TrackState(() => (IEntityState<T>)state, state.Entity, state.Id);
            }
            state.Restore(entityState);
            return state;
        }

        public void RemoveEntityByKey(CompositeKey compositeKey)
        {
            var keyString = compositeKey.AsLegacyKeyString();
            if (EntitiesByRemoteKey.ContainsKey(keyString))
            {
                var mapping = EntitiesByRemoteKey[keyString];
                EntitiesByRemoteKey.Remove(keyString);
                if (mapping.Entity != null)
                {
                    UntrackEntity((T)mapping.Entity);
                }
            }
        }

        public void UntrackEntity(T entity)
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

            EntitiesByKey.Remove(state.LocalKey.AsLegacyKeyString());
            EntitiesByObject.Remove(entity);
            if (state.PersistenceKey.HasValue)
            {
                EntitiesByPersistenceKey.Remove(state.PersistenceKey.Value);
            }

            _tracking.Remove(entity);
            state.AttachedToTracker = false;
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
                var observer = new EntityObserver(EntityConfiguration, entity);
                _entityObservers.Add(sourceEntity, observer);
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
                                if (sourceConstraint.Kind.HasFlag(IqlPropertyKind.Key) &&
                                    sourceConstraint.Relationship.OtherEnd == relationship.ThisEnd)
                                {
                                    compositeKey.Keys.Single(key => key.Name == ((IMetadata)sourceConstraint).Name)
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
                            var relationship = this.EntityConfiguration.FindRelationshipByName(changeEvent.List.PropertyName);
                            if (relationship.OtherEnd.Property.Nullable == false)
                            {
                                var state = DataTracker.GetEntityState(changeEvent.Item);
                                if (state != null)
                                {
                                    if (state.IsNew && !state.IsAttachedToGraph)
                                    {
                                        DataTracker.DeleteEntity(changeEvent.Item);
                                    }
                                    else if (!state.MarkedForDeletion)
                                    {
                                        state.MarkForCascadeDeletion(changeEvent.Item, relationship.Relationship);
                                    }
                                }
                            }
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
                if (property.Kind.HasFlag(IqlPropertyKind.Key) && !property.CanWrite)
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

                if (property.Kind.HasFlag(IqlPropertyKind.Key))
                {
                    Reindex(propertyChange.Entity, false);
                }
            }, propertyChange.Entity);
        }

        private void EntityPropertyChanging(IPropertyChangeEvent propertyChange)
        {
            var property = EntityConfiguration.FindProperty(propertyChange.PropertyName);
            if ((property.Kind.HasFlag(IqlPropertyKind.Key) ||
                 property.Kind.HasFlag(IqlPropertyKind.Primitive)) &&
                !property.Kind.HasFlag(IqlPropertyKind.Relationship) &&
                !property.Kind.HasFlag(IqlPropertyKind.RelationshipKey))
            {
                var entityState = GetEntityState(propertyChange.Entity);
                if (entityState != null)
                {
                    var propertyState = entityState.GetPropertyState(property.Name);
                    propertyState.LocalValue = propertyChange.NewValue;
                }
            }
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
                Reindex(entity, overrideChanges);
            }, entity);
        }

        private void Reindex(object entity, bool updateRemoteKey)
        {
            if (EntitiesByObject.ContainsKey(entity))
            {
                var state = EntitiesByObject[entity]();

                var newKey = EntityConfiguration.GetCompositeKey(entity);
                var oldKey = state.LocalKey;
                var oldKeyString = oldKey.AsLegacyKeyString();
                var newKeyString = newKey.AsLegacyKeyString();
                if (newKeyString != oldKeyString ||
                    !state.IsNew && !EntitiesByKey.ContainsKey(newKeyString))
                {
                    if (EntitiesByKey.ContainsKey(oldKeyString))
                    {
                        EntitiesByKey.Remove(oldKeyString);
                    }

                    if (state.HasValidKey())
                    {
                        EntitiesByKey.Add(newKeyString, () => state);
                    }

                    state.LocalKey = newKey;
                }

                TrackRemoteKey(state.Entity, state.IsNew, oldKey, updateRemoteKey);
            }
        }

        private void TrackRemoteKey(
            //IEntityState<T> state,
            T entity,
            bool isNew,
            CompositeKey oldKey,
            bool useLocalKey = false)
        {
            var compositeKey = EntityConfiguration.GetCompositeKey(entity);
            var keyString = compositeKey.AsLegacyKeyString();
            if (EntitiesByKey.ContainsKey(keyString) && EntitiesByKey[keyString]().Entity != entity)
            {
                throw new DuplicateKeyException();
            }
            if (EntitiesByRemoteKey.ContainsKey(keyString))
            {
                if (EntitiesByRemoteKey[keyString].Entity != entity)
                {
                    throw new DuplicateKeyException();
                }
            }
            else if (!isNew)
            {
                EntitiesByRemoteKey.Add(keyString, new RemoteKeyMap(entity, compositeKey));
            }
            //var state = GetEntityState(entity);
            //if (!isNew && CompositeKey.IsValid(EntityConfiguration, entity, isNew))
            //{
            //    var keyString = (useLocalKey ? state.LocalKey : state.RemoteKey).AsLegacyKeyString();
            //    if (EntitiesByRemoteKey.ContainsKey(keyString))
            //    {
            //        var map = EntitiesByRemoteKey[keyString];
            //        if (map.Entity == null)
            //        {
            //            map.Entity = entity;
            //        }
            //        else if (map.Entity != entity)
            //        {
            //            throw new DuplicateKeyException();
            //        }
            //    }
            //    else
            //    {
            //        EntitiesByRemoteKey.Add(keyString, new RemoteKeyMap(state, state.LocalKey));
            //    }

            //    if (oldKey != null)
            //    {
            //        var oldKeyString = oldKey.AsLegacyKeyString();
            //        if (oldKeyString != keyString && EntitiesByRemoteKey.ContainsKey(oldKeyString))
            //        {
            //            EntitiesByRemoteKey.Remove(oldKeyString);
            //        }
            //    }
            //}
            //else if (oldKey != null)
            //{
            //    var keyString = state.LocalKey.AsLegacyKeyString();
            //    var oldKeyString = oldKey.AsLegacyKeyString();
            //    if (oldKeyString != keyString && EntitiesByRemoteKey.ContainsKey(oldKeyString))
            //    {
            //        var remoteKeyMap = EntitiesByRemoteKey[oldKeyString];
            //        remoteKeyMap.OldPropertyValues = state.PropertyStates.Select(p => p.Copy()).ToArray();
            //        //remoteKeyMap.State = null;
            //    }
            //}

            ////if (!state.IsNew && state.CurrentKey.HasDefaultValue())
            ////{
            ////    state.IsNew = true;
            ////}
            //if (state.IsNew)
            //{
            //    var keyString = state.LocalKey.AsLegacyKeyString();
            //    if (EntitiesByRemoteKey.ContainsKey(keyString))
            //    {
            //        var map = EntitiesByRemoteKey[keyString];
            //        if (map.Entity == null)
            //        {
            //            state.IsNew = false;
            //            foreach (var propertyState in map.OldPropertyValues)
            //            {
            //                var newPropertyState = state.GetPropertyState(propertyState.Property.Name);
            //                newPropertyState.RemoteValue = propertyState.RemoteValue;
            //                newPropertyState.LocalValue = propertyState.Property.GetValue(state.Entity);
            //            }

            //            map.OldPropertyValues = null;
            //            map.Entity = entity;
            //        }
            //        else if (map.Entity != entity)
            //        {
            //            throw new DuplicateKeyException();
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

        public void AbandonChangesForEntityStates(IEnumerable<IEntityState<T>> allStates, IProperty[] properties = null)
        {
            var removedStates = new List<IEntityState<T>>();
            var allStatesArr = allStates.ToList();
            for (var i = 0; i < allStatesArr.Count; i++)
            {
                var state = allStatesArr[i];
                state.AbandonPropertyChanges(properties);
                if (state.IsNew)
                {
                    removedStates.Add(state);
                }
            }

            for (var i = 0; i < removedStates.Count; i++)
            {
                var removedState = removedStates[i];
                removedState.MarkedForDeletion = true;
                //UntrackEntity(removedState.Entity);
            }
        }

        public void AbandonChangesForEntityState(IEntityState<T> state)
        {
            state.AbandonChanges();
            if (state.IsNew)
            {
                state.MarkedForDeletion = true;
                //UntrackEntity(state.Entity);
            }
        }

        private IEnumerable<IEntityStateBase> WithChanges(IEnumerable<IEntityStateBase> entityStates)
        {
            return entityStates.Where(_ => _.IsNew || _.MarkedForAnyDeletion || _.GetChangedProperties().Length > 0);
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

        public void Dispose()
        {
            foreach (var state in AllEntityStates())
            {
                state.Dispose();
            }
        }

        public void NotifyMarkedForDeletionChanged(bool isMarkedForDeletion, IEntityStateBase entityState)
        {
            DataTracker.RelationshipObserver.NotifyMarkedForDeletionChange(isMarkedForDeletion, entityState);
        }
    }
}