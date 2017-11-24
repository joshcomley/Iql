using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.Data.Tracking
{
    public class TrackingSetCollection
    {
        public TrackingSetCollection(IDataContext dataContext)
        {
            DataContext = dataContext;
            SetsMap = new Dictionary<string, ITrackingSet>();
            Sets = new List<ITrackingSet>();
        }

        public Dictionary<string, ITrackingSet> SetsMap { get; set; }
        public List<ITrackingSet> Sets { get; set; }
        private IDataContext DataContext { get; }

        public List<IUpdateEntityOperation> GetChanges(bool reset = false)
        {
            ClearParents();
            var changes = new Dictionary<Type, List<IUpdateEntityOperation>>();
            var setsChecked = new List<ITrackingSet>();

            while (true)
            {
                var sets = Sets.ToList();
                foreach (var set in sets)
                {
                    if (!setsChecked.Contains(set))
                    {
                        if (!changes.ContainsKey(set.EntityType))
                        {
                            changes.Add(set.EntityType, new List<IUpdateEntityOperation>());
                        }
                        changes[set.EntityType].AddRange(set.GetChangesInternal());
                        setsChecked.Add(set);
                    }
                }
                if (Sets.Count == sets.Count)
                {
                    break;
                }
            }
            var allChanges = new List<IUpdateEntityOperation>();
            // At this point we must sanitise the changes
            foreach (var type in changes)
            {
                allChanges.AddRange(changes[type.Key]);
                foreach (var change in type.Value)
                {
                    var relationshipChanges =
                        change.EntityState.ChangedProperties.Where(p => p.Property.Kind == PropertyKind.Relationship)
                            .ToList();
                    var relationshipKeyChanges =
                        change.EntityState.ChangedProperties.Where(p => p.Property.Kind == PropertyKind.RelationshipKey)
                            .ToList();
                    int a = 0;
                }
            }
            return allChanges;
        }

        public TrackingSet<T> GetSet<T>() where T : class
        {
            var type = typeof(T);
            if (!SetsMap.ContainsKey(type.Name))
            {
                var set = new TrackingSet<T>(DataContext, this);
                SetsMap[type.Name] = set;
                Sets.Add(set);
            }
            return SetsMap[type.Name] as TrackingSet<T>;
        }

        public bool IsTracked(object entity, Type entityType)
        {
            var set = TrackingSet(entityType);
            var trackedEntity = set.FindTrackedEntity(entity);
            return trackedEntity != null && trackedEntity.Entity == entity;
        }

        public void Track(object entity, Type entityType)
        {
            var flattenObjectGraph = DataContext.EntityConfigurationContext.FlattenObjectGraph(entity, entityType);
            foreach (var obj in flattenObjectGraph)
            {
                var set = TrackingSet(obj.EntityType);
                set.Track(obj.Entity);
            }
        }

        public ITrackingSet TrackingSet(Type entityType)
        {
            var set = (ITrackingSet) typeof(TrackingSetCollection).GetRuntimeMethods()
                .First(m => m.Name == nameof(GetSet))
                .MakeGenericMethod(entityType)
                .Invoke(this, new object[]
                {
#if TypeScript
                    entityType
#endif
                });
            return set;
        }

        public object FindClone(object entity)
        {
            if (entity == null)
            {
                return null;
            }
            var type = entity.GetType();
            foreach (var set in Sets)
            {
                if (set.EntityType == type)
                {
                    return set.FindClone(entity);
                }
            }
            return null;
        }

        public ITrackedEntity FindEntity(object entity)
        {
            if (entity == null)
            {
                return null;
            }
            var type = entity.GetType();
            foreach (var set in Sets)
            {
                if (set.EntityType == type)
                {
                    return set.FindTrackedEntity(entity);
                }
            }
            return null;
        }

        internal void ClearParents()
        {
            _parents = new Dictionary<object, Dictionary<string, object>>();
        }

        /// <summary>
        /// First we look up an entity.
        /// Then we look up the relationship property that we're concerned with.
        /// </summary>
        private Dictionary<object, Dictionary<string, object>> _parents = new Dictionary<object, Dictionary<string, object>>();
        /// <summary>
        /// Part of the integrity check.
        /// If an entity is assigned to multiple parents when the relationship only allows one
        /// then we throw an exception.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="parent"></param>
        /// <param name="property"></param>
        internal void RecordParent(object entity, object parent, string property)
        {
            if (!_parents.ContainsKey(entity))
            {
                _parents.Add(entity, new Dictionary<string, object>());
            }
            if (_parents[entity].ContainsKey(property) && _parents[entity][property] != parent)
            {
                throw new DuplicateParentException(entity);
            }
            _parents[entity].Add(property, parent);
        }
    }
}