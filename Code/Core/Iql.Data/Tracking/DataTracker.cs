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
    public class DataTracker : IJsonSerializable, IDataChangeProvider, IDisposable, ISnapshotManager, ILockable
    {
        private readonly Dictionary<object, Dictionary<string, Action<IEntityStateBase, IPropertyState>>> _interestedParties =
            new Dictionary<object, Dictionary<string, Action<IEntityStateBase, IPropertyState>>>();

        private readonly List<TrackerSnapshot> _removedSnapshots = new List<TrackerSnapshot>();

        private readonly List<TrackerSnapshot> _snapshots = new List<TrackerSnapshot>();
        private bool _hasChanges;
        private bool _hasChangesSinceSnapshot;
        private bool _hasSnapshot;

        private bool _ignoreChanges = false;
        private bool _isRestoring = false;
        private bool _hasRestorableSnapshot;

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

        public DataTrackerState StateSinceSnapshot { get; } = new DataTrackerState(true);

        public DataTrackerState StateSinceSave { get; } = new DataTrackerState(false);

        //public EventEmitter<OfflineChangeStateChangedEvent> StateChanged { get; } = new EventEmitter<OfflineChangeStateChangedEvent>();
        public string SynchronicityKey => Name;
        public DataTrackerKind Kind { get; }

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

        public EventEmitter<ValueChangedEvent<bool>> HasSnapshotChanged { get; } =
            new EventEmitter<ValueChangedEvent<bool>>();

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

        public List<IUpdateEntityOperation> GetUpdates(IDataContext dataContext = null, object[] entities = null,
            IProperty[] properties = null)
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

        public void Dispose()
        {
            for (var i = 0; i < Sets.Count; i++)
            {
                var set = Sets[i];
                set.Dispose();
            }
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

        public ILockable Parent => null;
        public bool IsLocked { get; private set; }

        public void Lock()
        {
            IsLocked = true;
        }

        public void Unlock()
        {
            IsLocked = false;
        }

        public TrackerSnapshot CurrentSnapshot => _snapshots.LastOrDefault();

        public void ClearSnapshots()
        {
            while (_snapshots.Any())
            {
                RemoveLastSnapshot();
            }
        }
        public EventEmitter<DataTracker> SnapshotAdding { get; } = new EventEmitter<DataTracker>();
        public EventEmitter<DataTracker> SnapshotAdded { get; } = new EventEmitter<DataTracker>();
        public TrackerSnapshot AddSnapshot(bool? nullIfEmpty = null)
        {
            var doNullIfEmpty = nullIfEmpty ?? false;
            if (doNullIfEmpty && !HasChangesSinceSnapshot)
            {
                return null;
            }

            SnapshotAdding.Emit(() => this);
            var snapshot = NewTrackerSnapshot();
            snapshot.Id = Guid.NewGuid();
            _isRestoring = true;
            var items = StateSinceSnapshot.GetPropertiesChanged(true);
            foreach (var entity in snapshot.Entities)
            {
                entity.Key.AddSnapshot();
            }
            foreach (var propertyState in items)
            {
                if (!propertyState.IsRelationshipCollection)
                {
                    snapshot.Values.Add(propertyState, new PropertySnapshot
                    {
                        State = propertyState,
                        PreviousValue = propertyState.SnapshotValue,
                        CurrentValue = propertyState.LocalValue
                    });
                }

                propertyState.AddSnapshot();
            }

            _isRestoring = false;
            ResetSnapshotState(true, true);
            _snapshots.Add(snapshot);
            UpdateHasSnapshot();
            SnapshotAdded.Emit(() => this);
            return snapshot;
        }

        public bool UndoChanges(object[] entities = null, object[] properties = null)
        {
            return StateSinceSnapshot.UndoChanges(entities, properties);
            //return GoToLastSnapshot(false, true, false, entities, properties);
        }

        public bool RemoveLastSnapshot(SnapshotRemoveKind? kind = null)
        {
            var result = RemoveLastSnapshotInternal(kind);
            return result;
        }

        private bool RemoveLastSnapshotInternal(SnapshotRemoveKind? kind)
        {
            kind = kind ?? SnapshotRemoveKind.None;
            switch (kind)
            {
                case SnapshotRemoveKind.GoToPreSnapshotValues:
                    StateSinceSnapshot.UndoChanges();
                    //return GoToLastSnapshot(true, true, true);
                    break;
                case SnapshotRemoveKind.GoToSnapshotValues:
                    StateSinceSnapshot.UndoChanges();
                    break;
            }

            var snapshotRemoved = TryRemoveLastSnapshotInternal();
            if (kind == SnapshotRemoveKind.GoToPreSnapshotValues)
            {
                SetSnapshotValues(snapshotRemoved, true);
                StateSinceSnapshot.RemoveMatching(snapshotRemoved);
            }
            else
            {
                StateSinceSnapshot.MergeWith(snapshotRemoved);
            }
            // All changes in save state but not in snapshot
            _removedSnapshots.Add(snapshotRemoved);
            UpdateHasSnapshot();
            //if (kind == SnapshotRemoveKind.GoToPreSnapshotValues)
            //{
            //    UndoChanges();
            //}
            return true;
        }

        private static void SetSnapshotValues(TrackerSnapshot snapshot, bool usePrevious)
        {
            foreach (var item in snapshot.Entities)
            {
                var value = usePrevious ? item.Value.PreviousValue : item.Value.CurrentValue;
                if (value.WillUntrack())
                {
                    item.Key.SetSnapshotValue(value);
                    item.Key.Status = value;
                }
            }
            foreach (var item in snapshot.Values)
            {
                var value = usePrevious ? item.Value.PreviousValue : item.Value.CurrentValue;
                item.Key.SetSnapshotValue(value);
                item.Key.Property.SetValue(item.Key.EntityState.Entity, value);
            }
            foreach (var item in snapshot.Entities)
            {
                var value = usePrevious ? item.Value.PreviousValue : item.Value.CurrentValue;
                item.Key.SetSnapshotValue(value);
                item.Key.Status = value;
            }
        }

        private TrackerSnapshot TryRemoveLastSnapshotInternal()
        {
            if (HasSnapshot)
            {
                var last = _snapshots.Last();
                _snapshots.Remove(last);
                UpdateHasSnapshot();
                return last;
            }

            return null;
        }

        public bool RestoreNextAbandonedSnapshot()
        {
            if (!HasRestorableSnapshot)
            {
                return false;
            }

            var last = _removedSnapshots.Last();
            _removedSnapshots.Remove(last);
            SetSnapshotValues(last, false);
            _snapshots.Add(last);
            UpdateHasSnapshot();
            return true;
        }

        public TrackerSnapshot ReplaceLastSnapshot()
        {
            RemoveLastSnapshot();
            return AddSnapshot();
        }

        public bool HasSnapshot
        {
            get => _hasSnapshot;
            set
            {
                var old = _hasSnapshot;
                _hasSnapshot = value;
                if (old != value)
                {
                    HasSnapshotChanged.Emit(() => new ValueChangedEvent<bool>(old, value));
                }
            }
        }

        public EventEmitter<ValueChangedEvent<bool>> HasRestorableSnapshotChanged { get; } = new EventEmitter<ValueChangedEvent<bool>>();
        public bool HasRestorableSnapshot
        {
            get => _hasRestorableSnapshot;
            set
            {
                var old = _hasRestorableSnapshot;
                _hasRestorableSnapshot = value;
                if (old != value)
                {
                    HasRestorableSnapshotChanged.Emit(() => new ValueChangedEvent<bool>(old, value));
                }
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

        public bool HasChanges => StateSinceSave.HasChanges;

        public EventEmitter<ValueChangedEvent<bool>> HasChangesSinceSnapshotChanged =>
            StateSinceSnapshot.HasChangesChanged;

        public EventEmitter<ValueChangedEvent<bool>> HasChangesChanged => StateSinceSave.HasChangesChanged;

        public TrackerSnapshot[] Snapshots => (_snapshots ?? new List<TrackerSnapshot>()).ToArray();
        public int SnapshotsCount => Snapshots.Length;
        public TrackerSnapshot[] RestorableSnapshots => (_removedSnapshots ?? new List<TrackerSnapshot>()).ToArray();
        public int RestorableSnapshotsCount => _removedSnapshots == null ? 0 : _removedSnapshots.Count;

        public bool HasChangesSinceSnapshot => StateSinceSnapshot.HasChanges;

        //private bool HasChangedSinceSnapshot(TrackerSnapshot snapshot = null)
        //{
        //    if (StateSinceSnapshot.PropertiesChangedCount > 0)
        //    {
        //        return true;
        //    }

        //    snapshot = snapshot ?? NewTrackerSnapshot();
        //    var newEntities = snapshot.EntitiesPendingInsert;
        //    var deletedEntities = snapshot.EntitiesPendingDelete;
        //    if (CurrentSnapshot != null)
        //    {
        //        if (newEntities.Length != CurrentSnapshot.EntitiesPendingInsert.Length)
        //        {
        //            return true;
        //        }

        //        if (deletedEntities.Length != CurrentSnapshot.EntitiesPendingDelete.Length)
        //        {
        //            return true;
        //        }

        //        foreach (var entity in newEntities)
        //        {
        //            if (!CurrentSnapshot.EntitiesPendingInsert.Contains(entity))
        //            {
        //                return true;
        //            }
        //        }

        //        foreach (var entity in deletedEntities)
        //        {
        //            if (!CurrentSnapshot.EntitiesPendingDelete.Contains(entity))
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return newEntities.Length > 0 || deletedEntities.Length > 0;
        //    }

        //    return false;
        //}

        private void UpdateHasSnapshot()
        {
            HasSnapshot = CurrentSnapshot != null;
            HasRestorableSnapshot = RestorableSnapshotsCount > 0;
        }

        private void ResetSnapshotState(bool clearRemovedSnapshots, bool forceClearCache)
        {
            if (clearRemovedSnapshots)
            {
                ClearRestorableSnapshots();
            }

            if (forceClearCache)
            {
                StateSinceSnapshot.Clear();
            }
        }

        private TrackerSnapshot NewTrackerSnapshot()
        {
            var snapshot = new TrackerSnapshot();
            var sinceSave = StateSinceSave.GetEntitiesChanged();
            var sinceSnapshot = StateSinceSnapshot.GetEntitiesChanged();
            foreach (var entity in sinceSave)
            {
                snapshot.Entities.Add(entity.Key, entity.Value);
            }
            foreach (var entity in sinceSnapshot)
            {
                if (snapshot.Entities.ContainsKey(entity.Key))
                {
                    snapshot.Entities[entity.Key] = entity.Value;
                }
                else
                {
                    snapshot.Entities.Add(entity.Key, entity.Value);
                }
            }
            return snapshot;
        }

        private bool GoToLastSnapshot(
            bool remove,
            bool? undoChanges,
            bool? usePreSnapshotValue,
            object[] entities = null,
            IProperty[] properties = null)
        {
            var snapshot = NewTrackerSnapshot();
            _isRestoring = true;
            var unsetSnapshotValue = false;
            if (_snapshots.Any())
            {
                var last = _snapshots.Last();
                if (remove)
                {
                    _snapshots.Remove(last);
                    _removedSnapshots.Add(last);
                    UpdateHasSnapshot();
                    if (!_snapshots.Any())
                    {
                        unsetSnapshotValue = true;
                    }
                }

                foreach (var item in last.Entities)
                {
                    if (entities != null && !entities.Contains(item.Key.Entity))
                    {
                        continue;
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

                    if (undoChanges == true && (usePreSnapshotValue == true || item.Key.HasChanged))
                    {
                        item.Key.Property.SetValue(item.Key.EntityState.Entity,
                            usePreSnapshotValue == true ? item.Value.PreviousValue : item.Value.CurrentValue);
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
                    var propertiesChanged = StateSinceSnapshot.GetPropertiesChanged();
                    foreach (var item in propertiesChanged)
                    {
                        if (entities != null && !entities.Contains(item.EntityState.Entity))
                        {
                            continue;
                        }

                        if (properties != null && !properties.Contains(item.Property))
                        {
                            continue;
                        }

                        if (!last.Values.ContainsKey(item))
                        {
                            item.AbandonChanges();
                        }
                    }
                    if (usePreSnapshotValue == true)
                    {
                        var previous = _snapshots.Where(_ => _ != last).LastOrDefault();
                        if (previous == null)
                        {
                            previous = new TrackerSnapshot();
                        }

                        RestoreNewAndDeleted(snapshot, previous, entities);
                    }
                    else
                    {
                        RestoreNewAndDeleted(snapshot, last, entities);
                    }
                }

                ResetSnapshotState(false, false);
                _isRestoring = false;
                return true;
            }

            AbandonChanges(entities, properties);
            ResetSnapshotState(false, false);
            _isRestoring = false;
            return false;
        }

        private void RestoreNewAndDeleted(TrackerSnapshot fromSnapshot, TrackerSnapshot toSnapshot, object[] entities)
        {
            bool IsAllowed(IEntityStateBase entityState)
            {
                if (entities == null)
                {
                    return true;
                }

                return entities.Contains(entityState.Entity);
            }
            var dictionary = new Dictionary<IEntityStateBase, EntitySnapshot>();
            foreach (var item in fromSnapshot.Entities)
            {
                if (!IsAllowed(item.Key))
                {
                    continue;
                }
                var snapshot = new EntitySnapshot();
                snapshot.State = item.Key;
                snapshot.PreviousValue = item.Value.CurrentValue;
                var match = toSnapshot.Entities.ContainsKey(item.Key)
                    ? toSnapshot.Entities[snapshot.State].CurrentValue
                    : item.Value.CurrentValue.Opposite();
                snapshot.CurrentValue = match;
                if (snapshot.PreviousValue != snapshot.CurrentValue)
                {
                    dictionary.Add(snapshot.State, snapshot);
                }
            }
            foreach (var item in toSnapshot.Entities)
            {
                if (!IsAllowed(item.Key))
                {
                    continue;
                }
                if (!dictionary.ContainsKey(item.Key) && !fromSnapshot.Entities.ContainsKey(item.Key))
                {
                    var snapshot = new EntitySnapshot();
                    snapshot.State = item.Key;
                    snapshot.CurrentValue = item.Value.CurrentValue;
                    snapshot.PreviousValue = item.Value.PreviousValue;
                    if (snapshot.PreviousValue != snapshot.CurrentValue)
                    {
                        dictionary.Add(snapshot.State, snapshot);
                    }
                }
            }

            var all = dictionary.Values.ToArray();
            var itemsToDelete = all.Where(_ => (_.PreviousValue == EntityStatus.Existing && _.CurrentValue == EntityStatus.ExistingAndPendingDelete) ||
                                               (_.PreviousValue == EntityStatus.New && _.CurrentValue == EntityStatus.NewAndDeleted))
                .ToArray();
            var itemsToAdd = all.Where(_ => (_.PreviousValue == EntityStatus.ExistingAndPendingDelete && _.CurrentValue == EntityStatus.Existing) ||
                                               (_.PreviousValue == EntityStatus.NewAndDeleted && _.CurrentValue == EntityStatus.New))
                .ToArray();
            for (var i = 0; i < itemsToAdd.Length; i++)
            {
                var entity = itemsToAdd[i];
                Sets.Single(_ => _.EntityType == entity.State.EntityType).AddEntity(entity.State.Entity);
                entity.State.UnmarkForDeletion();
            }

            for (var i = 0; i < itemsToDelete.Length; i++)
            {
                var entity = itemsToDelete[i];
                entity.State.MarkedForDeletion = true;
            }
        }

        private void RelationshipChanged(RelationshipChangedEvent relationshipChangedEvent)
        {
            var trackingSet = TrackingSetByType(relationshipChangedEvent.Relationship.Source.EntityConfiguration.Type);
            (trackingSet as TrackingSetBase).RelationshipChanged(relationshipChangedEvent);
        }

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

        public IEntityState<TEntity> ApplyAdd<TEntity>(QueuedAddEntityOperation<TEntity> operation, bool isOffline)
            where TEntity : class
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
                trackingSet.UntrackEntity((TEntity)ourState.Entity);
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

        /// <summary>
        ///     For internal use only.
        /// </summary>
        /// <param name="propertyState"></param>
        public void NotifyLocalValueChanged(IPropertyState propertyState)
        {
            NotifyInterestedParties(propertyState.EntityState, propertyState);
        }

        /// <summary>
        ///     For internal use only.
        /// </summary>
        /// <param name="propertyState"></param>
        public void NotifyRemoteValueChanged(IPropertyState propertyState)
        {
            var found = false;
            if (_removedSnapshots != null)
            {
                for (var i = 0; i < _removedSnapshots.Count; i++)
                {
                    var removedSnapshot = _removedSnapshots[i];
                    foreach (var item in removedSnapshot.Values)
                    {
                        if (item.Key == propertyState)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        break;
                    }
                }
            }

            if (found)
            {
                ClearRestorableSnapshots();
            }
        }

        /// <summary>
        ///     For internal use only.
        /// </summary>
        /// <param name="propertyState"></param>
        /// <param name="hasChanged"></param>
        public void NotifyChangedSinceSnapshotChanged(IPropertyState propertyState,
            bool isDifferentFromSave,
            bool isDifferentFromSnapshot)
        {
            if (!propertyState.EntityState.AttachedToTracker)
            {
                return;
            }

            //UpdateLookup(
            //    propertyState,
            //    isDifferentFromSave,
            //    isDifferentFromSnapshot,
            //    propertyState.RemoteValue,
            //    propertyState.SnapshotValue,
            //    propertyState.LocalValue,
            //    DataTrackerStateKind.Property,
            //    true);
            var clearRemovedSnapshotsOnlyIfTrue = true;
            StateSinceSave.Update(DataTrackerStateKind.Property, propertyState, isDifferentFromSave, propertyState.RemoteValue, propertyState.LocalValue);
            //if (_ignoreChangedSinceSnapshotChanges)
            //{
            //    return;
            //}

            if (!_isRestoring && !clearRemovedSnapshotsOnlyIfTrue)
            {
                ClearRestorableSnapshots();
            }

            if (isDifferentFromSnapshot)
            {
                if (!_isRestoring && clearRemovedSnapshotsOnlyIfTrue)
                {
                    ClearRestorableSnapshots();
                }

                StateSinceSnapshot.Update(DataTrackerStateKind.Property, propertyState, true, propertyState.SnapshotValue, propertyState.LocalValue);
            }
            else if (StateSinceSnapshot.Has(DataTrackerStateKind.Property, propertyState))
            {
                StateSinceSnapshot.Update(DataTrackerStateKind.Property, propertyState, false, propertyState.SnapshotValue, propertyState.LocalValue);
            }
            NotifyInterestedParties(propertyState.EntityState);
        }

        /// <summary>
        ///     For internal use only.
        /// </summary>
        /// <param name="propertyState"></param>
        /// <param name="hasChanged"></param>
        public void NotifyChangedChanged(IPropertyState propertyState, bool hasChanged)
        {
            if (!propertyState.EntityState.AttachedToTracker)
            {
                return;
            }

            if (!hasChanged)
            {
                if (!propertyState.IsRelationshipCollection)
                {
                    propertyState.SetSnapshotValue(propertyState.PropertyChanger.CloneValue(propertyState.RemoteValue));
                }
                //propertyState.SetSnapshotValue(propertyState.RemoteValue);//.PropertyChanger.CloneValue(propertyState.LocalValue));
                StateSinceSave.RemovePropertyChange(propertyState);
                StateSinceSnapshot.RemovePropertyChange(propertyState);
                foreach (var snapshot in Snapshots)
                {
                    if (snapshot.Values.ContainsKey(propertyState))
                    {
                        var propertySnapshot = snapshot.Values[propertyState];
                        propertySnapshot.PreviousValue = propertyState.LocalValue;
                        if (propertyState.PropertyChanger.AreEquivalent(propertySnapshot.PreviousValue, propertySnapshot.CurrentValue))
                        {
                            snapshot.Values.Remove(propertyState);
                        }
                    }
                }
            }
            //NotifyInterestedParties(propertyState.EntityState);
        }

        private void NotifyInterestedParties(IEntityStateBase entityState, IPropertyState propertyState = null)
        {
            if (entityState != null)
            {
                var entity = entityState.Entity;
                if (_interestedParties.ContainsKey(entity))
                {
                    var interestedParties = _interestedParties[entity].Values.ToList();
                    for (var i = 0; i < interestedParties.Count; i++)
                    {
                        var item = interestedParties[i];
                        if (item != null)
                        {
                            item(entityState, propertyState);
                        }
                    }
                }
            }
        }

        public void NotifyStatusChanged<T>(EntityState<T> entityState, bool isDifferentFromSave) where T : class
        {
            //if (!entityState.AttachedToTracker)
            //{
            //    return;
            //}
            var isDifferentFromSnapshot =
                (entityState.Status == EntityStatus.New ||
                 entityState.Status == EntityStatus.ExistingAndPendingDelete ||
                 entityState.Status == EntityStatus.NewAndDeleted) &&
                StateSinceSnapshot.GetEntityStatus(entityState) != entityState.Status;
            UpdateLookup(
                entityState,
                isDifferentFromSave,
                isDifferentFromSnapshot,
                entityState.Status,
                entityState.Status,
                entityState.Status,
                DataTrackerStateKind.EntityStatus,
                true);
            //StateSinceSave.UpdateStatusChanged(
            //    entityState,
            //    isDifferentFromSave,
            //    entityState.Status,
            //    entityState.Status);
            //StateSinceSnapshot.UpdateStatusChanged(
            //    entityState,
            //    isDifferentFromSnapshot,
            //    entityState.Status,
            //    entityState.Status);
        }

        private void UpdateLookup<TLookup>(
            TLookup item,
            bool isDifferentFromSave,
            bool isDifferentFromSnapshot,
            object remoteValue,
            object snapshotValue,
            object localValue,
            DataTrackerStateKind changeKind,
            bool clearRemovedSnapshotsOnlyIfTrue)
        {
            StateSinceSave.Update(changeKind, item, isDifferentFromSave, remoteValue, localValue);
            //if (_ignoreChangedSinceSnapshotChanges)
            //{
            //    return;
            //}

            if (!_isRestoring && !clearRemovedSnapshotsOnlyIfTrue)
            {
                ClearRestorableSnapshots();
            }

            if (isDifferentFromSnapshot)
            {
                if (!_isRestoring && clearRemovedSnapshotsOnlyIfTrue)
                {
                    ClearRestorableSnapshots();
                }

                StateSinceSnapshot.Update(changeKind, item, true, snapshotValue, localValue);
            }
            else if (StateSinceSnapshot.Has(changeKind, item))
            {
                StateSinceSnapshot.Update(changeKind, item, false, snapshotValue, localValue);
            }
        }

        private void ClearRestorableSnapshots()
        {
            _removedSnapshots.Clear();
            UpdateHasSnapshot();
        }

        public void NotifyAttachedToTrackerChanged<T>(EntityState<T> entityState, bool value) where T : class
        {
            if (!value)
            {
                StateSinceSave.Update(DataTrackerStateKind.EntityStatus, entityState, false, EntityStatus.Unattached, EntityStatus.Unattached);

                var propertyChangesToRemove = new List<IPropertyState>();
                var propertiesChangedSinceLastSave = StateSinceSave.GetPropertiesChanged().ToArray();
                foreach (var propertyChange in propertiesChangedSinceLastSave)
                {
                    if (propertyChange.EntityState == entityState)
                    {
                        propertyChangesToRemove.Add(propertyChange);
                    }
                }

                for (var i = 0; i < propertyChangesToRemove.Count; i++)
                {
                    var propertyState = propertyChangesToRemove[i];
                    StateSinceSave.Update(DataTrackerStateKind.Property, propertyState, false, null, null);
                }
            }
            else
            {
                if (entityState.PendingInsert || entityState.MarkedForDeletion)
                {
                    StateSinceSave.Update(DataTrackerStateKind.EntityStatus, entityState, true, entityState.Status.Opposite(), entityState.Status);
                }

                for (var i = 0; i < entityState.PropertyStates.Length; i++)
                {
                    var property = entityState.PropertyStates[i];
                    if (property.HasChanged)
                    {
                        StateSinceSave.Update(DataTrackerStateKind.Property, property, true, property.RemoteValue, property.LocalValue);
                    }
                }
            }
        }

        public IEntityStateBase FindTrackedEntity(CompositeKey compositeKey)
        {
            var set = Sets.FirstOrDefault(_ => _.EntityConfiguration.TypeName == compositeKey.TypeName);
            if (set == null)
            {
                return null;
            }

            return set.FindMatchingEntityState(compositeKey);
        }

        public void UnregisterInterest(object value, string key)
        {
            if (_interestedParties.ContainsKey(value))
            {
                var interestedParties = _interestedParties[value];
                if (interestedParties.ContainsKey(key))
                {
                    interestedParties.Remove(key);
                }
            }
        }

        public void RegisterInterest(object value, string key, Action<IEntityStateBase, IPropertyState> action)
        {
            if (!_interestedParties.ContainsKey(value))
            {
                _interestedParties.Add(value, new Dictionary<string, Action<IEntityStateBase, IPropertyState>>());
            }

            var interestedParties = _interestedParties[value];
            if (!interestedParties.ContainsKey(key))
            {
                interestedParties.Add(key, action);
            }
            else
            {
                interestedParties[key] = action;
            }
        }

        public void NotifyEntityIsNewChanged(IEntityStateBase entityState)
        {
            NotifyInterestedParties(entityState);
        }

        public void NotifyEntityHasChangedChanged(IEntityStateBase entityState)
        {
            NotifyInterestedParties(entityState);
        }

        public void NotifyEntityHasChangedSinceSnapshotChanged(IEntityStateBase entityState)
        {
            NotifyInterestedParties(entityState);
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
    }
}