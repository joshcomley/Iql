using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores;
using Iql.Data.Extensions;
using Iql.Data.IqlToIql;
using Iql.Data.Lists;
using Iql.Data.Operations;
using Iql.Data.Queryable;
using Iql.Data.Tracking;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.DisplayFormatting;
using Iql.Entities.Extensions;
using Iql.Entities.Relationships;
using Iql.Entities.Rules.Relationship;
using Iql.Entities.Search;
using Iql.Entities.SpecialTypes;
using Iql.Extensions;
using Iql.Parsing;
using Iql.Parsing.Expressions.QueryExpressions;
using Iql.Parsing.Reduction;
using Iql.Queryable;
using Iql.Queryable.Expressions;
using Iql.Queryable.Operations;

namespace Iql.Data.Context
{
    public class DbQueryable<T> : Queryable<T, DbQueryable<T>, DbList<T>>, IDbQueryable
        where T : class
    {
        private MethodInfo _mappedToListWithResponseAsyncMethod;
        private MethodInfo MappedToListWithResponseAsyncMethod => _mappedToListWithResponseAsyncMethod = _mappedToListWithResponseAsyncMethod ?? typeof(DbQueryable<T>).GetMethod(nameof(MappedToListWithResponseAsync), BindingFlags.Instance | BindingFlags.NonPublic);
        private MethodInfo _mappedCountWithResponseAsyncMethod;
        private MethodInfo MappedCountWithResponseAsyncMethod => _mappedCountWithResponseAsyncMethod = _mappedCountWithResponseAsyncMethod ?? typeof(DbQueryable<T>).GetMethod(nameof(MappedCountWithResponseAsync), BindingFlags.Instance | BindingFlags.NonPublic);

        private ITrackingSet _trackingSet;

        public DbQueryable(
            EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore> dataStoreGetter,
            EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(evaluateContext)
        {
            EntityConfigurationBuilder = entityConfigurationBuilder;
            EntityConfiguration = EntityConfigurationBuilder.GetEntityByType(typeof(T));
            DataContext = dataContext;
            DataStoreGetter = dataStoreGetter;
        }

        public override async Task<DbQueryable<T>> ApplyRelationshipFiltersAsync<TProperty>(IProperty relatedProperty, TProperty entity)
        {
            var query = this;
            var ctx = new RelationshipFilterContext<TProperty>();
            ctx.Owner = entity;
#if TypeScript
            var context = new EvaluateContext(e => ctx);
#endif
            query = await base.ApplyRelationshipFiltersAsync(relatedProperty, entity);
            for (var i = 0; i < relatedProperty.Relationship.ThisEnd.RelationshipMappings.Count; i++)
            {
                var relationshipMapping = relatedProperty.Relationship.ThisEnd.RelationshipMappings[i];
                if (relationshipMapping.UseForFiltering)
                {
                    var filterRule = relationshipMapping.BuildRelationshipFilterRule();
                    var filterFunction = filterRule.Run(ctx);
                    query = query.Where((Expression<Func<T, bool>>)filterFunction
#if TypeScript
                    , context
#endif
                    );
                }
            }

            return query;
        }

        public IEntityConfiguration EntityConfiguration { get; set; }

        public ITrackingSet TrackingSet
        {
            get { return _trackingSet = _trackingSet ?? DataTracker.TrackingSet<T>(); }
            set => _trackingSet = value;
        }

        public Func<IDataStore> DataStoreGetter { get; set; }
        public DataTracker DataTracker => DataContext?.TemporalDataTracker;
        public IDataContext DataContext { get; set; }
        public EntityConfigurationBuilder EntityConfigurationBuilder { get; set; }

        Func<IDataStore> IDbQueryable.DataStoreGetter { get => DataStoreGetter; set => DataStoreGetter = value; }
        public Task<DbQueryable<T>> ApplyRelationshipFiltersByExpressionAsync<TProperty>(
            Expression<Func<TProperty, T>> relatedProperty,
            TProperty entity)
        {
            var relatedConfiguration = this.EntityConfiguration.Builder.GetEntityByType(
                typeof(TProperty) ?? entity.GetType());
            var property = relatedConfiguration.FindNestedPropertyByLambdaExpression(relatedProperty);
            return ApplyRelationshipFiltersAsync(property, entity);
        }

        IDbQueryable IDbQueryable.WithKeys(IEnumerable<object> keys)
        {
            return WithKeys(keys);
        }

        public DbQueryable<T> WithKey(object entityOrKey)
        {
            return Then(new WithKeyOperation(CompositeKey.Ensure(entityOrKey, EntityConfiguration)));
        }

        IDbQueryable IDbQueryable.WithKey(object entityOrKey)
        {
            return WithKey(entityOrKey);
        }
        
        IDbQueryable IDbQueryable.Skip(int amount)
        {
            return Skip(amount);
        }
        
        IDbQueryable IDbQueryable.Take(int amount)
        {
            return Take(amount);
        }

        IDbQueryable IDbQueryable.WhereEquals(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return WhereEquals(expression
#if TypeScript
            , evaluateContext
#endif
            );
        }

        IDbQueryable IDbQueryable.OrderByProperty(string propertyName, bool? descending = null)
        {
            return OrderByProperty(propertyName, descending);
        }

        IDbQueryable IDbQueryable.OrderByPropertyExpression(IqlPropertyExpression property, bool? descending = null)
        {
            return OrderByPropertyExpression(property, descending);
        }

        IDbQueryable IDbQueryable.OrderByQuery(PropertyQueryExpression expression)
        {
            return OrderByQuery(expression);
        }

        IDbQueryable IDbQueryable.OrderByDefault(bool? descending = null, IqlDefaultOrderKind? orderKind = null)
        {
            return OrderByDefault(descending, orderKind);
        }

        IDbQueryable IDbQueryable.WithCompositeKeys(IEnumerable<CompositeKey> keys)
        {
            return WithKeys(keys);
        }

        public async Task LoadRelationshipAsync(T entity, Expression<Func<T, object>> property, Func<IDbQueryable, IDbQueryable> queryFilter = null)
        {
            await LoadRelationshipPropertyAsync(
                entity, DataContext.EntityConfigurationContext.EntityType<T>()
                    .FindPropertyByExpression(property),
                queryFilter);
        }

        public async Task<IList> LoadRelationshipPropertyAsync(
            T entity,
            IProperty property,
            Func<IDbQueryable, IDbQueryable> queryFilter = null)
        {
            if (property.Relationship == null)
            {
                return null;
            }
            return await LoadRelationshipPropertyInternalAsync(property, queryFilter, entity);
        }

        private async Task<IList> LoadRelationshipPropertyInternalAsync(
            IProperty property,
            Func<IDbQueryable, IDbQueryable> queryFilter,
            object entity)
        {
            var query = DataContext.RelationshipPropertyQuery(entity, property);
            if (queryFilter != null)
            {
                query = queryFilter(query);
            }
            return await query.ToListAsync();
        }

        async Task<IList> IDbQueryable.LoadRelationshipAsync(object entity, Expression<Func<object, object>> relationship, Func<IDbQueryable, IDbQueryable> queryFilter = null)
        {
            return await LoadRelationshipPropertyInternalAsync(
                DataContext.EntityConfigurationContext.GetEntityByType(entity.GetType()).FindPropertyByLambdaExpression(relationship),
                queryFilter,
                entity);
        }

        public async Task<Dictionary<IProperty, IList>> LoadRelationshipsAsync(T entity, IEnumerable<EntityRelationship> relationships)
        {
            var dictionary = new Dictionary<IProperty, IList>();
            var list = relationships.ToArray();
            for (var i = 0; i < list.Length; i++)
            {
                var relationship = list[i];
                if (relationship.ThisEnd.Type != typeof(T))
                {
                    continue;
                }
                if (relationship.ThisEnd.IsCollection)
                {
                    var result = await LoadRelationshipPropertyAsync(entity, relationship.ThisEnd.Property);
                    dictionary.Add(relationship.ThisEnd.Property, result);
                }
                else if (relationship.ThisEnd.Property.GetValue(entity) == null &&
                         !relationship.ThisEnd.GetCompositeKey(entity).HasDefaultValue())
                {
                    var result = await LoadRelationshipPropertyAsync(entity, relationship.ThisEnd.Property);
                    dictionary.Add(relationship.ThisEnd.Property, result);
                }
            }

            return dictionary;
        }

        public Task<Dictionary<IProperty, IList>> LoadAllRelationshipsAsync(T entity, LoadRelationshipMode mode = LoadRelationshipMode.Both)
        {
            IEnumerable<EntityRelationship> relationships = EntityConfiguration.AllRelationships;
            switch (mode)
            {
                case LoadRelationshipMode.Collections:
                    relationships = relationships.Where(r => r.ThisEnd.IsCollection);
                    break;
                case LoadRelationshipMode.References:
                    relationships = relationships.Where(r => !r.ThisEnd.IsCollection);
                    break;
            }
            return LoadRelationshipsAsync(entity, relationships);
        }

        Task<Dictionary<IProperty, IList>> IDbQueryable.LoadRelationshipsAsync(object entity, IEnumerable<EntityRelationship> relationships)
        {
            return LoadRelationshipsAsync((T)entity, relationships);
        }

        Task<Dictionary<IProperty, IList>> IDbQueryable.LoadAllRelationshipsAsync(object entity, LoadRelationshipMode mode = LoadRelationshipMode.Both)
        {
            return LoadAllRelationshipsAsync((T)entity, mode);
        }

        Task<IList> IDbQueryable.LoadRelationshipPropertyAsync(object entity, IProperty property, Func<IDbQueryable, IDbQueryable> queryFilter = null)
        {
            return LoadRelationshipPropertyAsync((T)entity, property, queryFilter);
        }

        IDataContext IDbQueryable.DataContext { get => DataContext; set => DataContext = value; }
        ITrackingSet IDbQueryable.TrackingSet { get => TrackingSet; set => TrackingSet = value; }

        public DbQueryable<T> WithKeys(IEnumerable<object> keys)
        {
            return WhereEquals(EntityConfiguration.BuildSearchKeyQuery(keys));
        }

        public DbQueryable<T> Search(string search, IqlSearchKind searchKind = IqlSearchKind.Primary, bool? splitIntoTerms = null, IEnumerable<IqlPropertyPath> excludeProperties = null)
        {
            return WhereEquals(EntityConfiguration.BuildSearchQuery(search, searchKind, splitIntoTerms ?? false, excludeProperties));
        }

        IDbQueryable IDbQueryable.Search(string search, IqlSearchKind searchKind, bool? splitIntoTerms = null, IEnumerable<IqlPropertyPath> excludeProperties = null)
        {
            return Search(search, searchKind, splitIntoTerms ?? false, excludeProperties);
        }

        public DbQueryable<T> SearchRemaining(ISourceRelationshipDetail relationship, object entity, string search, IqlSearchKind searchKind = IqlSearchKind.Primary, bool? splitIntoTerms = null, IEnumerable<IqlPropertyPath> excludeProperties = null, IEnumerable<object> explicitlyExclude = null)
        {
            var otherRelationship =
                relationship.EntityConfiguration.Key.Properties
                    .FirstOrDefault(p => p.Relationship.ThisEnd != relationship)
                    .Relationship
                    .ThisEnd;
            var rootVariableName = "q";
            var query = EntityConfiguration.BuildSearchQuery(search, searchKind, splitIntoTerms ?? false, excludeProperties, rootVariableName);
            var entityToFilterKey = entity is CompositeKey ? (CompositeKey)entity : relationship.OtherSide.GetCompositeKey(entity, true);
            var keyExpressions = new List<IqlIsEqualToExpression>();
            for (var i = 0; i < entityToFilterKey.Keys.Length; i++)
            {
                var key = entityToFilterKey.Keys[i];
                keyExpressions.Add(
                    new IqlIsEqualToExpression(
                        new IqlPropertyExpression(relationship.Constraints[i].Name, new IqlRootReferenceExpression("child")),
                        new IqlLiteralExpression(key.Value, key.ValueType.ToIqlType())));
            }
            var all = new IqlNotExpression(
                new IqlAnyExpression("child",
                    IqlPropertyPath.FromProperty(otherRelationship.OtherSide.Property, rootVariableName).Expression,
                    IqlLambdaExpression.Create(keyExpressions.And())));
            var excludeExpressions = new List<IqlExpression>();
            var attachedEntities = entity is CompositeKey ? null : relationship.OtherSide.Property.GetValue(entity) as IEnumerable;
            var exclude = new List<object>();
            if (attachedEntities != null)
            {
                IDataContext db = null;
                foreach (var item in attachedEntities)
                {
                    db = db ?? DataContextInternal.FindDataContextForEntity(item);
                    if (db != null)
                    {
                        if (db.IsEntityNew(item) == true)
                        {
                            var excludeKey = otherRelationship.GetCompositeKey(item, true);
                            exclude.Add(excludeKey);
                        }
                    }
                }
            }
            if (explicitlyExclude != null)
            {
                var arr = explicitlyExclude.ToArray();
                if (arr.Length > 0)
                {
                    exclude.AddRange(arr);
                }
            }

            if (exclude.Count > 0)
            {
                AppendExcludeExpressions(exclude, excludeExpressions);
            }
            return WhereEquals(IqlQueryBuilder.AndWith(query, all, excludeExpressions.And()));
        }

        private static void AppendExcludeExpressions(List<object> arr, List<IqlExpression> excludeExpressions)
        {
            var dic = new Dictionary<string, string>();
            for (var i = 0; i < arr.Count; i++)
            {
                var entityOrCompositeKey = arr[i];
                CompositeKey compositeKey = null;
                var explicitExcludeExpressions = new List<IqlIsEqualToExpression>();
                if (!(entityOrCompositeKey is CompositeKey))
                {
                    var db = DataContextInternal.FindDataContextForEntity(entityOrCompositeKey);
                    if (db == null || db.IsEntityNew(entityOrCompositeKey) == true)
                    {
                        continue;
                    }

                    compositeKey = db.GetCompositeKey(entityOrCompositeKey);
                }
                else
                {
                    compositeKey = (CompositeKey)entityOrCompositeKey;
                }

                if (compositeKey != null && !dic.ContainsKey(compositeKey.FullKeyString))
                {
                    dic.Add(compositeKey.FullKeyString, compositeKey.FullKeyString);
                    for (var j = 0; j < compositeKey.Keys.Length; j++)
                    {
                        var key = compositeKey.Keys[j];
                        explicitExcludeExpressions.Add(
                            new IqlIsEqualToExpression(
                                new IqlPropertyExpression(key.Name, new IqlRootReferenceExpression("child")),
                                new IqlLiteralExpression(key.Value, key.ValueType.ToIqlType())));
                    }

                    excludeExpressions.Add(new IqlNotExpression(explicitExcludeExpressions.And()));
                }
            }
        }

        IDbQueryable IDbQueryable.SearchRemaining(ISourceRelationshipDetail relationship, object entity, string search, IqlSearchKind searchKind, bool? splitIntoTerms = null, IEnumerable<IqlPropertyPath> excludeProperties = null, IEnumerable<object> explicitlyExclude = null)
        {
            return SearchRemaining(relationship, entity, search, searchKind, splitIntoTerms ?? false, excludeProperties, explicitlyExclude);
        }

        public DbQueryable<T> SearchForDisplayFormatter(string search, IEntityDisplayTextFormatter displayFormatter = null, bool? splitIntoTerms = null)
        {
            var paths =
                (displayFormatter ?? EntityConfiguration.DisplayFormatting.Default).ResolveUniqueTextSearchablePaths(
                    EntityConfiguration);
            return WhereEquals(IqlQueryBuilder.BuildSearchQueryForPropertyPaths(search, paths, splitIntoTerms ?? false));
        }

        IDbQueryable IDbQueryable.SearchForDisplayFormatter(string search, IEntityDisplayTextFormatter formatter = null, bool? splitIntoTerms = null)
        {
            return SearchForDisplayFormatter(search, formatter, splitIntoTerms ?? false);
        }

        public DbQueryable<T> SearchProperties(string search, IEnumerable<IqlPropertyPath> searchFields, bool? splitIntoTerms = null)
        {
            return WhereEquals(IqlQueryBuilder.BuildSearchQueryForProperties(search, searchFields, splitIntoTerms ?? false));
        }

        IDbQueryable IDbQueryable.SearchProperties(string search, IEnumerable<IqlPropertyPath> properties, bool? splitIntoTerms = null)
        {
            return SearchProperties(search, properties, splitIntoTerms ?? false);
        }


        public DbQueryable<T> SearchWithTerms(IqlSearchText searchTerms, IqlSearchKind searchKind = IqlSearchKind.Primary)
        {
            return WhereEquals(EntityConfiguration.BuildSearchQueryWithTerms(searchTerms, searchKind));
        }

        IDbQueryable IDbQueryable.SearchWithTerms(IqlSearchText searchTerms, IqlSearchKind searchKind)
        {
            return SearchWithTerms(searchTerms, searchKind);
        }

        public DbQueryable<T> SearchForDisplayFormatterWithTerms(IqlSearchText searchTerms, IEntityDisplayTextFormatter displayFormatter = null)
        {
            var paths =
                (displayFormatter ?? EntityConfiguration.DisplayFormatting.Default).ResolveUniqueTextSearchablePaths(
                    EntityConfiguration);
            return WhereEquals(IqlQueryBuilder.BuildSearchQueryForPropertyPathsWithTerms(searchTerms, paths));
        }

        IDbQueryable IDbQueryable.SearchForDisplayFormatterWithTerms(IqlSearchText searchTerms, IEntityDisplayTextFormatter formatter = null)
        {
            return SearchForDisplayFormatterWithTerms(searchTerms, formatter);
        }

        public DbQueryable<T> SearchPropertiesWithTerms(IqlSearchText searchTerms, IEnumerable<IqlPropertyPath> searchFields)
        {
            return WhereEquals(IqlQueryBuilder.BuildSearchQueryForPropertiesWithTerms(searchTerms, searchFields));
        }

        IDbQueryable IDbQueryable.SearchPropertiesWithTerms(IqlSearchText searchTerms, IEnumerable<IqlPropertyPath> properties)
        {
            return SearchPropertiesWithTerms(searchTerms, properties);
        }

        public override async Task<T> SingleAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            return (await SingleWithResponseAsync(expression
#if TypeScript
                                , evaluateContext
#endif
                )).Data;
        }

        public async Task<EntityState<T>> SingleStateAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return (await SingleWithResponseAsync(expression
#if TypeScript
                                , evaluateContext
#endif
            )).EntityState;
        }


        public async Task<EntityState<T>> SingleOrDefaultStateAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return (await SingleOrDefaultWithResponseAsync(expression
#if TypeScript
                                , evaluateContext
#endif
            ))?.EntityState;
        }

        public async Task<GetSingleResult<T>> SingleWithResponseAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            var result = await UseWhereIfExists(expression
#if TypeScript
                                , evaluateContext
#endif
                )
                .Take(1)
                .ToListWithResponseAsync();
            return ResolveSingle(result);
        }

        public async Task<GetSingleResult<T>> SingleQueryAsync(WhereQueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            var result = await UseWhereQueryIfExists(expression
#if TypeScript
                                , evaluateContext
#endif
                )
                .Take(1)
                .ToListWithResponseAsync();
            return ResolveSingle(result);
        }

        public override async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            return (await SingleOrDefaultWithResponseAsync(expression
#if TypeScript
                                , evaluateContext
#endif
                )).Data;
        }

        public async Task<GetSingleResult<T>> SingleOrDefaultWithResponseAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            var result = await UseWhereIfExists(expression
#if TypeScript
                                , evaluateContext
#endif
                )
                .Take(1)
                .ToListWithResponseAsync();
            return ResolveSingleOrDefault(result);
        }

        public async Task<GetSingleResult<T>> SingleOrDefaultQueryAsync(WhereQueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            var result = await UseWhereQueryIfExists(expression
#if TypeScript
                                , evaluateContext
#endif
                )
                .Take(1)
                .ToListWithResponseAsync();
            return ResolveSingleOrDefault(result);
        }

        public override async Task<T> FirstAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return (await FirstWithResponseAsync(expression
#if TypeScript
                                , evaluateContext
#endif
            )).Data;
        }

        public async Task<EntityState<T>> FirstStateAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return (await FirstWithResponseAsync(expression
#if TypeScript
                                , evaluateContext
#endif
            )).EntityState;
        }

        public async Task<EntityState<T>> FirstOrDefaultStateAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return (await FirstOrDefaultWithResponseAsync(expression
#if TypeScript
                                , evaluateContext
#endif
            ))?.EntityState;
        }

        public async Task<GetSingleResult<T>> FirstWithResponseAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var result = await UseWhereIfExists(expression
#if TypeScript
                                , evaluateContext
#endif
                )
                .Take(1)
                .ToListWithResponseAsync();
            return ResolveFirst(result);
        }

        public async Task<GetSingleResult<T>> FirstQueryAsync(WhereQueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            var result = await UseWhereQueryIfExists(expression
#if TypeScript
                                , evaluateContext
#endif
                )
                .Take(1)
                .ToListWithResponseAsync();
            return ResolveFirst(result);
        }

        public override async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return (await FirstOrDefaultWithResponseAsync(expression
#if TypeScript
                                , evaluateContext
#endif
            )).Data;
        }

        public async Task<GetSingleResult<T>> FirstOrDefaultWithResponseAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var result = await UseWhereIfExists(expression
#if TypeScript
                                , evaluateContext
#endif
                )
                .Take(1)
                .ToListWithResponseAsync();
            return ResolveFirstOrDefault(result);
        }

        public async Task<GetSingleResult<T>> FirstOrDefaultQueryAsync(WhereQueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            var result = await UseWhereQueryIfExists(expression
#if TypeScript
                                , evaluateContext
#endif
                )
                .Take(1)
                .ToListWithResponseAsync();
            return ResolveFirstOrDefault(result);
        }


        public override async Task<T> LastAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return (await LastWithResponseAsync(expression
#if TypeScript
                                , evaluateContext
#endif
            )).Data;
        }

        public async Task<EntityState<T>> LastStateAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return (await LastWithResponseAsync(expression
#if TypeScript
                                , evaluateContext
#endif
            )).EntityState;
        }

        public async Task<EntityState<T>> LastOrDefaultStateAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return (await LastOrDefaultWithResponseAsync(expression
#if TypeScript
                                , evaluateContext
#endif
            )).EntityState;
        }

        public async Task<GetSingleResult<T>> LastWithResponseAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var result = await UseWhereIfExists(expression
#if TypeScript
                                , evaluateContext
#endif
                )
                .ToListWithResponseAsync();
            return ResolveLast(result);
        }

        public async Task<GetSingleResult<T>> LastQueryAsync(WhereQueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            var result = await UseWhereQueryIfExists(expression
#if TypeScript
                                , evaluateContext
#endif
                )
                .ToListWithResponseAsync();
            return ResolveLast(result);
        }

        public override async Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return (await LastOrDefaultWithResponseAsync(expression
#if TypeScript
                                , evaluateContext
#endif
            )).Data;
        }

        public async Task<GetSingleResult<T>> LastOrDefaultWithResponseAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var result = await UseWhereIfExists(expression
#if TypeScript
                                , evaluateContext
#endif
                )
                .ToListWithResponseAsync();
            return ResolveLastOrDefault(result);
        }


        public async Task<GetSingleResult<T>> LastOrDefaultQueryAsync(WhereQueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            var result = await UseWhereQueryIfExists(expression
#if TypeScript
                                , evaluateContext
#endif
                )
                .ToListWithResponseAsync();
            return ResolveLastOrDefault(result);
        }

        public override DbQueryable<T> OrderByDefault(bool? descending = null, IqlDefaultOrderKind? orderKind = null)
        {
            orderKind = orderKind ?? IqlDefaultOrderKind.Browse;
            var resolvedSort =
                orderKind == IqlDefaultOrderKind.Browse
                ? EntityConfiguration.DefaultBrowseSortExpression
                : EntityConfiguration.DefaultSearchSortExpression;
            var resolvedDescending = descending == null
                ? (orderKind == IqlDefaultOrderKind.Browse
                    ? EntityConfiguration.DefaultBrowseSortDescending
                    : EntityConfiguration.DefaultSearchSortDescending)
                : descending.Value;
            if (string.IsNullOrWhiteSpace(resolvedSort))
            {
                var sortProperty = (EntityConfiguration.ResolveSearchProperties().FirstOrDefault()?.PathToHere ??
                                    EntityConfiguration.Key.Properties.FirstOrDefault()?.Name ??
                                    EntityConfiguration.Properties.FirstOrDefault()?.Name);
                resolvedSort = sortProperty;
            }
            if (string.IsNullOrWhiteSpace(resolvedSort))
            {
                resolvedSort =
                   orderKind == IqlDefaultOrderKind.Browse
                       ? EntityConfiguration.DefaultSearchSortExpression
                       : EntityConfiguration.DefaultBrowseSortExpression;
                if (string.IsNullOrWhiteSpace(resolvedSort))
                {
                    return this;
                }
                resolvedDescending = descending == null
                    ? (orderKind == IqlDefaultOrderKind.Browse
                        ? EntityConfiguration.DefaultSearchSortDescending
                        : EntityConfiguration.DefaultBrowseSortDescending)
                    : descending.Value;
            }
            return OrderByProperty(resolvedSort, resolvedDescending);
        }

        public override IqlPropertyExpression PropertyExpression(string propertyName, string rootReferenceName = null)
        {
            return IqlPropertyPath.FromString(EntityConfiguration.Builder, propertyName, this.EntityConfiguration.TypeMetadata, null, rootReferenceName).Expression;
        }

        public override async Task<bool> AnyQueryAsync(IqlLambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await CountQueryAsync(expression
#if TypeScript
            , evaluateContext
#endif
                   ) > 0;
        }

        public override async Task<bool> AllQueryAsync(IqlLambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var withoutFilterCount = await CountQueryAsync();
            var withFilterCount = await CountQueryAsync(expression
#if TypeScript
                , evaluateContext
#endif
            );
            return withFilterCount == withoutFilterCount;
        }

        public override async Task<long> CountQueryAsync(IqlLambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var set = this;
            if (expression != null)
            {
                set = set.WhereEquals(expression);

            }
            var result = await set.CountWithResponseAsync(null
#if TypeScript
                , evaluateContext
#endif
            );
            return result?.Count ?? -1;
        }

        public override async Task<bool> AnyAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await CountAsync(expression
#if TypeScript
            , evaluateContext
#endif
            ) > 0;
        }

        public override async Task<bool> AllAsync(Expression<Func<T, bool>> expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var withoutFilterCount = await CountAsync(null
#if TypeScript
                , evaluateContext
#endif
            );
            var withFilterCount = await CountAsync(expression
#if TypeScript
                , evaluateContext
#endif
            );
            return withFilterCount == withoutFilterCount;
        }

        public override async Task<long> CountAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var result = await CountWithResponseAsync(expression
#if TypeScript
                , evaluateContext
#endif
            );
            return result?.Count ?? -1;
        }

        public override async Task<DbList<T>> ToListAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var result = await ToListWithResponseAsync(expression
#if TypeScript
                , evaluateContext
#endif
            );
            return result?.Data;
        }

        public async Task<EntityState<T>[]> ToStateListAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var result = await ToListWithResponseAsync(expression
#if TypeScript
                , evaluateContext
#endif
            );
            var data = result?.Data;
            if(data != null)
            {
                return data.States();
            }

            return null;
        }

        async Task<IDbList> IDbQueryable.ToListAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await ToListAsync((Expression<Func<T, bool>>)expression
#if TypeScript
, evaluateContext
#endif
            );
        }

        public override async Task<DbList<T>> AllPagesToListAsync(ProgressNotifier progressNotifier = null,
            Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var result = await AllPagesToListWithResponseAsync(progressNotifier, expression
#if TypeScript
                , evaluateContext
#endif
            );
            return result?.Data;
        }

        async Task<IDbList> IDbQueryable.AllPagesToListAsync(ProgressNotifier progressNotifier = null,
            LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await AllPagesToListAsync(progressNotifier, (Expression<Func<T, bool>>)expression
#if TypeScript
, evaluateContext
#endif
            );
        }

        public async Task<AggregatedGetDataResult<T>> AllPagesToListWithResponseAsync(ProgressNotifier progressNotifier = null,
            Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var results = new List<GetDataResult<T>>();
            GetDataResult<T> response = null;
            progressNotifier?.NotifyProgress(0, false);
            while (true)
            {
                if (response == null)
                {
                    response = await ToListWithResponseAsync(expression);
                }
                results.Add(response);
                if (response.Success)
                {
                    if (response.Data.HasNextPage)
                    {
                        progressNotifier?.NotifyProgress((float)(response.Data.PagingInfo.Page + 1) / (float)response.Data.PagingInfo.PageCount, false);
                        response = await response.Data.NewNextPageQuery().ToListWithResponseAsync(expression);
                    }
                    else
                    {
                        progressNotifier?.NotifyProgress(1, true);
                        break;
                    }
                }
                else
                {
                    progressNotifier?.NotifyProgress(progressNotifier.CurrentProgress, true);
                    break;
                }
            }
            var result = new AggregatedGetDataResult<T>(results.ToArray());
            //while (initialResult.Success)
            //{
            //    if (initialResult.Data.HasNextPage)
            //    {

            //    }
            //    else
            //    {
            //        progressNotifier?.NotifyProgress(1, true);
            //    }
            //}

            return result;
        }

        async Task<IGetDataResult> IDbQueryable.ToListWithResponseAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await ToListWithResponseAsync((Expression<Func<T, bool>>)expression
#if TypeScript
                , evaluateContext
#endif
            );
        }

        async Task<IAggregatedGetDataResult> IDbQueryable.AllPagesToListWithResponseAsync(ProgressNotifier progressNotifier = null,
            LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            return await AllPagesToListWithResponseAsync(progressNotifier,
                (Expression<Func<T, bool>>)expression
#if TypeScript
                , evaluateContext
#endif
                );
        }

        public async Task<CountDataResult<T>> CountWithResponseAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var dbQueryable = this;
            if (expression != null)
            {
                dbQueryable = dbQueryable.Where(expression
#if TypeScript
            , evaluateContext
#endif
                );
            }
            var getDataOperation = new GetDataOperation<T>(dbQueryable, DataContext);
            var specialTypeMap = DataContext.EntityConfigurationContext.GetSpecialTypeMap(EntityConfiguration.Type.Name);
            if (specialTypeMap != null && specialTypeMap.EntityConfiguration != EntityConfiguration)
            {
                var mappedType = specialTypeMap.EntityConfiguration.Type;
                var dbSet = DataContext.GetDbSetByEntityType(mappedType);
                return await (Task<CountDataResult<T>>)MappedCountWithResponseAsyncMethod.InvokeGeneric(dbQueryable,
                    new object[] { dbSet, specialTypeMap, getDataOperation },
                    mappedType);
            }
            return await DataContext.CountAsync(getDataOperation);
        }

        public async Task<GetDataResult<T>> ToListWithResponseAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var dbQueryable = this;
            if (expression != null)
            {
                dbQueryable = dbQueryable.Where(expression
#if TypeScript
            , evaluateContext
#endif
                );
            }
            var getDataOperation = new GetDataOperation<T>(dbQueryable, DataContext);
            var specialTypeMap = DataContext.EntityConfigurationContext.GetSpecialTypeMap(EntityConfiguration.Type.Name);
            if (specialTypeMap != null && specialTypeMap.EntityConfiguration != EntityConfiguration)
            {
                var mappedType = specialTypeMap.EntityConfiguration.Type;
                var dbSet = DataContext.GetDbSetByEntityType(mappedType);
                return await (Task<GetDataResult<T>>)MappedToListWithResponseAsyncMethod.InvokeGeneric(dbQueryable,
                    new object[] { dbSet, specialTypeMap, getDataOperation },
                    mappedType);
            }
            return await DataContext.GetAsync(getDataOperation);
        }

        private async Task<CountDataResult<T>> MappedCountWithResponseAsync<TMap>(
            global::Iql.Queryable.IQueryable<TMap> queryable,
            SpecialTypeDefinition definition,
            GetDataOperation<T> getOperation)
            where TMap : class
        {
            for (var i = 0; i < Operations.Count; i++)
            {
                var operation = Operations[i];
                queryable = (global::Iql.Queryable.IQueryable<TMap>)queryable.Then(operation);
            }

            queryable.MappedFrom = typeof(T);
            var getDataResult = await DataContext.CountAsync(new GetDataOperation<TMap>(queryable, DataContext, typeof(T)));
            var result = new CountDataResult<T>(getDataResult.IsOffline, getOperation, getDataResult.Count, getDataResult.Success);
            return result;
        }

        private async Task<GetDataResult<T>> MappedToListWithResponseAsync<TMap>(
            global::Iql.Queryable.IQueryable<TMap> queryable,
            SpecialTypeDefinition definition,
            GetDataOperation<T> getOperation)
            where TMap : class
        {
            for (var i = 0; i < Operations.Count; i++)
            {
                var operation = Operations[i];
                queryable = (global::Iql.Queryable.IQueryable<TMap>)queryable.Then(operation);
            }

            queryable.MappedFrom = typeof(T);
            var getDataResult = await DataContext.GetAsync(new GetDataOperation<TMap>(queryable, DataContext, typeof(T)));
            var list = new List<T>();
            var entityConfiguration = EntityConfiguration;//DataContext.EntityConfigurationContext.EntityType<T>();
            for (var i = 0; i < getDataResult.Data.Count; i++)
            {
                var item = getDataResult.Data[i];
                var mappedItem = (T)Activator.CreateInstance(typeof(T));
                for (int j = 0; j < entityConfiguration.Properties.Count; j++)
                {
                    IProperty property = entityConfiguration.Properties[j];
                    var mappedProperty = definition.ResolvePropertyMap(property.Name);
                    if (mappedProperty != null)
                    {
                        mappedItem.SetPropertyValueByName(property.Name,
                            item.GetPropertyValueByName(mappedProperty.CustomProperty.Name));
                    }
                }

                list.Add(mappedItem);
            }

            var flattened = new Dictionary<Type, IList>();
            flattened.Add(typeof(T), list);
            var flattenedGetDataResult = new FlattenedGetDataResult<T>(DataTracker, flattened, getOperation, getDataResult.Success);
            flattenedGetDataResult.Root = list;
            flattenedGetDataResult.Queryable = this;
            var dbList = await DataContext.TrackGetDataResultAsync(
                flattenedGetDataResult);
            var result = new GetDataResult<T>(getDataResult.IsOffline, DataTracker, dbList, getOperation, getDataResult.Success);
            return result;
        }

        public EntityState<T> Add(T entity)
        {
            return (EntityState<T>)DataContext.AddEntity(entity);
        }

        //public UpdateEntityResult<T> Update(T entity)
        //{
        //    return DataContext.DataStore.Update(
        //        new UpdateEntityOperation<T>(entity, DataContext));
        //}

        public EntityState<T> Delete(T entity)
        {
            return (EntityState<T>)DataContext.DeleteEntity(entity);
        }

        private DbQueryable<T> UseWhereIfExists(Expression<Func<T, bool>> expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            var queryable = this;
            if (expression != null)
            {
                queryable = Where(expression
#if TypeScript
                                , evaluateContext
#endif
                    );
            }
            return queryable;
        }

        private DbQueryable<T> UseWhereQueryIfExists(QueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            var queryable = this;
            if (expression != null)
            {
                queryable = WhereQuery(expression
#if TypeScript
                                , evaluateContext
#endif
                    );
            }
            return queryable;
        }

        private static GetSingleResult<T> ResolveSingle(GetDataResult<T> result)
        {
            if (result.Success)
            {
                if (result.Data.Count < 1)
                {
                    throw new Exception("No entities returned for \"single\" call");
                }
                if (result.Data.Count > 1)
                {
                    throw new Exception("More than one entity returned for \"single\" call");
                }
            }
            return new GetSingleResult<T>(
                result.Data[0],
                result.Data.States().SingleOrDefault(),
                result.Operation,
                result.Success);
        }

        private static GetSingleResult<T> ResolveSingleOrDefault(GetDataResult<T> result)
        {
            T data;
            if (result.Data.Count < 1)
            {
                data = null;
            }
            else if (result.Data.Count > 1)
            {
                throw new Exception("More than one entity returned for \"single\" call");
            }
            else
            {
                data = result.Data[0];
            }
            return new GetSingleResult<T>(
                data,
                result.Data.States().SingleOrDefault(),
                result.Operation,
                result.Success);
        }

        private static GetSingleResult<T> ResolveFirst(GetDataResult<T> result)
        {
            if (result.Data.Count < 1)
            {
                throw new Exception("No entities returned for \"First\" call");
            }
            return new GetSingleResult<T>(
                result.Data[0],
                result.Data.States().SingleOrDefault(),
                result.Operation,
                result.Success);
        }

        private static GetSingleResult<T> ResolveFirstOrDefault(GetDataResult<T> result)
        {
            var data = result.Data.Count < 1 ? null : result.Data[0];
            return new GetSingleResult<T>(
                data,
                result.Data.States().SingleOrDefault(),
                result.Operation,
                result.Success);
        }

        private static GetSingleResult<T> ResolveLast(GetDataResult<T> result)
        {
            if (result.Data.Count < 1)
            {
                throw new Exception("No entities returned for \"Last\" call");
            }

            var data = result.Data[result.Data.Count];
            return new GetSingleResult<T>(
                data,
                result.Data.States().SingleOrDefault(_ => _ == data),
                result.Operation,
                result.Success);
        }

        private static GetSingleResult<T> ResolveLastOrDefault(GetDataResult<T> result)
        {
            var data = result.Data.Count < 1 ? null : result.Data[result.Data.Count];
            return new GetSingleResult<T>(
                data,
                result.Data.States().SingleOrDefault(_ => _ == data),
                result.Operation,
                result.Success);
        }

        public DbQueryable<T> ClearExpands()
        {
            var copy = Then();
            var noExpandOperations = copy.Operations.Where(_ => !(_ is IExpandOperation));
            copy.Operations.Clear();
            copy.Operations.AddRange(noExpandOperations);
            return copy;
        }

        IDbQueryable IDbQueryable.ClearExpands()
        {
            return ClearExpands();
        }

        public DbQueryable<T> ExpandAllCollectionRelationships()
        {
            return AllCollectionRelationships(
                (queryable, relationship, detail) => queryable.ExpandRelationship(detail.Property.Name));
        }

        IDbQueryable IDbQueryable.ExpandAllCollectionRelationships()
        {
            return ExpandAllCollectionRelationships();
        }

        IDbQueryable IDbQueryable.ExpandCollectionRelationshipCount(string name)
        {
            var isDone = false;
            return AllCollectionRelationships(
                (queryable, relationship, detail) =>
                {
                    if (isDone)
                    {
                        return queryable;
                    }
                    else if (detail.Property.Name == name && detail.CountProperty != null)
                    {
                        return queryable.ExpandRelationship(detail.CountProperty.Name);
                    }
                    else if (detail.CountProperty != null && detail.CountProperty.Name == name)
                    {
                        return queryable.ExpandRelationship(detail.CountProperty.Name);
                    }
                    return queryable;
                });
        }

        public DbQueryable<T> ExpandAllCollectionCounts()
        {
            return AllCollectionRelationships(
                (queryable, relationship, detail) =>
                {
                    if (detail.CountProperty != null)
                    {
                        return queryable.ExpandRelationship(detail.CountProperty.Name);
                    }
                    return queryable;
                });
        }

        public DbQueryable<T> ExpandAllSingleRelationships()
        {
            return AllSingleRelationships(
                (queryable, relationship, detail) => queryable.ExpandRelationship(detail.Property.Name));
        }

        public DbQueryable<T> Expand<TTarget>(
            Expression<Func<T, TTarget>> target)
        {
            return ExpandQuery(new ExpandQueryExpression(target));
        }

        public DbQueryable<T> ExpandForDisplayFormatter(IEntityDisplayTextFormatter displayFormatter = null)
        {
            var query = this;
            var paths =
                (displayFormatter ?? EntityConfiguration.DisplayFormatting.Default).ResolveUniqueRelationshipPaths(
                    EntityConfiguration);
            for (var i = 0; i < paths.Length; i++)
            {
                var map = paths[i];
                query = query.ExpandRelationship(map.RelationshipPathToHere);
            }

            return query;
        }

        IDbQueryable IDbQueryable.ExpandForDisplayFormatter(IEntityDisplayTextFormatter displayFormatter = null)
        {
            return ExpandForDisplayFormatter(displayFormatter);
        }

        public DbQueryable<T> ExpandRelationship(
            string propertyName)
        {
            var expandOperation = DataContext.BuildExpandOperation(typeof(T), propertyName);
            return expandOperation == null ? this : Then(expandOperation);
        }

        //public IqlPropertyExpression PropertyExpression(string propertyName)
        //{
        //    var property = this.Configuration.GetEntityByType(typeof(T)).Properties.Single(p => p.Name == propertyName);
        //    var rootReferenceExpression = new IqlRootReferenceExpression("entity", "");
        //    var propertyExpression = new IqlPropertyExpression(propertyName, typeof(T).Name, property.Type.ToIqlType());
        //    propertyExpression.Parent = rootReferenceExpression;
        //    return propertyExpression;
        //}

        public DbQueryable<T> ExpandAll()
        {
            return AllRelationships(
                (queryable, relationship, detail) => queryable.ExpandRelationship(((IMetadata) detail.Property).Name));
        }

        public DbQueryable<T> ExpandQuery(
            ExpandQueryExpression expression, bool countOnly = false)
        {
            return Then(new ExpandOperation(expression, countOnly));
        }

        public DbQueryable<T> ExpandSingle<TTarget>(
            Expression<Func<T, TTarget>> target,
            Func<DbQueryable<TTarget>, DbQueryable<TTarget>> filter)
            where TTarget : class
        {
            return ExpandQuery(
                new ExpandQueryExpression(
                    target,
                    q => filter((DbQueryable<TTarget>)q)
                    ));
        }

        public DbQueryable<T> ExpandCollection<TTarget>(
            Expression<Func<T, IEnumerable<TTarget>>> target,
            Func<DbQueryable<TTarget>, DbQueryable<TTarget>> filter = null)
            where TTarget : class
        {
            return ExpandQuery(
                new ExpandQueryExpression(
                    target,
                    q => filter == null ? q : filter((DbQueryable<TTarget>)q)
                    ));
        }

        public DbQueryable<T> ExpandCollectionCount<TTarget>(
            Expression<Func<T, IEnumerable<TTarget>>> target,
            Func<DbQueryable<TTarget>, DbQueryable<TTarget>> filter = null)
            where TTarget : class
        {
            return ExpandQuery(
                new ExpandQueryExpression(
                    target,
                    q => filter == null ? q : filter((DbQueryable<TTarget>)q)
                    ),
                true);
        }

        public DbQueryable<T> AllCollectionRelationships(
            Func<DbQueryable<T>, IRelationship, IRelationshipDetail, DbQueryable<T>> action)
        {
            var entityConfig = EntityConfigurationBuilder.GetEntityByType(typeof(T));
            return AllRelationships(
                (queryable, relationship, detail) =>
                {
                    if (entityConfig.FindProperty(((IMetadata) detail.Property).Name).TypeDefinition.IsCollection)
                    {
                        return action(queryable, relationship, detail);
                    }
                    return queryable;
                });
        }

        public DbQueryable<T> AllSingleRelationships(
            Func<DbQueryable<T>, IRelationship, IRelationshipDetail, DbQueryable<T>> action)
        {
            var entityConfig = EntityConfigurationBuilder.GetEntityByType(typeof(T));
            return AllRelationships(
                (queryable, relationship, detail) =>
                {
                    if (!entityConfig.FindProperty(((IMetadata) detail.Property).Name).TypeDefinition.IsCollection)
                    {
                        return action(queryable, relationship, detail);
                    }
                    return queryable;
                });
        }

        public DbQueryable<T> AllRelationships(Func<DbQueryable<T>, IRelationship, IRelationshipDetail, DbQueryable<T>> action)
        {
            var set = this;
            var entityConfiguration = DataContext.EntityConfigurationContext.EntityType<T>();
            foreach (var relationship in entityConfiguration.Relationships)
            {
                var thisEnd = relationship.Source.EntityConfiguration == entityConfiguration
                    ? relationship.Source
                    : relationship.Target;
                set = action(set, relationship, thisEnd);
            }
            return set;
        }

        public override void AddEntity(object entity)
        {
            Add((T)entity);
        }

        IEntityStateBase IDbQueryable.Add(object entity)
        {
            return Add((T)entity);
        }

        async Task<IEntityStateBase> IDbQueryable.SingleStateAsync(LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await SingleStateAsync((Expression<Func<T, bool>>) expression
#if TypeScript
            , evaluateContext
#endif
            );
        }

        async Task<IEntityStateBase> IDbQueryable.SingleOrDefaultStateAsync(LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await SingleOrDefaultStateAsync((Expression<Func<T, bool>>)expression
#if TypeScript
            , evaluateContext
#endif
            );
        }

        async Task<IGetSingleResult> IDbQueryable.SingleWithResponseAsync(LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await SingleWithResponseAsync((Expression<Func<T, bool>>)expression
#if TypeScript
            , evaluateContext
#endif
            );
        }

        async Task<IGetSingleResult> IDbQueryable.SingleOrDefaultWithResponseAsync(LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await SingleOrDefaultWithResponseAsync((Expression<Func<T, bool>>)expression
#if TypeScript
            , evaluateContext
#endif
            );
        }

        async Task<IEntityStateBase> IDbQueryable.FirstStateAsync(LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await FirstStateAsync((Expression<Func<T, bool>>)expression
#if TypeScript
            , evaluateContext
#endif
            );
        }

        async Task<IEntityStateBase> IDbQueryable.FirstOrDefaultStateAsync(LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await FirstOrDefaultStateAsync((Expression<Func<T, bool>>)expression
#if TypeScript
            , evaluateContext
#endif
            );
        }

        async Task<IGetSingleResult> IDbQueryable.FirstWithResponseAsync(LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await FirstWithResponseAsync((Expression<Func<T, bool>>)expression
#if TypeScript
            , evaluateContext
#endif
            );
        }

        async Task<IGetSingleResult> IDbQueryable.FirstOrDefaultWithResponseAsync(LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await FirstOrDefaultWithResponseAsync((Expression<Func<T, bool>>)expression
#if TypeScript
            , evaluateContext
#endif
            );
        }

        async Task<IEntityStateBase> IDbQueryable.LastStateAsync(LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await LastStateAsync((Expression<Func<T, bool>>)expression
#if TypeScript
            , evaluateContext
#endif
            );
        }

        async Task<IEntityStateBase> IDbQueryable.LastOrDefaultStateAsync(LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await LastOrDefaultStateAsync((Expression<Func<T, bool>>)expression
#if TypeScript
            , evaluateContext
#endif
            );
        }

        async Task<IGetSingleResult> IDbQueryable.LastWithResponseAsync(LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await LastWithResponseAsync((Expression<Func<T, bool>>)expression
#if TypeScript
            , evaluateContext
#endif
            );
        }

        async Task<IGetSingleResult> IDbQueryable.LastOrDefaultWithResponseAsync(LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await LastOrDefaultWithResponseAsync((Expression<Func<T, bool>>)expression
#if TypeScript
            , evaluateContext
#endif
            );
        }


        public override void DeleteEntity(object entity)
        {
            Delete((T)entity);
        }

        public override async Task<object> GetWithKeyAsync(object entityOrKey)
        {
            return await WithKey(entityOrKey).SingleOrDefaultAsync();
        }

        public override async Task<IList> GetWithKeysAsync(IEnumerable<object> keys)
        {
            return await WithKeys(keys).ToListAsync();
        }

        public virtual DbQueryable<T> IncludeCount()
        {
            return Then(new IncludeCountOperation());
        }

        public DbQueryable<T> NoTracking()
        {
            return SetTracking(false);
        }

        IDbQueryable IDbQueryable.NoTracking()
        {
            return NoTracking();
        }

        public DbQueryable<T> NoOffline()
        {
            return SetAllowOffline(false);
        }

        IDbQueryable IDbQueryable.NoOffline()
        {
            return NoOffline();
        }

        /// <summary>
        /// Any value here will override the data context's value
        /// A null value will resort to the data context's value
        /// </summary>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public DbQueryable<T> SetTracking(bool? enabled)
        {
            var queryable = Copy();
            queryable.TrackEntities = enabled;
            return queryable;
        }

        public DbQueryable<T> SetAllowOffline(bool? enabled)
        {
            var queryable = Copy();
            queryable.AllowOffline = enabled;
            return queryable;
        }

        public override DbQueryable<T> New()
        {
            var dbQueryable = new DbQueryable<T>(
                EntityConfigurationBuilder,
                DataStoreGetter,
                EvaluateContext,
                DataContext);
            return dbQueryable;
        }

        IDbQueryable IDbQueryable.SetTracking(bool enabled)
        {
            return SetTracking(enabled);
        }

        IDbQueryable IDbQueryable.SetAllowOffline(bool enabled)
        {
            return SetAllowOffline(enabled);
        }

        IDbQueryable IDbQueryable.IncludeCount()
        {
            return IncludeCount();
        }

        IDbQueryable IDbQueryable.ExpandAll()
        {
            return ExpandAll();
        }

        IDbQueryable IDbQueryable.ExpandRelationship(string name)
        {
            return ExpandRelationship(name);
        }

        IDbQueryable IDbQueryable.ExpandAllSingleRelationships()
        {
            return ExpandAllSingleRelationships();
        }

        IDbQueryable IDbQueryable.ExpandAllCollectionCounts()
        {
            return ExpandAllCollectionRelationships();
        }

        //IDbQueryable IDbQueryable.Copy()
        //{
        //    return Copy();
        //}

        //IDbQueryable IDbQueryable.New()
        //{
        //    return New();
        //}

        //IDbQueryable IDbQueryable.Skip(int skip)
        //{
        //    return Skip(skip);
        //}

        //IDbQueryable IDbQueryable.Take(int take)
        //{
        //    return Take(take);
        //}

        //IDbQueryable IDbQueryable.Reverse()
        //{
        //    return Reverse();
        //}

        //IDbQueryable IDbQueryable.Then(IQueryOperation queryOperation)
        //{
        //    return Then(queryOperation);
        //}

        public DbQueryable<T> WithCompositeKeys(IEnumerable<CompositeKey> keys)
        {
            return WithKeys(keys.Select(c => (object)c));
        }

        public async Task<DbList<T>> GetWithCompositeKeysAsync(IEnumerable<CompositeKey> keys)
        {
            return (await GetWithCompositeKeysWithResponseAsync(keys)).Data;
        }

        public async Task<GetDataResult<T>> GetWithCompositeKeysWithResponseAsync(IEnumerable<CompositeKey> keys)
        {
            return await WithCompositeKeys(keys).ToListWithResponseAsync();
        }

        public DbQueryable<T> WithCompositeKey(CompositeKey key)
        {
            return Then(new WithKeyOperation(key));
        }

        IDbQueryable IDbQueryable.WithCompositeKey(CompositeKey key)
        {
            return WithCompositeKey(key);
        }

        public async Task<T> GetWithCompositeKeyAsync(CompositeKey key)
        {
            return (await GetWithCompositeKeyWithResponseAsync(key)).Data;
        }

        public async Task<GetSingleResult<T>> GetWithCompositeKeyWithResponseAsync(CompositeKey key)
        {
            return await WithCompositeKey(key).SingleOrDefaultWithResponseAsync();
        }

        public async Task<IEntityState<T>> GetStateWithCompositeKeyAsync(CompositeKey key)
        {
            return (await GetWithCompositeKeyWithResponseAsync(key)).EntityState;
        }

        public async Task<IEntityState<T>> GetStateWithKeyAsync(object key)
        {
            var result = await GetWithKeyWithResponseAsync(key);
            return result?.EntityState;
        }

        public async Task<GetSingleResult<T>> GetWithKeyWithResponseAsync(object key)
        {
            return await WithKey(key).SingleOrDefaultWithResponseAsync();
        }

        public override async Task<IqlDataSetQueryExpression> ToIqlAsync(
            IExpressionToIqlConverter expressionConverter = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            expressionConverter = expressionConverter ?? IqlExpressionConversion.DefaultExpressionConverter();
            var queryExpression = new IqlDataSetQueryExpression(typeof(T).GetFullName());
            queryExpression.DataSet = new IqlDataSetReferenceExpression
            {
                Name = DataContext.GetDbSetPropertyNameByEntityType(EntityConfiguration.Type)
            };
            var typeMetadata = EntityConfiguration.TypeMetadata;
            for (var i = 0; i < Operations.Count; i++)
            {
                var operation = Operations[i];
                IqlExpression iql = null;
                if (operation is IExpressionQueryOperation)
                {
                    var expressionQueryOperation = operation as IExpressionQueryOperation;
                    iql =
                        expressionQueryOperation.Expression ??
                            expressionConverter
                                .ConvertQueryExpressionToIql<T>(
                                    expressionQueryOperation.QueryExpression,
                                    EntityConfiguration.Builder
#if TypeScript
                                    , operation.EvaluateContext ?? EvaluateContext ?? DataContext.EvaluateContext
#endif
                                )
                                .Expression;
                }
                if (operation is OrderByOperation)
                {
                    queryExpression.OrderBys = queryExpression.OrderBys ?? new List<IqlOrderByExpression>();
                    var orderByExpression = new IqlOrderByExpression(iql);
                    queryExpression.OrderBys.Add(orderByExpression);
                    orderByExpression.Descending = (operation as OrderByOperation).IsDescending();
                }
                else if (operation is WhereOperation)
                {
                    if (queryExpression.Filter == null)
                    {
                        queryExpression.Filter = iql;
                    }
                    else
                    {
                        queryExpression.Filter = new IqlAndExpression(iql, queryExpression.Filter);
                    }
                }
                else if (operation is IExpandOperation)
                {
                    queryExpression.Expands = queryExpression.Expands ?? new List<IqlExpandExpression>();
                    var iqlExpandExpression = new IqlExpandExpression();
                    iqlExpandExpression.NavigationProperty = iql.TryGetPropertyExpression();
                    var propertytPath = IqlPropertyPath.FromPropertyExpression(EntityConfiguration.Builder, typeMetadata, iqlExpandExpression.NavigationProperty);
                    var expressionQueryOperatiton = operation as IExpandOperation;
                    var expandQueryExpression = expressionQueryOperatiton.QueryExpression as IExpandQueryExpression;
                    var property = propertytPath.Property.EntityProperty();
                    if (property.Kind.HasFlag(IqlPropertyKind.Count) || expressionQueryOperatiton.CountOnly)
                    {
                        iqlExpandExpression.Count = true;
                        property = property.Relationship.ThisEnd.CountProperty;
                        iqlExpandExpression.NavigationProperty.PropertyName = property.Name;
                    }
                    var expandEntityType = property.Relationship.OtherEnd.EntityConfiguration.Type;
                    var expandDbSet = DataContext.GetDbSetByEntityType(expandEntityType)
                        .ClearOperations()
                        ;
                    var expandQueryable = expandQueryExpression.Queryable(expandDbSet);
                    var expandQuery = await expandQueryable.ToIqlAsync();
                    iqlExpandExpression.Query = expandQuery;
                    queryExpression.Expands.Add(iqlExpandExpression);
                }
                else if (operation is SkipOperation)
                {
                    queryExpression.Skip = queryExpression.Skip ?? 0;
                    queryExpression.Skip += (operation as SkipOperation).Skip;
                }
                else if (operation is TakeOperation)
                {
                    queryExpression.Take = queryExpression.Take ?? 0;
                    queryExpression.Take += (operation as TakeOperation).Take;
                }
                else if (operation is IncludeCountOperation)
                {
                    queryExpression.IncludeCount = true;
                }
                else if (operation is WithKeyOperation)
                {
                    var withKey = operation as WithKeyOperation;
                    var isEqualToOperations = new List<IqlIsEqualToExpression>();
                    for (var index = 0; index < withKey.Key.Keys.Length; index++)
                    {
                        var keyPart = withKey.Key.Keys[index];
                        isEqualToOperations.Add(new IqlIsEqualToExpression(
                            IqlExpression.GetPropertyExpression(keyPart.Name),
                            // TODO: Use configuration context and property path to resolve type
                            new IqlLiteralExpression(keyPart.Value, keyPart.ValueType?.ToIqlType() ?? IqlType.Unknown)));
                    }
                    queryExpression.WithKey = new IqlWithKeyExpression(isEqualToOperations);
                }
            }

            queryExpression = queryExpression.CloneIql();
            var result = (IqlDataSetQueryExpression)new IqlReducer(
#if TypeScript
                    evaluateContext ?? EvaluateContext ?? DataContext.EvaluateContext
#endif
                // TODO: Add reducer registry

                //queryOperation.getExpression().evaluateContext || this.evaluateContext
                )
                .ReduceStaticContent(queryExpression);

            await result.ProcessAsync(typeMetadata, EntityConfigurationBuilder, DataContext, null, false, MappedFrom);
            return result;
        }
    }
}