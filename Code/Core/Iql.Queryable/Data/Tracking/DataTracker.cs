using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Extensions;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.Relationships;

namespace Iql.Queryable.Data.Tracking
{
    public class DataTracker
    {
        public bool TrackEntities { get; }
        public IDataContext DataContext => DataStore.DataContext;
        public IDataStore DataStore { get; set; }
        public TrackingSetCollection Tracking => _tracking ?? (_tracking = new TrackingSetCollection(DataStore));

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

        internal static List<DataTracker> AllDataTrackers { get; }
            = new List<DataTracker>();
        public DataTracker(IDataStore dataStore, bool trackEntities)
        {
            AllDataTrackers.Add(this);
            TrackEntities = trackEntities;
            DataStore = dataStore;
        }

        public List<TEntity> TrackResults<TEntity>(
            Dictionary<Type, IList> responseData, 
            List<TEntity> responseRoot = null,
            bool mergeExistingOnly = false)
        {
            Dictionary<Type, IList> data;
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
            if (TrackEntities)
            {
                data = new Dictionary<Type, IList>();
                if (responseRoot != null)
                {
                    responseRoot = (List<TEntity>) TrackCollection(responseRoot, typeof(TEntity), data, mergeExistingOnly).ToList(typeof(TEntity));
                }
                // TODO: Implement tracking
                foreach (var dataSet in responseData)
                {
                    TrackCollection(dataSet.Value, dataSet.Key, data, mergeExistingOnly);
                }
            }
            else
            {
                data = responseData;
            }

            if (!mergeExistingOnly)
            {
                RelationshipObserver.ObserveAll(data);
            }
            return responseRoot;
        }

        private IList TrackCollection(
            IList set, Type type, Dictionary<Type, IList> data, bool mergeExistingOnly)
        {
            if (set.Count > 0)
            {
#if TypeScript
                set = DataContext.EnsureTypedListByType(set, type, null, null, false, true);
#endif
                var trackingSet = Tracking.TrackingSetByType(type);
                var states = trackingSet.TrackEntities(set, false, !mergeExistingOnly, mergeExistingOnly);
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

            return set;
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