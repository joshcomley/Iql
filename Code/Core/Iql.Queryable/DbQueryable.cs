using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Parsing;
using Iql.Queryable.Data;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Expressions.QueryExpressions;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public class DbQueryable<T> : Queryable<T, DbQueryable<T>> where T : class
    {
        public DbQueryable(EntityConfigurationBuilder configuration, Func<IDataStore> dataStoreGetter,
            EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(evaluateContext)
        {
            Configuration = configuration;
            DataContext = dataContext;
            DataStoreGetter = dataStoreGetter;
        }

        public Func<IDataStore> DataStoreGetter { get; set; }
        public IDataContext DataContext { get; set; }
        public EntityConfigurationBuilder Configuration { get; set; }

        public async Task<GetSingleResult<T>> Single(Expression<Func<T, bool>> expression = null,
            EvaluateContext evaluateContext = null)
        {
            var result = await UseWhereIfExists(expression
#if TypeScript
                                , evaluateContext
#endif
                )
                .ToListWithResponse();
            return ResolveSingle(result);
        }

        public async Task<GetSingleResult<T>> SingleQuery(WhereQueryExpression<T> expression,
            EvaluateContext evaluateContext = null)
        {
            var result = await UseWhereQueryIfExists(expression
#if TypeScript
                                , evaluateContext
#endif
                )
                .ToListWithResponse();
            return ResolveSingle(result);
        }

        public async Task<GetSingleResult<T>> SingleOrDefault(Expression<Func<T, bool>> expression = null,
            EvaluateContext evaluateContext = null)
        {
            var result = await UseWhereIfExists(expression
#if TypeScript
                                , evaluateContext
#endif
                )
                .ToListWithResponse();
            return ResolveSingleOrDefault(result);
        }

        public async Task<GetSingleResult<T>> SingleOrDefaultQuery(WhereQueryExpression<T> expression,
            EvaluateContext evaluateContext = null)
        {
            var result = await UseWhereQueryIfExists(expression
#if TypeScript
                                , evaluateContext
#endif
                )
                .ToListWithResponse();
            return ResolveSingleOrDefault(result);
        }

        public async Task<GetSingleResult<T>> First(Expression<Func<T, bool>> expression = null,
            EvaluateContext evaluateContext = null)
        {
            var result = await UseWhereIfExists(expression
#if TypeScript
                                , evaluateContext
#endif
                )
                .ToListWithResponse();
            return ResolveFirst(result);
        }

        public async Task<GetSingleResult<T>> FirstQuery(WhereQueryExpression<T> expression,
            EvaluateContext evaluateContext = null)
        {
            var result = await UseWhereQueryIfExists(expression
#if TypeScript
                                , evaluateContext
#endif
                )
                .ToListWithResponse();
            return ResolveFirst(result);
        }

        public async Task<GetSingleResult<T>> FirstOrDefault(Expression<Func<T, bool>> expression = null,
            EvaluateContext evaluateContext = null)
        {
            var result = await UseWhereIfExists(expression
#if TypeScript
                                , evaluateContext
#endif
                )
                .ToListWithResponse();
            return ResolveFirstOrDefault(result);
        }

        public async Task<GetSingleResult<T>> FirstOrDefaultQuery(WhereQueryExpression<T> expression,
            EvaluateContext evaluateContext = null)
        {
            var result = await UseWhereQueryIfExists(expression
#if TypeScript
                                , evaluateContext
#endif
                )
                .ToListWithResponse();
            return ResolveFirstOrDefault(result);
        }

        public async Task<List<T>> ToList()
        {
            var result = await ToListWithResponse();
            return result?.Data;
        }

        public async Task<GetDataResult<T>> ToListWithResponse()
        {
            return await DataContext.DataStore.Get(new GetDataOperation<T>(this, DataContext));
        }

        public AddEntityResult<T> Add(T entity)
        {
            return DataContext.DataStore.Add(
                new AddEntityOperation<T>(entity, DataContext));
        }

        public UpdateEntityResult<T> Update(T entity)
        {
            return DataContext.DataStore.Update(
                new UpdateEntityOperation<T>(entity, DataContext));
        }

        public DeleteEntityResult<T> Delete(T entity)
        {
            var entityConfiguration = DataContext.EntityConfigurationContext.GetEntity<T>();
            foreach (var configuration in DataContext.EntityConfigurationContext.AllConfigurations())
            {
                foreach (var relationship in configuration.Relationships)
                {
                    var isSource = relationship.Source.Configuration == entityConfiguration;
                    if (isSource ||
                        relationship.Target.Configuration == entityConfiguration)
                    {
                        var target = isSource
                            ? relationship.Target
                            : relationship.Source;
                        foreach (var relatedEntity in DataContext.DataStore.GetTracking().TrackingSet(target.Type)
                            .TrackedEntites())
                        {
                            var relatedItem = relatedEntity.GetPropertyValue(target.Property.PropertyName);
                            if (relatedItem.IsArray())
                            {
                                var enumerable2 = relatedItem as IEnumerable;
                                var enumerable = relatedItem as IList;
                                if (enumerable != null)
                                {
                                    var toRemove = new List<object>();
                                    foreach (var relatedArrayItem in enumerable)
                                    {
                                        if (DataContext.IsIdMatch(entity, relatedArrayItem))
                                        {
                                            toRemove.Add(relatedArrayItem);
                                        }
                                    }
                                    foreach (var toRemoveItem in toRemove)
                                    {
                                        enumerable.Remove(toRemoveItem);
                                    }
                                }
                            }
                            else
                            {
                                if (DataContext.IsIdMatch(entity, relatedItem))
                                {
                                    relatedEntity.SetPropertyValue(target.Property.PropertyName, null);
                                    foreach (var constraint in relationship.Constraints)
                                    {
                                        var property = isSource
                                            ? constraint.SourceKeyProperty
                                            : constraint.TargetKeyProperty;
                                        relatedEntity.SetPropertyValue(property.PropertyName, null);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return DataContext.DataStore.Delete(
                new DeleteEntityOperation<T>(entity, DataContext));
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
                throw new Exception("No entities returned for \"first\" call");
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

        public DbQueryable<T> Expand<TTarget>(
            Expression<Func<T, TTarget>> target)
            where TTarget : class
        {
            return ExpandQuery(new ExpandQueryExpression<T, TTarget, TTarget>(target));
        }

        public DbQueryable<T> ExpandQuery<TTarget>(
            ExpandQueryExpression<T, TTarget, TTarget> expression)
            where TTarget : class
        {
            return Then(new ExpandOperation<T, TTarget, TTarget>(expression));
        }

        public DbQueryable<T> ExpandSingle<TTarget>(
            Expression<Func<T, TTarget>> target,
            Func<DbQueryable<TTarget>, DbQueryable<TTarget>> filter)
            where TTarget : class
        {
            return ExpandSingleQuery(
                new ExpandQueryExpression<T, TTarget, TTarget>(
                    target,
                    q => filter((DbQueryable<TTarget>)q)
                    ));
        }

        public DbQueryable<T> ExpandSingleQuery<TTarget>(
            ExpandQueryExpression<T, TTarget, TTarget> expression)
            where TTarget : class
        {
            return Then(new ExpandOperation<T, TTarget, TTarget>(expression));
        }

        public DbQueryable<T> ExpandCollection<TTarget>(
            Expression<Func<T, IEnumerable<TTarget>>> target,
            Func<DbQueryable<TTarget>, DbQueryable<TTarget>> filter)
            where TTarget : class
        {
            return ExpandCollectionQuery(
                new ExpandQueryExpression<T, IEnumerable<TTarget>, TTarget>(
                    target,
                    q => filter((DbQueryable<TTarget>)q)
                    ));
        }

        public DbQueryable<T> ExpandCollectionQuery<TTarget>(
            ExpandQueryExpression<T, IEnumerable<TTarget>, TTarget> expression)
            where TTarget : class
        {
            return Then(new ExpandOperation<T, IEnumerable<TTarget>, TTarget>(expression));
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
            var compositeKey = new CompositeKey();
            var propertyName = DataContext.EntityConfigurationContext.GetEntity<T>().Key.Properties.First().PropertyName;
            compositeKey.Keys.Add(new KeyValue(
                propertyName,
                key
            ));
            return compositeKey;
        }

        protected override DbQueryable<T> New()
        {
            var dbQueryable = new DbQueryable<T>(
                Configuration,
                DataStoreGetter,
                EvaluateContext,
                DataContext);
            return dbQueryable;
        }
    }
}