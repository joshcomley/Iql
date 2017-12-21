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
using Iql.Queryable.Data.Crud.State;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Data.Tracking;
using Iql.Queryable.Expressions.QueryExpressions;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public class DbQueryable<T> : Queryable<T, DbQueryable<T>> where T : class
    {
        public bool TrackEntities { get; set; } = true;
        public DbQueryable(EntityConfigurationBuilder configuration, Func<IDataStore> dataStoreGetter,
            EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(evaluateContext)
        {
            Configuration = configuration;
            DataContext = dataContext;
            DataStoreGetter = dataStoreGetter;
            TrackingSetCollection = dataContext.DataStore.GetTracking();
            TrackingSet = TrackingSetCollection.TrackingSet(typeof(T));
        }

        public ITrackingSet TrackingSet { get; set; }

        public Func<IDataStore> DataStoreGetter { get; set; }
        public TrackingSetCollection TrackingSetCollection { get; private set; }
        public IDataContext DataContext { get; set; }
        public EntityConfigurationBuilder Configuration { get; set; }

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

        public async Task<GetSingleResult<T>> SingleQuery(WhereQueryExpression<T> expression
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

        public async Task<GetSingleResult<T>> SingleOrDefaultQuery(WhereQueryExpression<T> expression
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

        public async Task<GetSingleResult<T>> FirstQuery(WhereQueryExpression<T> expression
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

        public async Task<GetSingleResult<T>> FirstOrDefaultQuery(WhereQueryExpression<T> expression
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

        public async Task<GetSingleResult<T>> LastQuery(WhereQueryExpression<T> expression
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


        public async Task<GetSingleResult<T>> LastOrDefaultQuery(WhereQueryExpression<T> expression
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
            if (TrackingSet.IsTracked(entity))
            {
                return null;
            }
            return null;
            return DataContext.DataStore.Add(entity);
        }

        //public UpdateEntityResult<T> Update(T entity)
        //{
        //    return DataContext.DataStore.Update(
        //        new UpdateEntityOperation<T>(entity, DataContext));
        //}

        public EntityState<T> Delete(T entity)
        {
            if (!DataContext.DataStore.GetTracking().IsTracked(entity, typeof(T)))
            {
                return null;
            }
            RelationshipManagerBase.DeleteRelationships(entity, typeof(T), DataContext);
            //var entityConfiguration = DataContext.EntityConfigurationContext.GetEntity<T>();
            //foreach (var configuration in DataContext.EntityConfigurationContext.AllConfigurations())
            //{
            //    foreach (var relationship in configuration.Relationships)
            //    {
            //        var isSource = relationship.Source.Configuration == entityConfiguration;
            //        if (isSource ||
            //            relationship.Target.Configuration == entityConfiguration)
            //        {
            //            var target = isSource
            //                ? relationship.Target
            //                : relationship.Source;
            //            foreach (var relatedEntity in DataContext.DataStore.GetTracking().TrackingSet(target.Type)
            //                .TrackedEntites())
            //            {
            //                var relatedItem = relatedEntity.GetPropertyValue(target.Property.PropertyName);
            //                if (relatedItem.IsArray())
            //                {
            //                    var enumerable = relatedItem as IList;
            //                    if (enumerable != null)
            //                    {
            //                        var toRemove = new List<object>();
            //                        foreach (var relatedArrayItem in enumerable)
            //                        {
            //                            if (DataContext.IsIdMatch(entity, relatedArrayItem, typeof(T)))
            //                            {
            //                                toRemove.Add(relatedArrayItem);
            //                            }
            //                        }
            //                        foreach (var toRemoveItem in toRemove)
            //                        {
            //                            enumerable.Remove(toRemoveItem);
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    if (DataContext.IsIdMatch(entity, relatedItem, typeof(T)))
            //                    {
            //                        relatedEntity.SetPropertyValue(target.Property.PropertyName, null);
            //                        foreach (var constraint in relationship.Constraints)
            //                        {
            //                            var property = isSource
            //                                ? constraint.SourceKeyProperty
            //                                : constraint.TargetKeyProperty;
            //                            relatedEntity.SetPropertyValue(property.PropertyName, null);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

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

        public DbQueryable<T> ExpandCollectionCount<TTarget>(
            Expression<Func<T, IEnumerable<TTarget>>> target)
            where TTarget : class
        {
            return ExpandCollectionCountQuery(new ExpandQueryExpression<T, IEnumerable<TTarget>, TTarget>(target));
        }

        public DbQueryable<T> ExpandCollectionCountRelationship(
            string propertyName)
        {
            return ExpandRelationshipInternal(propertyName, typeof(ExpandCountOperation<,,>));
        }

        public DbQueryable<T> ExpandCollectionCountQuery<TTarget>(
            ExpandQueryExpression<T, IEnumerable<TTarget>, TTarget> expression)
            where TTarget : class
        {
            return Then(new ExpandCountOperation<T, IEnumerable<TTarget>, TTarget>(expression));
        }

        public DbQueryable<T> ExpandAllCollectionCounts()
        {
            return AllCollectionRelationships(
                (queryable, relationship, detail) => queryable.ExpandCollectionCountRelationship(detail.Property.PropertyName));
        }

        public DbQueryable<T> ExpandAllSingleRelationships()
        {
            return AllSingleRelationships(
                (queryable, relationship, detail) => queryable.ExpandRelationship(detail.Property.PropertyName));
        }

        public DbQueryable<T> Expand<TTarget>(
            Expression<Func<T, TTarget>> target)
            where TTarget : class
        {
            return ExpandQuery(new ExpandQueryExpression<T, TTarget, TTarget>(target));
        }

        public DbQueryable<T> ExpandRelationship(
            string propertyName)
        {
            return ExpandRelationshipInternal(propertyName, typeof(ExpandOperation<,,>));
        }

        private DbQueryable<T> ExpandRelationshipInternal(string propertyName, Type type)
        {
            var entityConfiguration = DataContext.EntityConfigurationContext.GetEntityByType(typeof(T));
            var relationship = entityConfiguration
                .Relationships.Single(r =>
                {
                    var thisEnd = r.Source.Configuration == entityConfiguration
                        ? r.Source
                        : r.Target;
                    if (thisEnd.Property.PropertyName == propertyName)
                    {
                        return true;
                    }
                    return false;
                });
            var source = relationship.Source.Configuration == entityConfiguration
                ? relationship.Source
                : relationship.Target;
            var target = relationship.Source.Configuration == entityConfiguration
                ? relationship.Target
                : relationship.Source;
            var property = entityConfiguration.Properties.Single(p => p.Name == source.Property.PropertyName);
            var propertyExpression = PropertyExpression(propertyName);
            var expandOperationType = type.MakeGenericType(
                typeof(T),
                property.Type,
                target.Type);
            var expandOperation =
                (IExpressionQueryOperation)Activator.CreateInstance(expandOperationType, new object[] { null });
            expandOperation.Expression = propertyExpression;
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
                (queryable, relationship, detail) => queryable.ExpandRelationship(detail.Property.PropertyName));
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
            Func<DbQueryable<TTarget>, DbQueryable<TTarget>> filter = null)
            where TTarget : class
        {
            return ExpandCollectionQuery(
                new ExpandQueryExpression<T, IEnumerable<TTarget>, TTarget>(
                    target,
                    q => filter == null ? q : filter((DbQueryable<TTarget>)q)
                    ));
        }

        public DbQueryable<T> ExpandCollectionQuery<TTarget>(
            ExpandQueryExpression<T, IEnumerable<TTarget>, TTarget> expression)
            where TTarget : class
        {
            return Then(new ExpandOperation<T, IEnumerable<TTarget>, TTarget>(expression));
        }

        public DbQueryable<T> AllCollectionRelationships(
            Func<DbQueryable<T>, IRelationship, IRelationshipDetail, DbQueryable<T>> action)
        {
            var entityConfig = Configuration.GetEntityByType(typeof(T));
            return AllRelationships(
                (queryable, relationship, detail) =>
                {
                    if (entityConfig.FindProperty(detail.Property.PropertyName).IsCollection)
                    {
                        return action(queryable, relationship, detail);
                    }
                    return queryable;
                });
        }

        public DbQueryable<T> AllSingleRelationships(
            Func<DbQueryable<T>, IRelationship, IRelationshipDetail, DbQueryable<T>> action)
        {
            var entityConfig = Configuration.GetEntityByType(typeof(T));
            return AllRelationships(
                (queryable, relationship, detail) =>
                {
                    if (!entityConfig.FindProperty(detail.Property.PropertyName).IsCollection)
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
            var compositeKey = new CompositeKey();
            var propertyName = DataContext.EntityConfigurationContext.GetEntity<T>().Key.Properties.First().PropertyName;
            compositeKey.Keys.Add(new KeyValue(
                propertyName,
                key,
                DataContext.EntityConfigurationContext.GetEntity<T>().FindProperty(propertyName).Type
            ));
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
                Configuration,
                DataStoreGetter,
                EvaluateContext,
                DataContext);
            return dbQueryable;
        }
    }
}