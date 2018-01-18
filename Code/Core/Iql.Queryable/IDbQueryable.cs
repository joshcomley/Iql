using System;
using Iql.Queryable.Data;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.Tracking;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public interface IDbQueryable : IQueryableBase
    {
        IDbQueryable SetTracking(bool enabled);
        IDbQueryable IncludeCount();
        IDbQueryable ExpandAll();
        IDbQueryable ExpandRelationship(string name);
        IDbQueryable ExpandAllSingleRelationships();
        IDbQueryable ExpandAllCollectionCounts();
        IDbQueryable ExpandCollectionCountRelationship(string name);
        TrackingSetCollection TrackingSetCollection { get; }
        Func<IDataStore> DataStoreGetter { get; set; }
        IDataContext DataContext { get; set; }
        EntityConfigurationBuilder Configuration { get; set; }
        ITrackingSet TrackingSet { get; set; }
        bool TrackEntities { get; set; }
        //IDbQueryable Copy();
        //IDbQueryable New();
        //IDbQueryable Skip(int skip);
        //IDbQueryable Take(int take);
        //IDbQueryable Reverse();
        //IDbQueryable Then(IQueryOperation operation);
    }
}