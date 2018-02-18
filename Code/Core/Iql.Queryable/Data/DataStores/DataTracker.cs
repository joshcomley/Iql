using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Extensions;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.Tracking;
using Iql.Queryable.Native;

namespace Iql.Queryable.Data.DataStores
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
                    _relationshipObserver = new RelationshipObserver(DataStore, TrackEntities);
                }
                return _relationshipObserver;
            }
        }

        public DataTracker(IDataStore dataStore, bool trackEntities)
        {
            TrackEntities = trackEntities;
            DataStore = dataStore;
        }

        public void TrackResults<TEntity>(IFlattenedGetDataResult response)
        {
            Dictionary<Type, IList> data;
            if (!response.Data.ContainsKey(typeof(TEntity)))
            {
                response.Data.Add(typeof(TEntity), new List<TEntity>());
            }

            var rootDictionary = new Dictionary<object, object>();
            foreach (var item in response.Data[typeof(TEntity)])
            {
                rootDictionary.Add(item, item);
            }
            foreach (var item in response.Root)
            {
                if (rootDictionary.ContainsKey(item))
                {
                    rootDictionary.Remove(item);
                }
            }

            var newList = new List<TEntity>();
            response.Data[typeof(TEntity)] = newList;
            foreach (var item in rootDictionary)
            {
                newList.Add((TEntity)item.Key);
            }
            if (TrackEntities)
            {
                data = new Dictionary<Type, IList>();
                response.Root = TrackCollection(response.Root, response.EntityType, data).ToList(response.EntityType);
                // TODO: Implement tracking
                foreach (var dataSet in response.Data)
                {
                    TrackCollection(dataSet.Value, dataSet.Key, data);
                }
            }
            else
            {
                data = response.Data;
            }
            RelationshipObserver.ObserveAll(data);
        }

        private IList TrackCollection(IList set, Type type, Dictionary<Type, IList> data)
        {
            if (set.Count > 0)
            {
#if TypeScript
                set = DataContext.EnsureTypedListByType(set, type, null, null, false, true);
#endif
                var trackingSet = Tracking.TrackingSetByType(type);
                var states = trackingSet.TrackEntities(set, false);
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
    }
}