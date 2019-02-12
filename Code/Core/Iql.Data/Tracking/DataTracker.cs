using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Conversion;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.Extensions;
using Iql.Data.Lists;
using Iql.Data.Relationships;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Extensions;
using Newtonsoft.Json;

namespace Iql.Data.Tracking
{
    public class DataTracker : IJsonSerializable
    {
        private RelationshipObserver _relationshipObserver;

        public DataTracker(EntityConfigurationBuilder entityConfigurationBuilder, string name,
            bool offline = false, bool silent = false)
        {
            EntityConfigurationBuilder = entityConfigurationBuilder;
            Name = name;
            Offline = offline;
            SetsMap = new Dictionary<string, ITrackingSet>();
            Sets = new List<ITrackingSet>();
            //Id = Guid.NewGuid().ToString();
            if (!silent)
            {
                _allDataTrackers.Add(this);
            }
        }

        public Dictionary<string, ITrackingSet> SetsMap { get; set; }
        public List<ITrackingSet> Sets { get; set; }

        public bool AllowLocalKeyGeneration => Offline;
        public EntityConfigurationBuilder EntityConfigurationBuilder { get; }
        public string Name { get; }
        public bool Offline { get; }
        public IDataContext DataContext { get; set; }

        public IRelationshipObserver RelationshipObserver
        {
            get
            {
                if (_relationshipObserver == null)
                {
                    _relationshipObserver = new RelationshipObserver(this);
                }

                return _relationshipObserver;
            }
        }

        private static List<DataTracker> _allDataTrackers { get; }
            = new List<DataTracker>();

        public string SerializeToJson()
        {
            return JsonConvert.SerializeObject(PrepareForJson());
        }

        public object PrepareForJson()
        {
            return new
            {
                Sets = Sets.Select(_ => _.PrepareForJson())
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

        public List<IEntityCrudOperationBase> GetInserts(object[] entities = null)
        {
            var inserts = new List<IEntityCrudOperationBase>();
            foreach (var set in Sets)
            {
                inserts.AddRange(set.GetInserts(entities));
            }

            inserts = inserts
                .OrderBy(operation => GetPendingDependencyCount(operation.Entity, operation.EntityType))
                .ToList();
            return inserts;
        }

        public List<IUpdateEntityOperation> GetUpdates(object[] entities = null, IProperty[] properties = null)
        {
            var updates = new List<IUpdateEntityOperation>();
            foreach (var set in Sets)
            {
                updates.AddRange(set.GetUpdates(entities, properties));
            }

            return updates;
        }

        public List<IEntityCrudOperationBase> GetDeletions(object[] entities = null)
        {
            var deletions = new List<IEntityCrudOperationBase>();
            foreach (var set in Sets)
            {
                deletions.AddRange(set.GetDeletions(entities));
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

        private IEnumerable<IQueuedOperation> GetQueuedOperations(object[] entities = null,
            IProperty[] properties = null)
        {
            var changes = new List<IQueuedOperation>();
            GetDeletions(entities).ForEach(deletion =>
            {
                var queuedOperation =
                    Activator.CreateInstance(
                        typeof(QueuedDeleteEntityOperation<>).MakeGenericType(deletion.EntityType), deletion, null);
                changes.Add((IQueuedOperation)queuedOperation);
            });
            GetUpdates(entities, properties).ForEach(update =>
            {
                var queuedOperation =
                    Activator.CreateInstance(
                        typeof(QueuedUpdateEntityOperation<>).MakeGenericType(update.EntityType), update, null);
                changes.Add((IQueuedOperation)queuedOperation);
            });
            GetInserts(entities).ForEach(insert =>
            {
                var queuedOperation =
                    Activator.CreateInstance(
                        typeof(QueuedAddEntityOperation<>).MakeGenericType(insert.EntityType), insert, null);
                changes.Add((IQueuedOperation)queuedOperation);
            });
            return changes;
        }

        public virtual IQueuedOperation Filter<TEntity>(
            IQueuedOperation operation) where TEntity : class
        {
            switch (operation.Operation.Type)
            {
                case OperationType.Add:
                    break;
                case OperationType.Delete:
                    break;
                case OperationType.Update:
                    break;
            }

            return operation;
        }

        public void Reset(Dictionary<Type, IList> entities)
        {
            foreach (var entry in entities)
            {
                var setType = entry.Key;
                var set = Sets.FirstOrDefault(_ => _.EntityConfiguration.Type == setType);
                if (set != null)
                {
                    foreach (var entity in entry.Value)
                    {
                        set.FindMatchingEntityState(entity)?.Reset();
                    }
                }
            }
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
            return GetChanges().Any();
        }

        internal static void ForAllDataTrackers(
            Action<DataTracker> action, IDataContext dataContext = null)
        {
            var dataTrackersDealtWith = new Dictionary<DataTracker, DataTracker>();
            var allDataTrackers = AllDataTrackers();
            foreach (var dataTracker in allDataTrackers)
            {
                if (!dataTrackersDealtWith.ContainsKey(dataTracker))
                {
                    dataTrackersDealtWith.Add(dataTracker, dataTracker);
                }

                if (dataContext == null)
                {
                    action(dataTracker);
                }
                else if (dataTracker.DataContext != null &&
                    dataTracker.DataContext.EntityConfigurationContext == dataContext.EntityConfigurationContext &&
                    dataTracker.DataContext.SynchronicityKey == dataContext.SynchronicityKey)
                {
                    action(dataTracker);
                }
            }
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
                RemoveEntity((T)state.Entity);
            }
            else
            {
                set.RemoveEntityByKey(key);
            }
        }

        public void RemoveEntity<T>(T entity)
            where T : class
        {
            var set = TrackingSet<T>();
            set.RemoveEntity(entity);
            RelationshipObserver.Unobserve(entity, typeof(T));
        }

        public void ApplyAdd<TEntity>(QueuedAddEntityOperation<TEntity> operation, bool isOffline) where TEntity : class
        {
            var trackingSet = TrackingSet<TEntity>();
            var state = trackingSet.AttachEntity(
                (TEntity) operation.Result.RemoteEntity.Clone(
                    EntityConfigurationBuilder,
                    typeof(TEntity),
                    RelationshipCloneMode.DoNotClone),
                isOffline);
            if (!isOffline)
            {
                state.Reset();
            }
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
                    property.Reset();
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

        public IQueuedOperation[] GetChanges(object[] entities = null, IProperty[] properties = null)
        {
            var queue = new List<IQueuedOperation>();
            var queuedOperations = GetQueuedOperations(entities, properties).ToArray();
            for (var i = 0; i < queuedOperations.Length; i++)
            {
                var operation = queuedOperations[i];
                var filteredOperation = GetType()
                        .GetMethod(nameof(Filter))
                        .InvokeGeneric(this, new object[]
                        {
                            operation
                        }, operation.Operation.EntityType)
                    as IQueuedOperation;
                if (filteredOperation != null)
                {
                    queue.Add(filteredOperation);
                }
            }

            return queue.ToArray();
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

        internal static void ForAnEntityAcrossAllDataTrackers(
            CompositeKey key, Action<DataTracker, CompositeKey> action, IDataContext dataContext = null)
        {
            // This needs to also accept a CompositeKey
            //var sourceEntity = entity as IEntity;
            //var alreadyEmitted = new Dictionary<string, string>();
            //var dataTrackersDealtWith = new Dictionary<DataTracker, DataTracker>();
            ForAllDataTrackers(dataTracker =>
                {
                    //var keyString = key.AsKeyString();
                    //if (!alreadyEmitted.ContainsKey(keyString))
                    //{
                    //    alreadyEmitted.Add(keyString, keyString);
                    //}
                    action(dataTracker, key);
                },
                dataContext);
            //if (sourceEntity != null && !dataTrackersDealtWith.ContainsKey(DataTracker) &&
            //    !alreadyEmitted.ContainsKey(sourceEntity))
            //{
            //    var entityState = Tracking.TrackingSet<TEntity>().GetEntityState(entity);
            //    action(DataTracker, (EntityState<TEntity>)entityState);
            //}
        }
    }
}