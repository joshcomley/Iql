using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores;
using Iql.Data.Tracking;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.DisplayFormatting;
using Iql.Entities.Relationships;
using Iql.Entities.Search;
#if TypeScript
using Iql.Parsing;
#endif
using Iql.Queryable;

namespace Iql.Data.Lists
{
    public interface IDbQueryable : IQueryableBase
    {
        void DeleteEntity(object entity);
        IEntityStateBase Add(object entity);
        Task<IEntityStateBase> SingleStateAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<IEntityStateBase> SingleOrDefaultStateAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<IGetSingleResult> SingleWithResponseAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<IGetSingleResult> SingleOrDefaultWithResponseAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<IEntityStateBase> FirstStateAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<IEntityStateBase> FirstOrDefaultStateAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<IGetSingleResult> FirstWithResponseAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<IGetSingleResult> FirstOrDefaultWithResponseAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<IEntityStateBase> LastStateAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<IEntityStateBase> LastOrDefaultStateAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<IGetSingleResult> LastWithResponseAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<IGetSingleResult> LastOrDefaultWithResponseAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        
        IDbQueryable WhereEquals(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        IDbQueryable Skip(int skip);
        
        IDbQueryable Take(int take);

        Task<IDbList> ToListAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<IDbList> AllPagesToListAsync(ProgressNotifier progressNotifier = null,
            LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<IGetDataResult> ToListWithResponseAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<IAggregatedGetDataResult> AllPagesToListWithResponseAsync(ProgressNotifier progressNotifier = null,
            LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<object> GetWithKeyAsync(object key);
        Task<IList> GetWithKeysAsync(IEnumerable<object> keys);
        Task<IList> LoadRelationshipPropertyAsync(object entity, IProperty relationship, Func<IDbQueryable, IDbQueryable> queryFilter = null);
        Task<IList> LoadRelationshipAsync(object entity, Expression<Func<object, object>> relationship, Func<IDbQueryable, IDbQueryable> queryFilter = null);
        Task<Dictionary<IProperty, IList>> LoadAllRelationshipsAsync(object entity, LoadRelationshipMode mode = LoadRelationshipMode.Both);
        Task<Dictionary<IProperty, IList>> LoadRelationshipsAsync(object entity, IEnumerable<EntityRelationship> relationships);
        IDbQueryable SetAllowOffline(bool enabled);
        IDbQueryable NoOffline();
        IDbQueryable NoTracking();
        IDbQueryable SetTracking(bool enabled);
        IDbQueryable IncludeCount();
        IDbQueryable ClearExpands();
        IDbQueryable ExpandAll();
        IDbQueryable ExpandForDisplayFormatter(IEntityDisplayTextFormatter displayFomatter = null);
        IDbQueryable ExpandRelationship(string name);
        IDbQueryable ExpandCollectionRelationshipCount(string name);
        IDbQueryable ExpandAllSingleRelationships();
        IDbQueryable ExpandAllCollectionCounts();
        IDbQueryable ExpandAllCollectionRelationships();
        IDbQueryable WithKeys(IEnumerable<object> keys);
        IDbQueryable WithKey(object entityOrKey);
        IDbQueryable WithCompositeKeys(IEnumerable<CompositeKey> keys);
        IDbQueryable WithCompositeKey(CompositeKey key);
        IDbQueryable Search(string search, IqlSearchKind searchKind, bool? splitIntoTerms = null, IEnumerable<IqlPropertyPath> excludeProperties = null);
        IDbQueryable SearchRemaining(ISourceRelationshipDetail relationship, object entity, string search, IqlSearchKind searchKind, bool? splitIntoTerms = null, IEnumerable<IqlPropertyPath> excludeProperties = null, IEnumerable<object> explicitlyExclude = null);
        IDbQueryable SearchForDisplayFormatter(string search, IEntityDisplayTextFormatter formatter = null, bool? splitIntoTerms = null);
        IDbQueryable SearchProperties(string search, IEnumerable<IqlPropertyPath> properties, bool? splitIntoTerms = null);
        IDbQueryable SearchWithTerms(IqlSearchText searchTerms, IqlSearchKind searchKind);
        IDbQueryable SearchForDisplayFormatterWithTerms(IqlSearchText searchTerms, IEntityDisplayTextFormatter formatter = null);
        IDbQueryable SearchPropertiesWithTerms(IqlSearchText searchTerms, IEnumerable<IqlPropertyPath> properties);
        DataTracker DataTracker { get; }
        Func<IDataStore> DataStoreGetter { get; set; }
        IDataContext DataContext { get; set; }
        EntityConfigurationBuilder EntityConfigurationBuilder { get; set; }
        IEntityConfiguration EntityConfiguration { get; set; }
        ITrackingSet TrackingSet { get; set; }
    }
}