using System.Collections.Generic;

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
                var set = new TrackingSet<T>(DataContext);
                SetsMap[type.Name] = set;
                Sets.Add(set);
            }
            return SetsMap[type.Name] as TrackingSet<T>;
        }
    }
}