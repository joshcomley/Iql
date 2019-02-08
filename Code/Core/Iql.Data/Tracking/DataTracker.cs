using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores;
using Iql.Data.Extensions;
using Iql.Data.Lists;
using Iql.Data.Relationships;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Extensions;

namespace Iql.Data.Tracking
{
    public class DataTracker
    {
        public EntityConfigurationBuilder EntityConfigurationBuilder { get; }
        public bool TrackEntities { get; }
        public bool Offline { get; }
        public IDataContext DataContext { get; set; }
        public TrackingSetCollection Tracking => _tracking ?? (_tracking = new TrackingSetCollection(this, TrackEntities));

        private RelationshipObserver _relationshipObserver;
        private TrackingSetCollection _tracking;

        public IRelationshipObserver RelationshipObserver
        {
            get
            {
                if (_relationshipObserver == null)
                {
                    _relationshipObserver = new RelationshipObserver(Tracking, TrackEntities);
                }
                return _relationshipObserver;
            }
        }

        public static DataTracker[] AllDataTrackers()
        {
            return _allDataTrackers.ToArray();

        }
        private static List<DataTracker> _allDataTrackers { get; }
            = new List<DataTracker>();

        public DataTracker(EntityConfigurationBuilder entityConfigurationBuilder, bool trackEntities, bool offline = false)
        {
            EntityConfigurationBuilder = entityConfigurationBuilder;
            TrackEntities = trackEntities;
            Offline = offline;
            if (trackEntities)
            {
                _allDataTrackers.Add(this);
            }
        }

        private List<TEntity> ResolveRoot<TEntity>(FlattenedGetDataResult<TEntity> response) where TEntity : class
        {
            if (response.Data.ContainsKey(response.EntityType))
            {
                return (List<TEntity>)response.Data[response.EntityType];
            }

            return null;
        }

        public DbList<TEntity> TrackGetDataResult<TEntity>(FlattenedGetDataResult<TEntity> response) where TEntity : class
        {
            response.Root = response.Root ?? ResolveRoot(response);
#if TypeScript
            response.Data = EntityConfigurationBuilder.EnsureTypedResult(response.Data);
#endif
            var dbList = new DbList<TEntity>();
            dbList.SourceQueryable = (DbQueryable<TEntity>)response.Queryable;
            // Flatten before we merge because the merge will update the result data set with
            // tracked data
            if (response.IsSuccessful())
            {
                var trackResults = dbList.SourceQueryable != null &&
                                   TrackEntities;
                if (dbList.SourceQueryable != null && dbList.SourceQueryable.TrackEntities.HasValue)
                {
                    trackResults = dbList.SourceQueryable.TrackEntities.Value;
                }

                void Track(DataTracker tracker)
                {
                    if (tracker.Offline)
                    {
                        foreach (var entityType in response.Data)
                        {
                            foreach (var entity in entityType.Value)
                            {
                                var trackingSet = tracker.Tracking.TrackingSetByType(entityType.Key);
                                trackingSet.TrackEntity(
                                    entity.Clone(EntityConfigurationBuilder, entityType.Key, RelationshipCloneMode.DoNotClone), entity,
                                    isNew: false, onlyMergeWithExisting: false);
                            }
                        }
                    }
                    else if (tracker == this)
                    {
                        if (!trackResults)
                        {
                            tracker = new DataTracker(EntityConfigurationBuilder, false);
                        }
                        response.Root = tracker.TrackResults(response.Data, response.Root);
                    }
                    else
                    {
                        foreach (var entityType in response.Data)
                        {
                            foreach (var entity in entityType.Value)
                            {
                                var trackingSet = tracker.Tracking.TrackingSetByType(entityType.Key);
                                if (trackingSet.IsMatchingEntityTracked(entity))
                                {
                                    var state = trackingSet.FindMatchingEntityState(entity);
                                    trackingSet.TrackEntity(state.Entity, entity, isNew: false, onlyMergeWithExisting: true);
                                    state.Reset();
                                }
                            }
                        }
                    }
                }

                ForAllDataTrackers(tracker =>
                {
                    Track(tracker);
                });
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
            var allDataTrackers = DataTracker.AllDataTrackers();
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
                newList.Add((TEntity)item.Key);
            }
            var data = new Dictionary<Type, IList>();
            List<IEntityStateBase> states = null;
            if (responseRoot != null)
            {
                var trackingResult = TrackCollection(responseRoot, typeof(TEntity), data, mergeExistingOnly);
                states = trackingResult.States;
                responseRoot = (List<TEntity>)trackingResult.Data.ToList(typeof(TEntity));
            }
            foreach (var dataSet in responseData)
            {
                TrackCollection(dataSet.Value, dataSet.Key, data, mergeExistingOnly);
            }
            if (!mergeExistingOnly)
            {
                RelationshipObserver.ObserveAll(data);
                if (states != null)
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

        class TrackCollectionResult
        {
            public IList Data { get; set; }
            public List<IEntityStateBase> States { get; set; }

            public TrackCollectionResult(IList data, List<IEntityStateBase> states)
            {
                Data = data;
                States = states;
            }
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
                var trackingSet = Tracking.TrackingSetByType(type);
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
            var set = Tracking.TrackingSet<T>();
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
            var set = Tracking.TrackingSet<T>();
            set.RemoveEntity(entity);
            RelationshipObserver.Unobserve(entity, typeof(T));
        }

        public void ApplyAdd<TEntity>(QueuedAddEntityOperation<TEntity> operation) where TEntity : class
        {
            Tracking.TrackingSet<TEntity>().TrackEntity(operation.Operation.Entity.Clone(
                EntityConfigurationBuilder,
                typeof(TEntity),
                RelationshipCloneMode.DoNotClone));
        }

        public void ApplyUpdate<TEntity>(QueuedUpdateEntityOperation<TEntity> operation) where TEntity : class
        {
            var changedProperties = operation.Operation.GetChangedProperties();
            var trackingSet = Tracking.TrackingSet<TEntity>();
            var ourState = trackingSet.FindMatchingEntityState(operation.Operation.Entity);
            foreach (var property in changedProperties)
            {
                property.Property.SetValue(ourState.Entity, property.LocalValue);
            }
        }

        public void ApplyDelete<TEntity>(QueuedDeleteEntityOperation<TEntity> operation) where TEntity : class
        {
            var ourState = Tracking.TrackingSet<TEntity>().FindMatchingEntityState(operation.Operation.Entity);
            var theirState = operation.Operation.EntityState;
            ourState.MarkedForDeletion = theirState.MarkedForDeletion;
            ourState.MarkedForCascadeDeletion = theirState.MarkedForCascadeDeletion;
        }

        public void Reset(Dictionary<Type, IList> entities)
        {
            Tracking.Reset(entities);
        }
    }
}