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
            bool offline = false)
        {
            EntityConfigurationBuilder = entityConfigurationBuilder;
            Name = name;
            Offline = offline;
            SetsMap = new Dictionary<string, ITrackingSet>();
            Sets = new List<ITrackingSet>();
            //Id = Guid.NewGuid().ToString();
            _allDataTrackers.Add(this);
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
                var set = (ITrackingSet) typeof(DataTracker).GetRuntimeMethods()
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

            return (TrackingSet<T>) SetsMap[type.Name];
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
                changes.Add((IQueuedOperation) queuedOperation);
            });
            GetUpdates(entities, properties).ForEach(update =>
            {
                var queuedOperation =
                    Activator.CreateInstance(
                        typeof(QueuedUpdateEntityOperation<>).MakeGenericType(update.EntityType), update, null);
                changes.Add((IQueuedOperation) queuedOperation);
            });
            GetInserts(entities).ForEach(insert =>
            {
                var queuedOperation =
                    Activator.CreateInstance(
                        typeof(QueuedAddEntityOperation<>).MakeGenericType(insert.EntityType), insert, null);
                changes.Add((IQueuedOperation) queuedOperation);
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

        private List<TEntity> ResolveRoot<TEntity>(FlattenedGetDataResult<TEntity> response) where TEntity : class
        {
            if (response.Data.ContainsKey(response.EntityType))
            {
                return (List<TEntity>) response.Data[response.EntityType];
            }

            return null;
        }

        public DbList<TEntity> TrackGetDataResult<TEntity>(FlattenedGetDataResult<TEntity> response)
            where TEntity : class
        {
            response.Root = response.Root ?? ResolveRoot(response);
#if TypeScript
            response.Data = EntityConfigurationBuilder.EnsureTypedResult(response.Data);
#endif
            var dbList = new DbList<TEntity>();
            dbList.SourceQueryable = (DbQueryable<TEntity>) response.Queryable;
            // Flatten before we merge because the merge will update the result data set with
            // tracked data
            if (response.IsSuccessful())
            {
                var trackResults = dbList.SourceQueryable != null;
                if (dbList.SourceQueryable != null && dbList.SourceQueryable.TrackEntities.HasValue)
                {
                    trackResults = dbList.SourceQueryable.TrackEntities.Value;
                }

                void Track(DataTracker tracker)
                {
                    if (tracker == this)
                    {
                        if (!trackResults)
                        {
                            tracker = new DataTracker(EntityConfigurationBuilder, "No Tracking");
                        }

                        response.Root = tracker.TrackResults(response.IsOffline, response.Data, response.Root);
                    }
                    else if (tracker.Offline)
                    {
                        // If the tracker 
                        foreach (var entityType in response.Data)
                        {
                            foreach (var entity in entityType.Value)
                            {
                                var trackingSet = tracker.TrackingSetByType(entityType.Key);
                                trackingSet.TrackEntity(
                                    entity.Clone(EntityConfigurationBuilder, entityType.Key,
                                        RelationshipCloneMode.DoNotClone), entity,
                                    response.IsOffline, false);
                            }
                        }
                    }
                    else
                    {
                        foreach (var entityType in response.Data)
                        {
                            foreach (var entity in entityType.Value)
                            {
                                var trackingSet = tracker.TrackingSetByType(entityType.Key);
                                if (trackingSet.IsMatchingEntityTracked(entity))
                                {
                                    var state = trackingSet.FindMatchingEntityState(entity);
                                    trackingSet.TrackEntity(state.Entity, entity, false, true);
                                    state.Reset();
                                }
                            }
                        }
                    }
                }

                if (Offline)
                {
                    Track(this);
                }
                else
                {
                    ForAllDataTrackers(tracker => { Track(tracker); });
                }

                if (response.Root != null)
                {
                    dbList.AddRange(response.Root);
                }
            }

            return dbList;
        }

        internal void ForAllDataTrackers(Action<DataTracker> action)
        {
            var dataTrackersDealtWith = new Dictionary<DataTracker, DataTracker>();
            var allDataTrackers = AllDataTrackers();
            foreach (var dataTracker in allDataTrackers)
            {
                if (!dataTrackersDealtWith.ContainsKey(dataTracker))
                {
                    dataTrackersDealtWith.Add(dataTracker, dataTracker);
                }

                if (dataTracker.DataContext != null &&
                    dataTracker.DataContext.EntityConfigurationContext == EntityConfigurationBuilder &&
                    dataTracker.DataContext.SynchronicityKey == DataContext.SynchronicityKey)
                {
                    action(dataTracker);
                }
            }
        }

        public List<TEntity> TrackResults<TEntity>(
            bool isOffline,
            Dictionary<Type, IList> responseDataOrig,
            List<TEntity> responseRoot = null,
            bool mergeExistingOnly = false)
        {
            var responseData = new Dictionary<Type, IList>();
            foreach (var keyValue in responseDataOrig)
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
                newList.Add((TEntity) item.Key);
            }

            var data = new Dictionary<Type, IList>();
            List<IEntityStateBase> states = null;
            if (responseRoot != null)
            {
                var trackingResult = TrackCollection(responseRoot, typeof(TEntity), data, mergeExistingOnly);
                states = trackingResult.States;
                responseRoot = (List<TEntity>) trackingResult.Data.ToList(typeof(TEntity));
            }

            foreach (var dataSet in responseData)
            {
                TrackCollection(dataSet.Value, dataSet.Key, data, mergeExistingOnly);
            }

            if (!mergeExistingOnly)
            {
                RelationshipObserver.ObserveAll(data);
                if (!isOffline && states != null)
                {
                    for (var i = 0; i < states.Count; i++)
                    {
                        var state = states[i];
                        state.Reset();
                    }
                }
            }

            return responseRoot;
        }

        private TrackCollectionResult TrackCollection(
            IList set, Type type, Dictionary<Type, IList> data, bool mergeExistingOnly, bool reset = true)
        {
            var states = new List<IEntityStateBase>();
            if (set.Count > 0)
            {
#if TypeScript
                set = EntityConfigurationBuilder.EnsureTypedListByType(set, type, null, null, false, true);
#endif
                var trackingSet = TrackingSetByType(type);
                states = trackingSet.TrackEntities(set, false, !mergeExistingOnly, mergeExistingOnly);
                if (reset)
                {
                    trackingSet.ResetAll(states);
                }

                set = states.Select(s => s.Entity).EnumerableToList(type);
                if (data.ContainsKey(type))
                {
                    foreach (var item in set)
                    {
                        if (!data[type].Contains(item))
                        {
                            data[type].Add(item);
                        }
                    }
                }
                else
                {
                    data.Add(type, set);
                }
            }

            return new TrackCollectionResult(set, states);
        }

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
                RemoveEntity((T) state.Entity);
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
            var state = trackingSet.TrackEntity(
                operation.Result.RemoteEntity.Clone(
                    EntityConfigurationBuilder,
                    typeof(TEntity),
                    RelationshipCloneMode.DoNotClone),
                null,
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
                trackingSet.RemoveEntity((TEntity) ourState.Entity);
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
    }
}