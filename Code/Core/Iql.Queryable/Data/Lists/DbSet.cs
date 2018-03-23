using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Parsing;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Data.Queryable;
using Iql.Queryable.Expressions.QueryExpressions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.Lists
{
    public class DbSet<T, TKey> : DbQueryable<T>, IDbSetOperations<T, TKey> where T : class
    {
        public DbSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore> dataStoreGetter,
            EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(
                entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {
            EntityConfigurationBuilder = entityConfigurationBuilder;
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
            return await Then(new WithKeyOperation(compositeKey)).SingleOrDefaultWithResponse();
        }

        public new DbSet<T, TKey> WithKeys(IEnumerable<TKey> ids)
        {
            return (DbSet<T, TKey>)base.WithKeys(ids.Select(c => (object)c));
        }

        public new DbSet<T, TKey> OrderByDefault(bool descending = false)
        {
            return (DbSet<T, TKey>)base.OrderByDefault(descending);
        }

        public new DbSet<T, TKey> Search(string search, PropertySearchKind searchKind = PropertySearchKind.Primary)
        {
            return (DbSet<T, TKey>)base.Search(search, searchKind);
        }

        public new DbSet<T, TKey> SearchProperties(string search, IEnumerable<IProperty> properties)
        {
            return (DbSet<T, TKey>)base.SearchProperties(search, properties);
        }

        public new DbSet<T, TKey> ExpandAllCollectionCounts()
        {
            return (DbSet<T, TKey>)base.ExpandAllCollectionCounts();
        }

        public new DbSet<T, TKey> ExpandAllSingleRelationships()
        {
            return (DbSet<T, TKey>)base.ExpandAllSingleRelationships();
        }

        public new DbSet<T, TKey> Expand<TTarget>(
            Expression<Func<T, TTarget>> target)
            where TTarget : class
        {
            return (DbSet<T, TKey>)base.Expand(target);
        }

        public new DbSet<T, TKey> ExpandAll()
        {
            return (DbSet<T, TKey>)base.ExpandAll();
        }

        public new DbSet<T, TKey> ExpandQuery(
            ExpandQueryExpression expression)
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

        public new DbSet<T, TKey> ExpandCollection<TTarget>(
            Expression<Func<T, IEnumerable<TTarget>>> target,
            Func<DbQueryable<TTarget>, DbQueryable<TTarget>> filter = null)
            where TTarget : class
        {
            return (DbSet<T, TKey>)base.ExpandCollection(target, filter);
        }

        public DbSet<T, TKey> AllCollectionRelationships(
            Func<DbSet<T, TKey>, IRelationship, IRelationshipDetail, DbSet<T, TKey>> action)
        {
            return (DbSet<T, TKey>)base.AllCollectionRelationships(
                (queryable, relationship, detail) => action((DbSet<T, TKey>)queryable, relationship, detail));
        }

        public DbSet<T, TKey> AllSingleRelationships(
            Func<DbSet<T, TKey>, IRelationship, IRelationshipDetail, DbSet<T, TKey>> action)
        {
            return (DbSet<T, TKey>)base.AllSingleRelationships(
                (queryable, relationship, detail) => action((DbSet<T, TKey>)queryable, relationship, detail));
        }

        public DbSet<T, TKey> AllRelationships(Func<DbSet<T, TKey>, IRelationship, IRelationshipDetail, DbQueryable<T>> action)
        {
            return (DbSet<T, TKey>)base.AllRelationships(
                (queryable, relationship, detail) => action((DbSet<T, TKey>)queryable, relationship, detail));
        }

        public new DbSet<T, TKey> IncludeCount()
        {
            return (DbSet<T, TKey>)base.IncludeCount();
        }

        public new DbSet<T, TKey> SetTracking(bool enabled)
        {
            return (DbSet<T, TKey>)base.SetTracking(enabled);
        }

        public override DbQueryable<T> New()
        {
            var dbQueryable = new DbSet<T, TKey>(
                EntityConfigurationBuilder,
                DataStoreGetter,
                EvaluateContext,
                DataContext);
            return dbQueryable;
        }
    }
}