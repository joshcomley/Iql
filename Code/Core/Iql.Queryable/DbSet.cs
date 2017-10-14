using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Parsing;
using Iql.Queryable.Data;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Expressions.QueryExpressions;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public class DbSet<T, TKey> : DbQueryable<T>, IDbSet, IDbSetOperations<T, TKey> where T : class
    {
        public DbSet(EntityConfigurationBuilder configuration, Func<IDataStore> dataStoreGetter,
            EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(
                configuration, dataStoreGetter, evaluateContext, dataContext)
        {
            Configuration = configuration;
            DataContext = dataContext;
            DataStoreGetter = dataStoreGetter;
        }

        public async Task<T> WithKey(TKey key)
        {
            return (await WithKeyWithResponse(key)).Data;
        }

        public async Task<GetSingleResult<T>> WithKeyWithResponse(TKey key)
        {
            CompositeKey compositeKey;
            if (key is CompositeKey)
            {
                compositeKey = key as CompositeKey;
            }
            else
            {
                compositeKey = GetCompositeKeyFromSingularKey(key);
            }
            return await Then(new WithKeyOperation(compositeKey)).SingleOrDefault();
        }

        public new DbSet<T, TKey> Expand<TTarget>(
            Expression<Func<T, TTarget>> target)
            where TTarget : class
        {
            return (DbSet<T, TKey>)base.Expand(target);
        }

        public new DbSet<T, TKey> ExpandQuery<TTarget>(
            ExpandQueryExpression<T, TTarget, TTarget> expression)
            where TTarget : class
        {
            return (DbSet<T, TKey>)base.ExpandQuery(expression);
        }

        public new DbSet<T, TKey> ExpandSingle<TTarget>(
            Expression<Func<T, TTarget>> target,
            Func<DbQueryable<TTarget>, DbQueryable<TTarget>> filter)
            where TTarget : class
        {
            return (DbSet<T, TKey>)base.ExpandSingle(target, filter);
        }

        public new DbSet<T, TKey> ExpandSingleQuery<TTarget>(
            ExpandQueryExpression<T, TTarget, TTarget> expression)
            where TTarget : class
        {
            return (DbSet<T, TKey>)base.ExpandSingleQuery(expression);
        }

        public new DbSet<T, TKey> ExpandCollection<TTarget>(
            Expression<Func<T, IEnumerable<TTarget>>> target,
            Func<DbQueryable<TTarget>, DbQueryable<TTarget>> filter)
            where TTarget : class
        {
            return (DbSet<T, TKey>)base.ExpandCollection(target, filter);
        }

        public new DbSet<T, TKey> ExpandCollectionQuery<TTarget>(
            ExpandQueryExpression<T, IEnumerable<TTarget>, TTarget> expression)
            where TTarget : class
        {
            return (DbSet<T, TKey>)base.ExpandCollectionQuery(expression);
        }

        public new DbSet<T, TKey> IncludeCount()
        {
            return (DbSet<T, TKey>)base.IncludeCount();
        }

        public override DbQueryable<T> New()
        {
            var dbQueryable = new DbSet<T, TKey>(
                Configuration,
                DataStoreGetter,
                EvaluateContext,
                DataContext);
            return dbQueryable;
        }
    }
}