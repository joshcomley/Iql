using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Data.Context;
using Iql.Data.DataStores;
using Iql.Data.Relationships;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Extensions;

namespace Iql.Data.Tracking
{
    public class DataTracker
    {
        public bool TrackEntities { get; }
        public IDataContext DataContext => DataStore.DataContext;
        public IDataStore DataStore { get; set; }
        public TrackingSetCollection Tracking => _tracking ?? (_tracking = new TrackingSetCollection(DataStore, TrackEntities));

        private RelationshipObserver _relationshipObserver;
        private TrackingSetCollection _tracking;

        public IRelationshipObserver RelationshipObserver
        {
            get
            {
                if (_relationshipObserver == null)
                {
                    _relationshipObserver = new RelationshipObserver(DataContext, Tracking, TrackEntities);
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

        public DataTracker(IDataStore dataStore, bool trackEntities)
        {
            TrackEntities = trackEntities;
            if (trackEntities)
            {
                _allDataTrackers.Add(this);
            }
            DataStore = dataStore;
        }

        public List<TEntity> TrackResults<TEntity>(
            Dictionary<Type, IList> responseDataOrig,
            List<TEntity> responseRoot = null,
            bool mergeExistingOnly = false)
        {
            Dictionary<Type, IList> data;
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
            data = new Dictionary<Type, IList>();
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
            IList set, Type type, Dictionary<Type, IList> data, bool mergeExistingOnly)
        {
            var states = new List<IEntityStateBase>();
            if (set.Count > 0)
            {
#if TypeScript
                set = DataContext.EnsureTypedListByType(set, type, null, null, false, true);
#endif
                var trackingSet = Tracking.TrackingSetByType(type);
                states = trackingSet.TrackEntities(set, false, !mergeExistingOnly, mergeExistingOnly);
                trackingSet.ResetAll(states);
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
    }
}