using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.Queryable;
using Iql.Queryable.Data.Tracking;

namespace Iql.Queryable.Data.Lists
{
    public interface IDbQueryable : IQueryableBase
    {
        void DeleteEntity(object entity);
        void AddEntity(object entity);
        Task<object> WithKeyAsync(object key);
        Task<IList> LoadRelationshipPropertyAsync(object entity, IProperty relationship, Func<IDbQueryable, IDbQueryable> queryFilter = null);
        Task<IList> LoadRelationshipAsync(object entity, Expression<Func<object, object>> relationship, Func<IDbQueryable, IDbQueryable> queryFilter = null);
        Task<Dictionary<IProperty, IList>> LoadAllRelationshipsAsync(object entity, LoadRelationshipMode mode = LoadRelationshipMode.Both);
        Task<Dictionary<IProperty, IList>> LoadRelationshipsAsync(object entity, IEnumerable<RelationshipMatch> relationships);
        IDbQueryable SetTracking(bool enabled);
        IDbQueryable IncludeCount();
        IDbQueryable ExpandAll();
        IDbQueryable ExpandRelationship(string name);
        IDbQueryable ExpandAllSingleRelationships();
        IDbQueryable ExpandAllCollectionCounts();
        IDbQueryable WithKeys(IEnumerable<object> keys);
        IDbQueryable WithCompositeKeys(IEnumerable<CompositeKey> keys);
        IDbQueryable Search(string search, PropertySearchKind searchKind);
        IDbQueryable SearchProperties(string search, IEnumerable<IProperty> properties);
        TrackingSetCollection TrackingSetCollection { get; }
        Func<IDataStore> DataStoreGetter { get; set; }
        IDataContext DataContext { get; set; }
        EntityConfigurationBuilder EntityConfigurationBuilder { get; set; }
        IEntityConfiguration EntityConfiguration { get; set; }
        ITrackingSet TrackingSet { get; set; }
    }
}