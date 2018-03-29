using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Parsing;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Data.Lists;
using Iql.Queryable.Data.Tracking;
using Iql.Queryable.Data.Tracking.State;
using Iql.Queryable.Expressions.QueryExpressions;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.Queryable
{
    public class DbQueryable<T> : Queryable<T, DbQueryable<T>>, IDbSet
        where T : class
    {
        private ITrackingSet _trackingSet;
        public bool TrackEntities { get; set; } = true;
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

        TrackingSetCollection IDbSet.TrackingSetCollection => TrackingSetCollection;

        Func<IDataStore> IDbSet.DataStoreGetter { get => DataStoreGetter; set => DataStoreGetter = value; }
        IDbSet IDbSet.WithKeys(IEnumerable<object> keys)
        {
            return WithKeys(keys);
        }
        IDbSet IDbSet.Search(string search, PropertySearchKind searchKind)
        {
            return Search(search, searchKind);
        }
        IDbSet IDbSet.SearchProperties(string search, IEnumerable<IProperty> properties)
        {
            return SearchProperties(search, properties);
        }
        IDataContext IDbSet.DataContext { get => DataContext; set => DataContext = value; }
        ITrackingSet IDbSet.TrackingSet { get => TrackingSet; set => TrackingSet = value; }
        bool IDbSet.TrackEntities { get => TrackEntities; set => TrackEntities = value; }

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

        public async Task<T> Single(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            return (await SingleWithResponse(expression
#if TypeScript
                                , evaluateContext
#endif
                )).Data;
        }

        public async Task<GetSingleResult<T>> SingleWithResponse(Expression<Func<T, bool>> expression = null
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
                .ToListWithResponse();
            return ResolveSingle(result);
        }

        public async Task<GetSingleResult<T>> SingleQuery(WhereQueryExpression expression
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
                .ToListWithResponse();
            return ResolveSingle(result);
        }

        public async Task<T> SingleOrDefault(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            return (await SingleOrDefaultWithResponse(expression
#if TypeScript
                                , evaluateContext
#endif
                )).Data;
        }

        public async Task<GetSingleResult<T>> SingleOrDefaultWithResponse(Expression<Func<T, bool>> expression = null
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
                .ToListWithResponse();
            return ResolveSingleOrDefault(result);
        }

        public async Task<GetSingleResult<T>> SingleOrDefaultQuery(WhereQueryExpression expression
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
                .ToListWithResponse();
            return ResolveSingleOrDefault(result);
        }

        public async Task<T> First(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return (await FirstWithResponse(expression
#if TypeScript
                                , evaluateContext
#endif
            )).Data;
        }

        public async Task<GetSingleResult<T>> FirstWithResponse(Expression<Func<T, bool>> expression = null
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
                .ToListWithResponse();
            return ResolveFirst(result);
        }

        public async Task<GetSingleResult<T>> FirstQuery(WhereQueryExpression expression
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
                .ToListWithResponse();
            return ResolveFirst(result);
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return (await FirstOrDefaultWithResponse(expression
#if TypeScript
                                , evaluateContext
#endif
            )).Data;
        }

        public async Task<GetSingleResult<T>> FirstOrDefaultWithResponse(Expression<Func<T, bool>> expression = null
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
                .ToListWithResponse();
            return ResolveFirstOrDefault(result);
        }

        public async Task<GetSingleResult<T>> FirstOrDefaultQuery(WhereQueryExpression expression
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
                .ToListWithResponse();
            return ResolveFirstOrDefault(result);
        }


        public async Task<T> Last(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return (await LastWithResponse(expression
#if TypeScript
                                , evaluateContext
#endif
            )).Data;
        }

        public async Task<GetSingleResult<T>> LastWithResponse(Expression<Func<T, bool>> expression = null
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
                .ToListWithResponse();
            return ResolveLast(result);
        }

        public async Task<GetSingleResult<T>> LastQuery(WhereQueryExpression expression
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
                .ToListWithResponse();
            return ResolveLast(result);
        }

        public async Task<T> LastOrDefault(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return (await LastOrDefaultWithResponse(expression
#if TypeScript
                                , evaluateContext
#endif
            )).Data;
        }

        public async Task<GetSingleResult<T>> LastOrDefaultWithResponse(Expression<Func<T, bool>> expression = null
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
                .ToListWithResponse();
            return ResolveLastOrDefault(result);
        }


        public async Task<GetSingleResult<T>> LastOrDefaultQuery(WhereQueryExpression expression
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
                .ToListWithResponse();
            return ResolveLastOrDefault(result);
        }

        public override DbQueryable<T> OrderByDefault(bool descending = false)
        {
            return this.OrderByProperty(EntityConfiguration.ResolveSearchProperties().First().Name, descending);
        }

        public override async Task<DbList<T>> ToList()
        {
            var result = await ToListWithResponse();
            return result?.Data;
        }

        public async Task<GetDataResult<T>> ToListWithResponse()
        {
            return await DataContext.DataStore.Get(new GetDataOperation<T>(this, DataContext));
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

        public async Task<SaveChangesResult> SaveChanges(T entity)
        {
            return await DataContext.DataStore.SaveChanges(
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
            var entityConfiguration = DataContext.EntityConfigurationContext.GetEntityByType(typeof(T));
            var expandOperation = entityConfiguration.BuildExpandOperation(propertyName);
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
            var entityConfiguration = DataContext.EntityConfigurationContext.GetEntity<T>();
            foreach (var relationship in entityConfiguration.Relationships)
            {
                var thisEnd = relationship.Source.Configuration == entityConfiguration
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

        public override async Task<object> WithKey(object key)
        {
            var compositeKey = GetCompositeKeyFromSingularKey(key);
            return await Then(new WithKeyOperation(compositeKey)).SingleOrDefault();
        }

        protected virtual CompositeKey GetCompositeKeyFromSingularKey(object key)
        {
            if (key is CompositeKey)
            {
                return key as CompositeKey;
            }
            var compositeKey = new CompositeKey(1);
            var propertyName = DataContext.EntityConfigurationContext.GetEntity<T>().Key.Properties.First().Name;
            compositeKey.Keys[0] = new KeyValue(
                propertyName,
                key,
                DataContext.EntityConfigurationContext.GetEntity<T>().FindProperty(propertyName).TypeDefinition
            );
            return compositeKey;
        }

        public virtual DbQueryable<T> IncludeCount()
        {
            return Then(new IncludeCountOperation());
        }

        public DbQueryable<T> SetTracking(bool enabled)
        {
            TrackEntities = enabled;
            return this;
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

        IDbSet IDbSet.SetTracking(bool enabled)
        {
            SetTracking(enabled);
            return this;
        }

        IDbSet IDbSet.IncludeCount()
        {
            return IncludeCount();
        }

        IDbSet IDbSet.ExpandAll()
        {
            return ExpandAll();
        }

        IDbSet IDbSet.ExpandRelationship(string name)
        {
            return ExpandRelationship(name);
        }

        IDbSet IDbSet.ExpandAllSingleRelationships()
        {
            return ExpandAllSingleRelationships();
        }

        IDbSet IDbSet.ExpandAllCollectionCounts()
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
    }
}