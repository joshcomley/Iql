using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores;
using Iql.Data.Search;
using Iql.Data.Tracking;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.DisplayFormatting;
using Iql.Queryable;

namespace Iql.Data.Lists
{
    public interface IDbQueryable : IQueryableBase
    {
        void DeleteEntity(object entity);
        IEntityStateBase Add(object entity);
        Task<IGetDataResult> ToListWithResponseAsync();
        Task<object> GetWithKeyAsync(object key);
        Task<IList> GetWithKeysAsync(IEnumerable<object> keys);
        Task<IList> LoadRelationshipPropertyAsync(object entity, IProperty relationship, Func<IDbQueryable, IDbQueryable> queryFilter = null);
        Task<IList> LoadRelationshipAsync(object entity, Expression<Func<object, object>> relationship, Func<IDbQueryable, IDbQueryable> queryFilter = null);
        Task<Dictionary<IProperty, IList>> LoadAllRelationshipsAsync(object entity, LoadRelationshipMode mode = LoadRelationshipMode.Both);
        Task<Dictionary<IProperty, IList>> LoadRelationshipsAsync(object entity, IEnumerable<EntityRelationship> relationships);
        IDbQueryable SetTracking(bool enabled);
        IDbQueryable IncludeCount();
        IDbQueryable ExpandAll();
        IDbQueryable ExpandForDisplayFormatter(IEntityDisplayTextFormatter displayFomatter = null);
        IDbQueryable ExpandRelationship(string name);
        IDbQueryable ExpandAllSingleRelationships();
        IDbQueryable ExpandAllCollectionCounts();
        IDbQueryable WithKeys(IEnumerable<object> keys);
        IDbQueryable WithKey(object entityOrKey);
        IDbQueryable WithCompositeKeys(IEnumerable<CompositeKey> keys);
        IDbQueryable WithCompositeKey(CompositeKey key);
        IDbQueryable Search(string search, PropertySearchKind searchKind, bool? splitIntoTerms = null);
        IDbQueryable SearchForDisplayFormatter(string search, IEntityDisplayTextFormatter formatter = null, bool? splitIntoTerms = null);
        IDbQueryable SearchProperties(string search, IEnumerable<IProperty> properties, bool? splitIntoTerms = null);
        IDbQueryable SearchWithTerms(IEnumerable<SearchTerm> searchTerms, PropertySearchKind searchKind);
        IDbQueryable SearchForDisplayFormatterWithTerms(IEnumerable<SearchTerm> searchTerms, IEntityDisplayTextFormatter formatter = null);
        IDbQueryable SearchPropertiesWithTerms(IEnumerable<SearchTerm> searchTerms, IEnumerable<IProperty> properties);
        DataTracker DataTracker { get; }
        Func<IDataStore> DataStoreGetter { get; set; }
        IDataContext DataContext { get; set; }
        EntityConfigurationBuilder EntityConfigurationBuilder { get; set; }
        IEntityConfiguration EntityConfiguration { get; set; }
        ITrackingSet TrackingSet { get; set; }
    }
}