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
using Iql.Extensions;
using Newtonsoft.Json;

namespace Iql.Data.Tracking
{
    public class DataTracker : IJsonSerializable, IDataChangeProvider
    {
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
                return await persistState.DeleteStateAsync(PersistStateKey());
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
                .Where(_ => _.GetQueuedChanges(null).Length > 0)
                .ToArray();
            if(trackingSets.Length == 0)
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
                EntityConfigurationBuilder.FlattenDependencyGraph(entity,
                    entityType);
            foreach (var item in flattened)
            {
                var tracking = TrackingSetByType(item.Key);
                foreach (var dependency in item.Value)
                {
                    if (dependency == entity)
                    {
                        continue;
                    }

                    if (tracking.FindMatchingEntityState(dependency).IsNew)
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
                .OrderBy(operation => GetPendingDependencyCount(operation.Entity, operation.EntityType))
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

        //public void TrackGraph(object entity, Type entityType)
        //{
        //    var flattenObjectGraph = DataContext.EntityConfigurationContext.FlattenObjectGraph(entity, entityType);
        //    foreach (var obj in flattenObjectGraph)
        //    {
        //        var set = TrackingSetByType(obj.EntityType);
        //        set.TrackEntity(obj.Entity);
        //    }
        //}

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

        public void AbandonChanges()
        {
            for (var i = 0; i < Sets.Count; i++)
            {
                var set = Sets[i];
                set.AbandonChanges();
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

        public bool HasChanges()
        {
            return GetChanges(null).Any();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="TEntity"></typeparam>
        ///// <param name="isOffline"></param>
        ///// <param name="flattenedObjectGraph"></param>
        ///// <param name="responseRoot">The root result. For example, if we get three Clients, and expand their child Clients,
        ///// then we'll have six Clients to track - but the user would like the three returned to them in a list, not all six.</param>
        ///// <param name="mergeExistingOnly"></param>
        ///// <returns></returns>
        //public List<TEntity> TrackResults<TEntity>(
        //    bool isOffline,
        //    Dictionary<Type, IList> flattenedObjectGraph,
        //    List<TEntity> responseRoot = null,
        //    bool mergeExistingOnly = false)
        //{
        //    var responseData = RemoveRootResultsFromFlattenedObjectGraph(flattenedObjectGraph, responseRoot);
        //    List<TEntity> trackedResults = null;
        //    var data = new Dictionary<Type, IList>();
        //    List<IEntityStateBase> states = new List<IEntityStateBase>();
        //    if (responseRoot != null)
        //    {
        //        var trackingResult = TrackCollection(responseRoot, typeof(TEntity), data, mergeExistingOnly);
        //        states.AddRange(trackingResult.States);
        //        trackedResults = (List<TEntity>)trackingResult.Data.ToList(typeof(TEntity));
        //    }

        //    foreach (var dataSet in responseData)
        //    {
        //        var trackingResult = TrackCollection(dataSet.Value, dataSet.Key, data, mergeExistingOnly);
        //        if (trackingResult.States != null)
        //        {
        //            states.AddRange(trackingResult.States);
        //        }
        //    }

        //    if (!mergeExistingOnly)
        //    {
        //        RelationshipObserver.ObserveAll(data);
        //        if (!isOffline)
        //        {
        //            for (var i = 0; i < states.Count; i++)
        //            {
        //                var state = states[i];
        //                state.Reset();
        //            }
        //        }
        //    }

        //    return trackedResults;
        //}

        private static Dictionary<Type, IList> RemoveRootResultsFromFlattenedObjectGraph<TEntity>(Dictionary<Type, IList> flattenedObjectGraph,
            List<TEntity> responseRoot)
        {
            var responseData = new Dictionary<Type, IList>();
            foreach (var keyValue in flattenedObjectGraph)
            {
                responseData.Add(keyValue.Key, keyValue.Value);
            }

            if (!responseData.ContainsKey(typeof(TEntity)))
            {
                responseData.Add(typeof(TEntity), new List<TEntity>());
            }

            var rootDictionary = new Dictionary<object, object>();
            foreach (var item in responseData[typeof(TEntity)])
            {
                rootDictionary.Add(item, item);
            }

            if (responseRoot != null)
            {
                foreach (var item in responseRoot)
                {
                    if (rootDictionary.ContainsKey(item))
                    {
                        rootDictionary.Remove(item);
                    }
                }
            }

            var newList = new List<TEntity>();
            responseData[typeof(TEntity)] = newList;
            foreach (var item in rootDictionary)
            {
                newList.Add((TEntity)item.Key);
            }

            return responseData;
        }

        //public void UpdateEntityStateWithRemoteEntity<TEntity>(
        //    TEntity localEntity,
        //    TEntity remoteEntity)
        //where TEntity : class
        //{
        //    var trackingSet = DataContext.DataTracker.TrackingSet<TEntity>();
        //    if (remoteEntity != null)
        //    {
        //        trackingSet.AttachEntity(localEntity, remoteEntity, false);
        //    }
        //    var flattenedObjectGraph = DataContext.EntityConfigurationContext.FlattenObjectGraph(
        //        localEntity,
        //        typeof(TEntity));
        //    var result = TrackResults<TEntity>(
        //        false,
        //        flattenedObjectGraph,
        //        null,
        //        true);
        //    trackingSet.FindMatchingEntityState(localEntity).Reset();
        //    //return (IEntityState<TEntity>) TrackingSet<TEntity>().GetEntityState(result[0]);
        //}

        //        private TrackCollectionResult TrackCollection(
        //            IList set, Type type, Dictionary<Type, IList> data, bool mergeExistingOnly, bool reset = true)
        //        {
        //            var states = new List<IEntityStateBase>();
        //            if (set.Count > 0)
        //            {
        //#if TypeScript
        //                set = EntityConfigurationBuilder.EnsureTypedListByType(set, type, null, null, false, true);
        //#endif
        //                var trackingSet = TrackingSetByType(type);
        //                states = trackingSet.TrackEntities(set, false, !mergeExistingOnly, mergeExistingOnly);
        //                if (reset)
        //                {
        //                    trackingSet.ResetAll(states);
        //                }

        //                set = states.Select(s => s.Entity).EnumerableToList(type);
        //                if (data.ContainsKey(type))
        //                {
        //                    foreach (var item in set)
        //                    {
        //                        if (!data[type].Contains(item))
        //                        {
        //                            data[type].Add(item);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    data.Add(type, set);
        //                }
        //            }

        //            return new TrackCollectionResult(set, states);
        //        }

        public void RemoveEntityByKey<T>(CompositeKey key)
            where T : class
        {
            if (key == null)
            {
                return;
            }

            var set = TrackingSet<T>();
            var state = set.GetEntityStateByKey(key);
            if (state != null)
            {
                UnattachEntity((T)state.Entity);
            }
            else
            {
                set.RemoveEntityByKey(key);
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
                    typeof(TEntity),
                    RelationshipCloneMode.DoNotClone),
                isOffline);
            if (!isOffline)
            {
                state.HardReset();
            }

            return state;
        }

        public void ApplyUpdate<TEntity>(QueuedUpdateEntityOperation<TEntity> operation, bool isOffline)
            where TEntity : class
        {
            var changedProperties = operation.Operation.GetChangedProperties();
            var trackingSet = TrackingSet<TEntity>();
            var ourState = trackingSet.FindMatchingEntityState(operation.Operation.Entity);
            foreach (var property in changedProperties)
            {
                property.Property.SetValue(ourState.Entity, property.LocalValue);
                if (!isOffline)
                {
                    property.HardReset();
                }
            }
        }

        public void ApplyDelete<TEntity>(QueuedDeleteEntityOperation<TEntity> operation, bool isOffline)
            where TEntity : class
        {
            var trackingSet = TrackingSet<TEntity>();
            if (isOffline)
            {
                var ourState = trackingSet.FindMatchingEntityState(operation.Operation.Entity);
                if (ourState != null)
                {
                    int a = 0;
                }
                var theirState = operation.Operation.EntityState;
                ourState.MarkedForDeletion = theirState.MarkedForDeletion;
                ourState.MarkedForCascadeDeletion = theirState.MarkedForCascadeDeletion;
            }
            else
            {
                var ourState = trackingSet.FindMatchingEntityState(operation.Operation.Entity);
                trackingSet.RemoveEntity((TEntity)ourState.Entity);
            }
        }

        public IQueuedOperation[] GetChanges(IDataContext dataContext = null, object[] entities = null, IProperty[] properties = null)
        {
            return this.GetQueuedChanges(dataContext, entities, properties);
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
            var deserialized = (SerializedTrackingState)JsonConvert.DeserializeObject(jsonWithChanges, typeof(SerializedTrackingState));
            Restore(deserialized);
        }

        public void Restore(SerializedTrackingState deserialized)
        {
            AbandonChanges();
            // Almost there...
            if(deserialized != null && deserialized.Sets != null)
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
    }
}