using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Conversion.State;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Extensions;
using Iql.Data.Relationships;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Events;
using Iql.Events;
using Iql.Extensions;
using Newtonsoft.Json;

namespace Iql.Data.Tracking
{
    public class DataTracker : IJsonSerializable, IDataChangeProvider, IDisposable, ISnapshotManager
    {
        //public EventEmitter<OfflineChangeStateChangedEvent> StateChanged { get; } = new EventEmitter<OfflineChangeStateChangedEvent>();
        public string SynchronicityKey => Name;
        public DataTrackerKind Kind { get; }
        public DataTracker(
            DataTrackerKind kind,
            IEntityConfigurationBuilder entityConfigurationBuilder,
            string name,
            bool offline = false,
            bool silent = false)
        {
            Kind = kind;
            EntityConfigurationBuilder = entityConfigurationBuilder;
            Name = name;
            Offline = offline;
            SetsMap = new Dictionary<string, ITrackingSet>();
            Sets = new List<ITrackingSet>();
            RelationshipObserver = new RelationshipObserver(this);
            RelationshipObserver.RelationshipChanged.Subscribe(RelationshipChanged);
            //Id = Guid.NewGuid().ToString();
            if (!silent)
            {
                _allDataTrackers.Add(this);
            }
        }

        private readonly Dictionary<IPropertyState, bool> _propertiesChangedSinceLastSnapshot = new Dictionary<IPropertyState, bool>();
        private readonly Dictionary<IEntityStateBase, bool> _entitiesPendingInsertSinceLastSnapshot = new Dictionary<IEntityStateBase, bool>();
        private readonly Dictionary<IEntityStateBase, bool> _entitiesPendingDeleteSinceLastSnapshot = new Dictionary<IEntityStateBase, bool>();

        private readonly Dictionary<IPropertyState, bool> _propertiesChangedSinceLastSave = new Dictionary<IPropertyState, bool>();
        private readonly Dictionary<IEntityStateBase, bool> _entitiesPendingInsertSinceLastSave = new Dictionary<IEntityStateBase, bool>();
        private readonly Dictionary<IEntityStateBase, bool> _entitiesPendingDeleteSinceLastSave = new Dictionary<IEntityStateBase, bool>();

        private readonly List<TrackerSnapshot> _snapshots = new List<TrackerSnapshot>();
        private readonly List<TrackerSnapshot> _removedSnapshots = new List<TrackerSnapshot>();

        private bool _ignoreChangedSinceSnapshotChanges = false;
        private bool _ignoreChanges = false;
        private bool _hasChanges = false;
        private bool _hasChangesSinceSnapshot = false;

        public TrackerSnapshot CurrentSnapshot => _snapshots.LastOrDefault();

        public void ClearSnapshots()
        {
            while (_snapshots.Any())
            {
                RemoveLastSnapshot();
            }
        }

        private bool HasChangedSinceSnapshot(TrackerSnapshot snapshot = null)
        {
            if (_propertiesChangedSinceLastSnapshot.Count > 0)
            {
                return true;
            }
            snapshot = snapshot ?? NewTrackerSnapshot();
            var newEntities = snapshot.EntitiesPendingInsert;
            var deletedEntities = snapshot.EntitiesPendingDelete;
            if (CurrentSnapshot != null)
            {
                if (newEntities.Length != CurrentSnapshot.EntitiesPendingInsert.Length)
                {
                    return true;
                }
                if (deletedEntities.Length != CurrentSnapshot.EntitiesPendingDelete.Length)
                {
                    return true;
                }
                foreach (var entity in newEntities)
                {
                    if (!CurrentSnapshot.EntitiesPendingInsert.Contains(entity))
                    {
                        return true;
                    }
                }
                foreach (var entity in deletedEntities)
                {
                    if (!CurrentSnapshot.EntitiesPendingDelete.Contains(entity))
                    {
                        return true;
                    }
                }
            }
            else
            {
                return newEntities.Length > 0 || deletedEntities.Length > 0;
            }

            return false;
        }

        public TrackerSnapshot AddSnapshot()
        {
            if (!HasChangesSinceSnapshot)
            {
                return null;
            }
            var snapshot = NewTrackerSnapshot();
            snapshot.Id = Guid.NewGuid();
            _ignoreChangedSinceSnapshotChanges = true;
            foreach (var item in _propertiesChangedSinceLastSnapshot)
            {
                snapshot.Values.Add(item.Key, new PropertySnapshot
                {
                    PreviousValue = item.Key.SnapshotValue,
                    CurrentValue = item.Key.LocalValue
                });
                item.Key.AddSnapshot();
            }
            _ignoreChangedSinceSnapshotChanges = false;
            ResetSnapshotState(true);
            _snapshots.Add(snapshot);
            return snapshot;
        }

        private void ResetSnapshotState(bool clearRemovedSnapshots)
        {
            if (clearRemovedSnapshots)
            {
                _removedSnapshots.Clear();
            }
            _propertiesChangedSinceLastSnapshot.Clear();
            _entitiesPendingInsertSinceLastSnapshot.Clear();
            _entitiesPendingDeleteSinceLastSnapshot.Clear();
        }

        private TrackerSnapshot NewTrackerSnapshot()
        {
            var snapshot = new TrackerSnapshot();
            snapshot.EntitiesPendingInsert = _entitiesPendingInsertSinceLastSave.Select(_ => _.Key).ToArray();
            snapshot.EntitiesPendingDelete = _entitiesPendingDeleteSinceLastSave.Select(_ => _.Key).ToArray();
            return snapshot;
        }

        public bool UndoChanges(object[] entities = null, IProperty[] properties = null)
        {
            return GoToLastSnapshot(false, true, false, entities, properties);
        }

        public bool RemoveLastSnapshot(SnapshotRemoveKind? kind = null)
        {
            kind = kind ?? SnapshotRemoveKind.None;
            switch (kind)
            {
                case SnapshotRemoveKind.GoToPreSnapshotValues:
                    return GoToLastSnapshot(true, true, true);
                case SnapshotRemoveKind.GoToSnapshotValues:
                    return GoToLastSnapshot(true, true, false);
            }
            return GoToLastSnapshot(true, false, false);
        }

        public bool RestoreNextAbandonedSnapshot()
        {
            if (!HasRestorableSnapshot)
            {
                return false;
            }

            var snapshot = NewTrackerSnapshot();
            var last = _removedSnapshots.Last();
            _removedSnapshots.Remove(last);
            foreach (var item in last.Values)
            {
                item.Key.Property.SetValue(item.Key.EntityState.Entity, item.Value.CurrentValue);
                item.Key.SetSnapshotValue(item.Value.CurrentValue);
            }

            _snapshots.Add(last);
            RestoreNewAndDeleted(snapshot, last);
            ResetSnapshotState(false);
            return true;
        }

        public bool HasSnapshot => CurrentSnapshot != null;

        public bool HasRestorableSnapshot => _removedSnapshots.Count > 0;

        private bool GoToLastSnapshot(bool remove, bool? undoChanges, bool? usePreSnapshotValue, object[] entities = null, IProperty[] properties = null)
        {
            var snapshot = NewTrackerSnapshot();
            _ignoreChangedSinceSnapshotChanges = true;
            var unsetSnapshotValue = false;
            if (_snapshots.Any())
            {
                var last = _snapshots.Last();
                if (remove)
                {
                    _snapshots.Remove(last);
                    _removedSnapshots.Add(last);
                    if (!_snapshots.Any())
                    {
                        unsetSnapshotValue = true;
                    }
                }
                foreach (var item in last.Values)
                {
                    if (entities != null && !entities.Contains(item.Key.EntityState.Entity))
                    {
                        continue;
                    }
                    if (properties != null && !properties.Contains(item.Key.Property))
                    {
                        continue;
                    }

                    if (undoChanges == true)
                    {
                        item.Key.Property.SetValue(item.Key.EntityState.Entity, usePreSnapshotValue == true ? item.Value.PreviousValue : item.Value.CurrentValue);
                    }
                    if (unsetSnapshotValue)
                    {
                        item.Key.ClearSnapshotValue();
                    }
                    else
                    {
                        item.Key.SetSnapshotValue(item.Value.CurrentValue);
                    }
                }

                if (undoChanges == true)
                {
                    if (usePreSnapshotValue == true)
                    {
                        var previous = _snapshots.Where(_ => _ != last).LastOrDefault();
                        if (previous == null)
                        {
                            previous = new TrackerSnapshot();
                        }
                        RestoreNewAndDeleted(snapshot, previous);
                    }
                    else
                    {
                        RestoreNewAndDeleted(snapshot, last);
                    }
                }
                ResetSnapshotState(false);
                _ignoreChangedSinceSnapshotChanges = false;
                return true;
            }
            AbandonChanges(entities, properties);
            ResetSnapshotState(false);
            _ignoreChangedSinceSnapshotChanges = false;
            return false;
        }

        private void RestoreNewAndDeleted(TrackerSnapshot fromSnapshot, TrackerSnapshot toSnapshot)
        {
            var itemsToDelete = toSnapshot.EntitiesPendingDelete.Where(_ => !fromSnapshot.EntitiesPendingDelete.Contains(_)).ToArray();
            var itemsToAdd = toSnapshot.EntitiesPendingInsert;//.Where(_ => !fromSnapshot.NewEntities.Contains(_)).ToArray();
            var itemsToUnAdd = fromSnapshot.EntitiesPendingInsert.Where(_ => !toSnapshot.EntitiesPendingInsert.Contains(_)).ToArray();
            for (var i = 0; i < itemsToAdd.Length; i++)
            {
                var entity = itemsToAdd[i];
                Sets.Single(_ => _.EntityType == entity.EntityType).AddEntity(entity.Entity);
            }

            for (var i = 0; i < itemsToDelete.Length; i++)
            {
                var entity = itemsToDelete[i];
                entity.MarkedForDeletion = true;
            }

            for (var i = 0; i < itemsToUnAdd.Length; i++)
            {
                var entity = itemsToUnAdd[i];
                Sets.Single(_ => _.EntityType == entity.EntityType).RemoveEntity(entity.Entity);
            }
        }

        public bool RevertToSnapshot()
        {
            if (CurrentSnapshot != null)
            {
                return UndoChanges();
            }
            return false;
        }

        private void RelationshipChanged(RelationshipChangedEvent relationshipChangedEvent)
        {
            var trackingSet = TrackingSetByType(relationshipChangedEvent.Relationship.Source.EntityConfiguration.Type);
            (trackingSet as TrackingSetBase).RelationshipChanged(relationshipChangedEvent);
        }

        public Dictionary<string, ITrackingSet> SetsMap { get; set; }
        public List<ITrackingSet> Sets { get; set; }

        public bool AllowLocalKeyGeneration => Offline;
        public IEntityConfigurationBuilder EntityConfigurationBuilder { get; }
        public string Name { get; }
        public bool Offline { get; }
        //public IDataContext DataContext { get; set; }

        public IRelationshipObserver RelationshipObserver { get; }

        private static List<DataTracker> _allDataTrackers { get; }
            = new List<DataTracker>();


        public async Task<bool> ClearStateAsync(IPersistState persistState)
        {
            if (persistState != null)
            {
                var success = await persistState.DeleteStateAsync(PersistStateKey());
                if (success)
                {
                    EmitStateChangedEvent();
                }

                return success;
            }

            return false;
        }

        public async Task<bool> SaveStateAsync(IPersistState persistState)
        {
            if (persistState != null)
            {
                return await persistState.SaveStateAsync(PersistStateKey(), SerializeToJson());
            }

            return false;
        }

        public async Task<bool> RestoreStateAsync(IPersistState persistState)
        {
            if (persistState != null)
            {
                var state = await persistState.FetchStateAsync(PersistStateKey());
                RestoreFromJson(state);
                EmitStateChangedEvent();
                return true;
            }
            return false;
        }

        private string PersistStateKey()
        {
            return $"DataTracker-{Kind.ToString()}-{Name}";
        }

        public string SerializeToJson()
        {
            return this.ToJson();
        }

        public object PrepareForJson()
        {
            var trackingSets = Sets
                .Where(_ => _.GetChangedStates().Length > 0)
                .ToArray();
            if (trackingSets.Length == 0)
            {
                return new { };
            }
            return new
            {
                Sets = trackingSets
                        .Select(_ => _.PrepareForJson())
            };
        }

        public int GetPendingDependencyCount(object entity, Type entityType = null)
        {
            var count = 0;
            var flattened =
                GraphFlattener.FlattenDependencyGraphInternal(
                    EntityConfigurationBuilder,
                    entity,
                    entityType,
                    (obj, relationship) =>
                    {
                        var value = relationship.Property.GetValue(obj);
                        if (value == null)
                        {
                            var key = relationship.GetCompositeKey(obj, true);
                            var type = relationship.OtherSide.Type;
                            return TrackingSetByType(type).FindMatchingEntityState(key)?.Entity;
                        }
                        return value;
                    });
            foreach (var item in flattened)
            {
                var tracking = TrackingSetByType(item.Key);
                foreach (var dependency in item.Value)
                {
                    if (dependency == entity)
                    {
                        continue;
                    }

                    var state = tracking.FindMatchingEntityState(dependency);
                    if (state != null && state.IsNew)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public List<IEntityCrudOperationBase> GetInserts(IDataContext dataContext = null, object[] entities = null)
        {
            var inserts = new List<IEntityCrudOperationBase>();
            foreach (var set in Sets)
            {
                inserts.AddRange(set.GetInserts(dataContext, entities));
            }

            inserts = inserts
                .OrderBy(operation => GetPendingDependencyCount(operation.EntityState.Entity, operation.EntityType))
                .ToList();
            return inserts;
        }

        public List<IUpdateEntityOperation> GetUpdates(IDataContext dataContext = null, object[] entities = null, IProperty[] properties = null)
        {
            var updates = new List<IUpdateEntityOperation>();
            foreach (var set in Sets)
            {
                updates.AddRange(set.GetUpdates(dataContext, entities, properties));
            }

            return updates;
        }

        public List<IEntityCrudOperationBase> GetDeletions(IDataContext dataContext = null, object[] entities = null)
        {
            var deletions = new List<IEntityCrudOperationBase>();
            foreach (var set in Sets)
            {
                deletions.AddRange(set.GetDeletions(dataContext, entities));
            }

            return deletions;
        }

        public ITrackingSet TrackingSetByType(Type type)
        {
            if (!SetsMap.ContainsKey(type.Name))
            {
                var set = (ITrackingSet)typeof(DataTracker).GetRuntimeMethods()
                    .First(m => m.Name == nameof(TrackingSet))
                    .InvokeGeneric(this, new object[] { }, type);
                return set;
            }

            return SetsMap[type.Name];
        }

        public IEntityStateBase DeleteEntity<T>(T entity)
            where T : class
        {
            var entityType = ResolveTrackingSet(entity, out var set);
            set.MarkForDelete(entity);
            RelationshipObserver.DeleteRelationships(entity, entityType);
            var entityState = set.FindMatchingEntityState(entity);
            return entityState;
        }

        public IEntityStateBase AddEntity<T>(T entity)
        where T : class
        {
            var entityType = ResolveTrackingSet(entity, out var set);
            return TrackingSetByType(entityType).AddEntity(entity);
        }

        public TrackingSet<T> TrackingSet<T>() where T : class
        {
            var type = typeof(T);
            if (!SetsMap.ContainsKey(type.Name))
            {
                var set = new TrackingSet<T>(this);
                SetsMap[type.Name] = set;
                Sets.Add(set);
            }

            return (TrackingSet<T>)SetsMap[type.Name];
        }

        public bool EntityWithSameKeyIsBeingTracked(object entity, Type entityType)
        {
            return TrackingSetByType(entityType).DifferentEntityWithSameKeyIsTracked(entity);
        }

        public bool KeyIsTracked(CompositeKey key, Type entityType)
        {
            return TrackingSetByType(entityType).IsMatchingEntityTracked(key);
        }

        public bool IsTracked(object entity, Type entityType = null)
        {
            return GetTrackingSetForEntity(entity, entityType) != null;
        }

        public ITrackingSet GetTrackingSetForEntity(object entity, Type entityType = null)
        {
            if (entity == null)
            {
                return null;
            }

            var trackingSetByType = TrackingSetByType(entityType ?? entity.GetType());
            var isTracked = trackingSetByType.IsTracked(entity);
            if (isTracked)
            {
                return trackingSetByType;
            }

            foreach (var trackingSet in Sets)
            {
                if (trackingSet != trackingSetByType && trackingSet.IsTracked(entity))
                {
                    return trackingSet;
                }
            }

            return null;
        }

        public bool IsMarkedForDeletion(object entity, Type entityType)
        {
            var set = TrackingSetByType(entityType);
            return set.FindMatchingEntityState(entity).MarkedForDeletion;
        }

        public bool IsMarkedForCascadeDeletion(object entity, Type entityType)
        {
            var set = TrackingSetByType(entityType);
            return set.FindMatchingEntityState(entity).MarkedForCascadeDeletion;
        }

        public void HardReset(Dictionary<Type, IList> entities)
        {
            Reset(entities, entityState =>
                entityState.HardReset());
        }

        private void Reset(Dictionary<Type, IList> entities, Action<IEntityStateBase> onEntityState)
        {
            foreach (var entry in entities)
            {
                var setType = entry.Key;
                var set = Sets.FirstOrDefault(_ => _.EntityConfiguration.Type == setType);
                if (set != null)
                {
                    foreach (var entity in entry.Value)
                    {
                        var state = set.FindMatchingEntityState(entity);
                        if (state != null)
                        {
                            onEntityState(state);
                        }
                    }
                }
            }
        }

        public void SoftReset(Dictionary<Type, IList> entities, bool markAsNotNew)
        {
            Reset(entities, entityState =>
                entityState.SoftReset(markAsNotNew));
        }

        public void AbandonChanges(object[] entities = null, IProperty[] properties = null)
        {
            for (var i = 0; i < Sets.Count; i++)
            {
                var set = Sets[i];
                set.AbandonChanges(entities, properties);
            }
        }

        public void Clear()
        {
            RelationshipObserver.Clear();
            for (var i = 0; i < Sets.Count; i++)
            {
                var set = Sets[i];
                set.Clear();
            }
        }

        public static DataTracker[] AllDataTrackers()
        {
            return _allDataTrackers.ToArray();
        }

        public bool HasChanges
        {
            get { return _hasChanges; }
            private set
            {
                var old = _hasChanges;
                _hasChanges = value;
                if (old != value)
                {
                    HasChangesChanged.Emit(() => new ValueChangedEvent<bool>(old, value));
                }
            }
        }

        public EventEmitter<ValueChangedEvent<bool>> HasChangesSinceSnapshotChanged { get; } = new EventEmitter<ValueChangedEvent<bool>>();
        public EventEmitter<ValueChangedEvent<bool>> HasChangesChanged { get; } = new EventEmitter<ValueChangedEvent<bool>>();

        public bool HasChangesSinceSnapshot
        {
            get { return _hasChangesSinceSnapshot; }
            private set
            {
                var old = _hasChangesSinceSnapshot;
                _hasChangesSinceSnapshot = value;
                if (old != value)
                {
                    HasChangesSinceSnapshotChanged.Emit(() => new ValueChangedEvent<bool>(old, value));
                }
            }
        }

        public void RemoveEntityByKeyAndType(CompositeKey key, Type entityType)
        {
            if (key == null)
            {
                return;
            }

            var set = TrackingSetByType(entityType);
            var state = set.GetEntityStateByKey(key);
            if (state != null)
            {
                UnattachEntity(state.Entity);
            }
            else
            {
                set.RemoveEntityByKey(key);
            }
        }

        public IEntityStateBase AttachEntity<T>(T entity)
            where T : class
        {
            var entityType = ResolveTrackingSet(entity, out var set);
            var state = set.AttachEntity(entity, true);
            RelationshipObserver.Observe(entity, entityType);
            return state;
        }

        public void UnattachEntity<T>(T entity)
            where T : class
        {
            var entityType = ResolveTrackingSet(entity, out var set);
            set.RemoveEntity(entity);
            RelationshipObserver.Unobserve(entity, entityType);
        }

        private Type ResolveTrackingSet<T>(T entity, out ITrackingSet set) where T : class
        {
            var entityType = typeof(T);
            if (entityType == typeof(object))
            {
                entityType = entity.GetType();
            }

            set = TrackingSetByType(entityType);
            return entityType;
        }

        public IEntityStateBase GetEntityState<T>(T entity)
            where T : class
        {
            var entityType = ResolveTrackingSet(entity, out var set);
            var state = set.GetEntityState(entity);
            return state;
        }

        public TrackingState GetTrackingState<T>(T entity)
            where T : class
        {
            var entityType = ResolveTrackingSet(entity, out var set);
            var state = set.GetEntityState(entity);
            if (state == null)
            {
                return TrackingState.Untracked;
            }

            return state.IsNew ? TrackingState.TrackedLocal : TrackingState.TrackedRemote;
        }

        public IEntityState<TEntity> ApplyAdd<TEntity>(QueuedAddEntityOperation<TEntity> operation, bool isOffline) where TEntity : class
        {
            var trackingSet = TrackingSet<TEntity>();
            var state = trackingSet.AttachEntity(
                (TEntity)operation.Result.RemoteEntity.Clone(
                    EntityConfigurationBuilder,
                    typeof(TEntity)),
                isOffline);
            if (!isOffline)
            {
                state.HardReset();
            }

            EmitStateChangedEvent();
            return state;
        }

        protected virtual void EmitStateChangedEvent()
        {
            //StateChanged.Emit(() => new OfflineChangeStateChangedEvent(this));
        }

        public void ApplyUpdate<TEntity>(QueuedUpdateEntityOperation<TEntity> operation, bool isOffline)
            where TEntity : class
        {
            var changedProperties = operation.Operation.GetChangedProperties();
            var trackingSet = TrackingSet<TEntity>();
            var ourState = trackingSet.FindMatchingEntityState(operation.Operation.EntityState.Entity);
            foreach (var property in changedProperties)
            {
                property.Property.SetValue(ourState.Entity, property.LocalValue);
                if (!isOffline)
                {
                    property.HardReset();
                }
            }

            EmitStateChangedEvent();
        }

        public void ApplyDelete<TEntity>(QueuedDeleteEntityOperation<TEntity> operation, bool isOffline)
            where TEntity : class
        {
            var trackingSet = TrackingSet<TEntity>();
            if (isOffline)
            {
                var ourState = trackingSet.FindMatchingEntityState(operation.Operation.EntityState.Entity);
                var theirState = operation.Operation.EntityState;
                ourState.MarkedForDeletion = theirState.MarkedForDeletion;
                ourState.MarkedForCascadeDeletion = theirState.MarkedForCascadeDeletion;
            }
            else
            {
                var ourState = trackingSet.FindMatchingEntityState(operation.Operation.EntityState.Entity);
                trackingSet.RemoveEntity((TEntity)ourState.Entity);
            }

            EmitStateChangedEvent();
        }

        public IqlDataChanges GetChanges(IGetChangesOperation getChangesOperation = null)
        {
            getChangesOperation = getChangesOperation ?? new SaveChangesOperation(null);
            return new IqlDataChanges(
                (SaveChangesOperation)getChangesOperation,
                this.GetQueuedChanges(getChangesOperation).OrderBy(_ =>
                {
                    if (_.Operation is IEntityCrudOperationBase op)
                    {
                        return GetPendingDependencyCount(op.EntityState.Entity, op.EntityType);
                    }

                    return 0;
                }).Select(_ => (IQueuedEntityCrudOperation)_).ToArray());
        }

        private class TrackCollectionResult
        {
            public TrackCollectionResult(IList data, List<IEntityStateBase> states)
            {
                Data = data;
                States = states;
            }

            public IList Data { get; }
            public List<IEntityStateBase> States { get; }
        }

        public void RestoreFromJson(string jsonWithChanges)
        {
            SerializedTrackingState state = null;
            if (!string.IsNullOrWhiteSpace(jsonWithChanges))
            {
                try
                {
                    state = (SerializedTrackingState)JsonConvert.DeserializeObject(jsonWithChanges,
                        typeof(SerializedTrackingState));
                }
                catch
                {

                }
            }
            state = state ?? new SerializedTrackingState();
            try
            {
                Restore(state);
            }
            catch (Exception e)
            {
                // TODO: Add logging
            }
        }

        public void Restore(SerializedTrackingState deserialized)
        {
            AbandonChanges();
            // Almost there...
            if (deserialized != null && deserialized.Sets != null)
            {
                for (var i = 0; i < deserialized.Sets.Length; i++)
                {
                    var deserializedSet = deserialized.Sets[i];
                    var set = TrackingSetByTypeName(deserializedSet.Type);
                    for (var j = 0; j < deserializedSet.EntityStates.Length; j++)
                    {
                        var entityState = deserializedSet.EntityStates[j];
                        set.Restore(entityState);
                    }
                }
            }
        }

        public ITrackingSet TrackingSetByTypeName(string typeName)
        {
            var entityConfiguration = EntityConfigurationBuilder.GetEntityByTypeName(typeName);
            var set = TrackingSetByType(entityConfiguration.Type);
            return set;
        }

        public void Dispose()
        {
            for (var i = 0; i < Sets.Count; i++)
            {
                var set = Sets[i];
                set.Dispose();
            }
        }

        /// <summary>
        /// For internal use only
        /// </summary>
        /// <param name="propertyState"></param>
        /// <param name="hasChanged"></param>
        public void NotifyChangedSinceSnapshot(IPropertyState propertyState, bool hasChanged)
        {
            if (!propertyState.EntityState.AttachedToTracker)
            {
                return;
            }
            UpdateLookup(propertyState, hasChanged, _propertiesChangedSinceLastSnapshot, _propertiesChangedSinceLastSave, true);
        }

        public void NotifyPendingInsertChanged<T>(EntityState<T> entityState, bool value) where T : class
        {
            if (!entityState.AttachedToTracker)
            {
                return;
            }
            UpdateLookup(entityState, value, _entitiesPendingInsertSinceLastSnapshot, _entitiesPendingInsertSinceLastSave, true);
        }

        public void NotifyMarkedForDeletionChanged<T>(EntityState<T> entityState, bool value) where T : class
        {
            if (!entityState.AttachedToTracker)
            {
                return;
            }
            UpdateLookup(entityState, value, _entitiesPendingDeleteSinceLastSnapshot, _entitiesPendingDeleteSinceLastSave, false);
        }

        private void UpdateLookup<TLookup>(
            TLookup item, 
            bool value, 
            Dictionary<TLookup, bool> snapshotLookup,
            Dictionary<TLookup, bool> saveLookup,
            bool clearRemovedSnapshotsOnlyIfTrue)
        {
            if (value)
            {
                if (!saveLookup.ContainsKey(item))
                {
                    saveLookup.Add(item, true);
                }
            }
            else if (saveLookup.ContainsKey(item))
            {
                saveLookup.Remove(item);
            }

            UpdateHasChanges();
            if (_ignoreChangedSinceSnapshotChanges)
            {
                return;
            }
            if (!clearRemovedSnapshotsOnlyIfTrue)
            {
                _removedSnapshots.Clear();
            }
            if (value)
            {
                if (clearRemovedSnapshotsOnlyIfTrue)
                {
                    _removedSnapshots.Clear();
                }
                if (!snapshotLookup.ContainsKey(item))
                {
                    snapshotLookup.Add(item, true);
                }
            }
            else if (snapshotLookup.ContainsKey(item))
            {
                snapshotLookup.Remove(item);
            }

            UpdateHasChangesSinceSnapshot();
        }

        private void UpdateHasChanges()
        {
            HasChanges = _entitiesPendingDeleteSinceLastSave.Count > 0 ||
                         _entitiesPendingInsertSinceLastSave.Count > 0 ||
                         _propertiesChangedSinceLastSave.Count > 0;
        }

        private void UpdateHasChangesSinceSnapshot()
        {
            HasChangesSinceSnapshot = _entitiesPendingDeleteSinceLastSnapshot.Count > 0 ||
                                      _entitiesPendingInsertSinceLastSnapshot.Count > 0 ||
                                      _propertiesChangedSinceLastSnapshot.Count > 0;
        }

        public void NotifyAttachedToTrackerChanged<T>(EntityState<T> entityState, bool value) where T : class
        {
            if(!value)
            {
                var lookups = new[]
                {
                    _entitiesPendingInsertSinceLastSave,
                    _entitiesPendingDeleteSinceLastSave,
                };

                for (var i = 0; i < lookups.Length; i++)
                {
                    var l = lookups[i];
                    if (l.ContainsKey(entityState))
                    {
                        l.Remove(entityState);
                    }
                }

                var propertyChangesToRemove = new List<IPropertyState>();
                foreach (var propertyChange in _propertiesChangedSinceLastSave)
                {
                    if (propertyChange.Key.EntityState == entityState)
                    {
                        propertyChangesToRemove.Add(propertyChange.Key);
                    }
                }

                for (var i = 0; i < propertyChangesToRemove.Count; i++)
                {
                    var propertyState = propertyChangesToRemove[i];
                    _propertiesChangedSinceLastSave.Remove(propertyState);
                }
            }
            else
            {
                if (entityState.PendingInsert)
                {
                    _entitiesPendingInsertSinceLastSave.Add(entityState, true);
                }

                if (entityState.MarkedForDeletion)
                {
                    _entitiesPendingDeleteSinceLastSave.Add(entityState, true);
                }

                for (var i = 0; i < entityState.PropertyStates.Length; i++)
                {
                    var property = entityState.PropertyStates[i];
                    if (property.HasChanged)
                    {
                        _propertiesChangedSinceLastSave.Add(property, true);
                    }
                }
            }
            UpdateHasChanges();
        }
    }
}