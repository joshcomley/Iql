using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        public void Track(object entity)
        {
            var entityType = entity.GetType();
            var set = TrackingSet(entityType);
            set.Track(entity);
        }

        public void TrackWithClone(object entity, object clone)
        {
            var entityType = entity.GetType();
            var set = TrackingSet(entityType);
            set.TrackWithClone(entity, clone);
        }

        public ITrackingSet TrackingSet(Type entityType)
        {
            var set = (ITrackingSet) typeof(TrackingSetCollection).GetRuntimeMethods()
                .Single(m => m.Name == nameof(GetSet))
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
    }
}