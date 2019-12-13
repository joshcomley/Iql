using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Functions;
using Iql.Entities.Relationships;
using Iql.Parsing;
using Iql.Queryable.Expressions;

namespace Iql.Data.Lists
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

        //Task IDbSetOperations<T, TKey>.LoadRelationshipPropertyAsync(T entity, IProperty relationship,
        //    Func<IDbSetOperations<T, TKey>, IDbSetOperations<T, TKey>> queryFilter = null)
        //{
        //    return LoadRelationshipPropertyAsync(entity, relationship, q =>
        //    {
        //        if (queryFilter == null)
        //        {
        //            return q;
        //        }
        //        return (DbSet<T, TKey>) queryFilter((DbSet<T, TKey>)q);
        //    });
        //}

        //Task IDbSetOperations<T, TKey>.LoadRelationshipAsync(T entity, Expression<Func<T, object>> relationship, Func<IDbSetOperations<T, TKey>, IDbSetOperations<T, TKey>> queryFilter = null)
        //{
        //    return LoadRelationshipAsync(entity, relationship, q =>
        //    {
        //        if (queryFilter == null)
        //        {
        //            return q;
        //        }
        //        return (DbSet<T, TKey>)queryFilter((DbSet<T, TKey>)q);
        //    });
        //}

        public DbSet<T, TKey> WithKey(TKey key)
        {
            return (DbSet<T, TKey>)WithCompositeKey(CompositeKey.Ensure(key, EntityConfiguration));
        }

        public DbSet<T, TKey> WithKeyOrEntity(object keyOrEntity)
        {
            return (DbSet<T, TKey>)WithCompositeKey(CompositeKey.Ensure(keyOrEntity, EntityConfiguration));
        }

        public async Task<T> GetWithKeyAsync(TKey key)
        {
            return (await GetWithKeyWithResponseAsync(key)).Data;
        }

        public async Task<IEntityState<T>> GetStateWithKeyAsync(TKey key)
        {
            return (await GetWithKeyWithResponseAsync(key)).EntityState;
        }

        public async Task<T> GetWithKeyOrEntityAsync(object keyOrEntity)
        {
            return (await GetWithKeyOrEntityWithResponseAsync(keyOrEntity)).Data;
        }

        public async Task<DbList<T>> GetWithKeysAsync(IEnumerable<TKey> keys)
        {
            return (await GetWithKeysWithResponseAsync(keys)).Data;
        }

        public async Task<GetSingleResult<T>> GetWithKeyWithResponseAsync(TKey key)
        {
            return await WithKey(key).SingleOrDefaultWithResponseAsync();
        }

        public async Task<GetSingleResult<T>> GetWithKeyOrEntityWithResponseAsync(object keyOrEntity)
        {
            return await WithKeyOrEntity(keyOrEntity).SingleOrDefaultWithResponseAsync();
        }

        public async Task<GetDataResult<T>> GetWithKeysWithResponseAsync(IEnumerable<TKey> keys)
        {
            return await WithKeys(keys).ToListWithResponseAsync();
        }

        public DbSet<T, TKey> WithKeys(IEnumerable<TKey> ids)
        {
            return (DbSet<T, TKey>)base.WithKeys(ids.Select(c => (object)c));
        }

        public new DbSet<T, TKey> WithCompositeKeys(IEnumerable<CompositeKey> ids)
        {
            return (DbSet<T, TKey>)base.WithKeys(ids.Select(c => (object)c));
        }

        public new DbSet<T, TKey> OrderByDefault(bool? descending = null, IqlDefaultOrderKind? orderKind = null)
        {
            return (DbSet<T, TKey>)base.OrderByDefault(descending, orderKind);
        }

        public new DbSet<T, TKey> Search(string search, IqlSearchKind searchKind = IqlSearchKind.Primary, bool? splitIntoTerms = null, IEnumerable<IqlPropertyPath> excludeProperties = null)
        {
            return (DbSet<T, TKey>)base.Search(search, searchKind, splitIntoTerms, excludeProperties);
        }

        public new DbSet<T, TKey> SearchProperties(string search, IEnumerable<IqlPropertyPath> properties)
        {
            return (DbSet<T, TKey>)base.SearchProperties(search, properties);
        }

        public new DbSet<T, TKey> ExpandAllCollectionCounts()
        {
            return (DbSet<T, TKey>)base.ExpandAllCollectionRelationships();
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

        public new DbSet<T, TKey> ClearExpands()
        {
            return (DbSet<T, TKey>)base.ClearExpands();
        }

        public new DbSet<T, TKey> ExpandAll()
        {
            return (DbSet<T, TKey>)base.ExpandAll();
        }

        public new DbSet<T, TKey> ExpandQuery(
            ExpandQueryExpression expression, 
            bool countOnly = false)
        {
            return (DbSet<T, TKey>)base.ExpandQuery(expression, countOnly);
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