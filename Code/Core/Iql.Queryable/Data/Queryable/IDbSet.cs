using System;
using System.Collections.Generic;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.Lists;
using Iql.Queryable.Data.Tracking;

namespace Iql.Queryable.Data.Queryable
{
    public interface IDbSet : IDbQueryable
    {
        IDbSet SetTracking(bool enabled);
        IDbSet IncludeCount();
        IDbSet ExpandAll();
        IDbSet ExpandRelationship(string name);
        IDbSet ExpandAllSingleRelationships();
        IDbSet ExpandAllCollectionCounts();
        IDbSet WithKeys(IEnumerable<object> keys);
        IDbSet Search(string search, PropertySearchKind searchKind);
        IDbSet SearchProperties(string search, IEnumerable<IProperty> properties);
        TrackingSetCollection TrackingSetCollection { get; }
        Func<IDataStore> DataStoreGetter { get; set; }
        IDataContext DataContext { get; set; }
        EntityConfigurationBuilder EntityConfigurationBuilder { get; set; }
        IEntityConfiguration EntityConfiguration { get; set; }
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