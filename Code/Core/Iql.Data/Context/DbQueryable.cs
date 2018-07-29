using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
using Iql.Entities.Relationships;
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
        private ITrackingSet _trackingSet;
        public DbQueryable(
            EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore> dataStoreGetter,
            EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(evaluateContext)
        {
            EntityConfigurationBuilder = entityConfigurationBuilder;
            EntityConfiguration = EntityConfigurationBuilder.GetEntityByType(typeof(T));
            DataContext = dataContext;
            DataStoreGetter = dataStoreGetter;
            TrackingSetCollection = dataContext?.DataStore?.Tracking;
            //TrackingSet = TrackingSetCollection.GetSet<T>();
        }

        public IEntityConfiguration EntityConfiguration { get; set; }

        public ITrackingSet TrackingSet
        {
            get { return _trackingSet = _trackingSet ?? TrackingSetCollection.TrackingSet<T>(); }
            set => _trackingSet = value;
        }

        public Func<IDataStore> DataStoreGetter { get; set; }
        public TrackingSetCollection TrackingSetCollection { get; }
        public IDataContext DataContext { get; set; }
        public EntityConfigurationBuilder EntityConfigurationBuilder { get; set; }

        TrackingSetCollection IDbQueryable.TrackingSetCollection => TrackingSetCollection;

        Func<IDataStore> IDbQueryable.DataStoreGetter { get => DataStoreGetter; set => DataStoreGetter = value; }
        IDbQueryable IDbQueryable.WithKeys(IEnumerable<object> keys)
        {
            return WithKeys(keys);
        }

        IDbQueryable IDbQueryable.WithCompositeKeys(IEnumerable<CompositeKey> keys)
        {
            return WithKeys(keys);
        }

        IDbQueryable IDbQueryable.Search(string search, PropertySearchKind searchKind)
        {
            return Search(search, searchKind);
        }
        IDbQueryable IDbQueryable.SearchProperties(string search, IEnumerable<IProperty> properties)
        {
            return SearchProperties(search, properties);
        }

        public async Task LoadRelationshipAsync(T entity, Expression<Func<T, object>> property, Func<IDbQueryable, IDbQueryable> queryFilter = null)
        {
            await LoadRelationshipPropertyAsync(
                entity, DataContext.EntityConfigurationContext.EntityType<T>()
                    .FindPropertyByExpression(property),
                queryFilter);
        }

        public async Task<IList> LoadRelationshipPropertyAsync(T entity, IProperty property, Func<IDbQueryable, IDbQueryable> queryFilter = null)
        {
            if (property.Relationship == null)
            {
                return null;
            }
            var otherDbSet = DataContext.GetDbSetByEntityType(property.Relationship.OtherEnd.Type);
            var root = new IqlRootReferenceExpression();
            var expressions = new List<IqlExpression>();
            var thisEndConstraints = property.Relationship.ThisEnd.Constraints.ToArray();
            var otherEndConstraints = property.Relationship.OtherEnd.Constraints.ToArray();
            for (var i = 0; i < thisEndConstraints.Length; i++)
            {
                expressions.Add(new IqlIsEqualToExpression(
                    new IqlPropertyExpression(otherEndConstraints[i].Name, root),
                    new IqlLiteralExpression(thisEndConstraints[i].GetValue(entity))
                ));
            }

            var iqlLambdaExpression = new IqlLambdaExpression
            {
                Body = expressions.And(),
                Parameters = new List<IqlRootReferenceExpression>()
            };
            iqlLambdaExpression.Parameters.Add(new IqlRootReferenceExpression());
            var query = (IDbQueryable)otherDbSet.WhereEquals(iqlLambdaExpression);
            if (queryFilter != null)
            {
                query = queryFilter(query);
            }
            return (IList)await query.ToListAsync();
        }

        async Task<IList> IDbQueryable.LoadRelationshipAsync(object entity, Expression<Func<object, object>> relationship, Func<IDbQueryable, IDbQueryable> queryFilter = null)
        {
            return await LoadRelationshipPropertyAsync((T)entity, DataContext.EntityConfigurationContext.GetEntityByType(entity.GetType())
                .FindPropertyByExpression(relationship),
                queryFilter);
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

        public async Task<Dictionary<IProperty, IList>> LoadAllRelationshipsAsync(T entity, LoadRelationshipMode mode = LoadRelationshipMode.Both)
        {
            IEnumerable<EntityRelationship> relationships = EntityConfiguration.AllRelationships();
            switch (mode)
            {
                case LoadRelationshipMode.Collections:
                    relationships = relationships.Where(r => r.ThisEnd.IsCollection);
                    break;
                case LoadRelationshipMode.References:
                    relationships = relationships.Where(r => !r.ThisEnd.IsCollection);
                    break;
            }
            return await LoadRelationshipsAsync(entity, relationships);
        }

        async Task<Dictionary<IProperty, IList>> IDbQueryable.LoadRelationshipsAsync(object entity, IEnumerable<EntityRelationship> relationships)
        {
            return await LoadRelationshipsAsync((T)entity, relationships);
        }

        async Task<Dictionary<IProperty, IList>> IDbQueryable.LoadAllRelationshipsAsync(object entity, LoadRelationshipMode mode = LoadRelationshipMode.Both)
        {
            return await LoadAllRelationshipsAsync((T)entity, mode);
        }

        async Task<IList> IDbQueryable.LoadRelationshipPropertyAsync(object entity, IProperty property, Func<IDbQueryable, IDbQueryable> queryFilter = null)
        {
            return await LoadRelationshipPropertyAsync((T)entity, property, queryFilter);
        }

        IDataContext IDbQueryable.DataContext { get => DataContext; set => DataContext = value; }
        ITrackingSet IDbQueryable.TrackingSet { get => TrackingSet; set => TrackingSet = value; }

        public DbQueryable<T> WithKeys(IEnumerable<object> keys)
        {
            return WhereEquals(EntityConfiguration.BuildSearchKeyQuery(keys));
        }

        public DbQueryable<T> Search(string search, PropertySearchKind searchKind = PropertySearchKind.Primary)
        {
            return WhereEquals(EntityConfiguration.BuildSearchQuery(search, searchKind));
        }

        public DbQueryable<T> SearchProperties(string search, IEnumerable<IProperty> searchFields)
        {
            return WhereEquals(IqlQueryBuilder.BuildSearchPropertiesQuery(search, searchFields));
        }

        public async Task<T> SingleAsync(Expression<Func<T, bool>> expression = null
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
                .ToListWithResponseAsync();
            return ResolveSingle(result);
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> expression = null
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
                .ToListWithResponseAsync();
            return ResolveSingleOrDefault(result);
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> expression = null
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
                .ToListWithResponseAsync();
            return ResolveFirst(result);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression = null
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
                .ToListWithResponseAsync();
            return ResolveFirstOrDefault(result);
        }


        public async Task<T> LastAsync(Expression<Func<T, bool>> expression = null
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

        public async Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> expression = null
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

        public override DbQueryable<T> OrderByDefault(bool descending = false)
        {
            return this.OrderByProperty(EntityConfiguration.ResolveSearchProperties().First().Name, descending);
        }

        public override IqlPropertyExpression PropertyExpression(string propertyName)
        {
            return IqlPropertyPath.FromString(propertyName, this.EntityConfiguration).Expression;
        }

        public override async Task<DbList<T>> ToListAsync()
        {
            var result = await ToListWithResponseAsync();
            return result?.Data;
        }

        public async Task<GetDataResult<T>> ToListWithResponseAsync()
        {
            return await DataContext.DataStore.GetAsync(new GetDataOperation<T>(this, DataContext));
        }

        public EntityState<T> Add(T entity)
        {
            return DataContext.DataStore.Add(entity);
        }

        //public UpdateEntityResult<T> Update(T entity)
        //{
        //    return DataContext.DataStore.Update(
        //        new UpdateEntityOperation<T>(entity, DataContext));
        //}

        public EntityState<T> Delete(T entity)
        {
            return DataContext.DataStore.Delete(entity);
        }

        public async Task<SaveChangesResult> SaveChangesAsync(T entity)
        {
            return await DataContext.DataStore.SaveChangesAsync(
                new SaveChangesOperation(DataContext));
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
                result.Operation,
                result.Success);
        }

        private static GetSingleResult<T> ResolveFirstOrDefault(GetDataResult<T> result)
        {
            var data = result.Data.Count < 1 ? null : result.Data[0];
            return new GetSingleResult<T>(
                data,
                result.Operation,
                result.Success);
        }

        private static GetSingleResult<T> ResolveLast(GetDataResult<T> result)
        {
            if (result.Data.Count < 1)
            {
                throw new Exception("No entities returned for \"Last\" call");
            }
            return new GetSingleResult<T>(
                result.Data[result.Data.Count],
                result.Operation,
                result.Success);
        }

        private static GetSingleResult<T> ResolveLastOrDefault(GetDataResult<T> result)
        {
            var data = result.Data.Count < 1 ? null : result.Data[result.Data.Count];
            return new GetSingleResult<T>(
                data,
                result.Operation,
                result.Success);
        }


        public DbQueryable<T> ExpandAllCollectionCounts()
        {
            return AllCollectionRelationships(
                (queryable, relationship, detail) => queryable.ExpandRelationship(detail.Property.Name));
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

        public DbQueryable<T> ExpandRelationship(
            string propertyName)
        {
            var expandOperation = DataContext.BuildExpandOperation(typeof(T), propertyName);
            return Then(expandOperation);
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
                (queryable, relationship, detail) => queryable.ExpandRelationship(detail.Property.Name));
        }

        public DbQueryable<T> ExpandQuery(
            ExpandQueryExpression expression)
        {
            return Then(new ExpandOperation(expression));
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

        public DbQueryable<T> AllCollectionRelationships(
            Func<DbQueryable<T>, IRelationship, IRelationshipDetail, DbQueryable<T>> action)
        {
            var entityConfig = EntityConfigurationBuilder.GetEntityByType(typeof(T));
            return AllRelationships(
                (queryable, relationship, detail) =>
                {
                    if (entityConfig.FindProperty(detail.Property.Name).TypeDefinition.IsCollection)
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
                    if (!entityConfig.FindProperty(detail.Property.Name).TypeDefinition.IsCollection)
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

        public override void DeleteEntity(object entity)
        {
            Delete((T)entity);
        }

        public override async Task<object> GetWithKeyAsync(object key)
        {
            return await Then(new WithKeyOperation(CompositeKey.Ensure(key, EntityConfiguration))).SingleOrDefaultAsync();
        }

        public override async Task<IList> GetWithKeysAsync(IEnumerable<object> keys)
        {
            return await WithKeys(keys).ToListAsync();
        }

        public virtual DbQueryable<T> IncludeCount()
        {
            return Then(new IncludeCountOperation());
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
            return ExpandAllCollectionCounts();
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

        public async Task<T> GetWithCompositeKeyAsync(CompositeKey key)
        {
            return (await GetWithCompositeKeyWithResponseAsync(key)).Data;
        }

        public async Task<GetSingleResult<T>> GetWithCompositeKeyWithResponseAsync(CompositeKey key)
        {
            return await WithCompositeKey(key).SingleOrDefaultWithResponseAsync();
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
                                .ConvertQueryExpressionToIql<T>(expressionQueryOperation.QueryExpression
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
                    var propertytPath = IqlPropertyPath.FromPropertyExpression(EntityConfiguration, iqlExpandExpression.NavigationProperty);
                    var expressionQueryOperatiton = operation as IExpressionQueryOperation;
                    var expandQueryExpression = expressionQueryOperatiton.QueryExpression as IExpandQueryExpression;
                    var property = propertytPath.Property;
                    if (property.Kind.HasFlag(PropertyKind.Count))
                    {
                        iqlExpandExpression.Count = true;
                        property = property.Relationship.ThisEnd.CountProperty;
                        iqlExpandExpression.NavigationProperty.PropertyName = property.Name;
                    }
                    var expandEntityType = property.Relationship.OtherEnd.EntityConfiguration.Type;
                    var expandDbSet = DataContext.GetDbSetByEntityType(expandEntityType);
                    var expandQueryable = expandQueryExpression.Queryable(expandDbSet);
                    var expandQuery = await expandQueryable.ToIqlAsync();
                    iqlExpandExpression.Query = expandQuery;
                    queryExpression.Expands.Add(iqlExpandExpression);
                }
                else if (operation is SkipOperation)
                {
                    queryExpression.Skip = (operation as SkipOperation).Skip;
                }
                else if (operation is TakeOperation)
                {
                    queryExpression.Take = (operation as TakeOperation).Take;
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
                            // TODO: Use configuratiton context and property path to resolve type
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

            var parser = new IqlToIqlParserInstance(EntityConfiguration);
            parser.Parse(result);
            return result;
        }
    }
}