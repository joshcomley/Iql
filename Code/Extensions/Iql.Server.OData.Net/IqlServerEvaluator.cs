using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Brandless.Data.EntityFramework.Crud;
using Iql.Conversion;
using Iql.Data.Context;
using Iql.Data.Evaluation;
using Iql.DotNet;
using Iql.Entities;
using Iql.Parsing.Types;
using Microsoft.EntityFrameworkCore;

namespace Iql.Server.OData.Net
{
    public class IqlServerEvaluator : IIqlDataEvaluator
    {
        static IqlServerEvaluator()
        {
            QueryAnyTypedAsyncMethod = typeof(IqlServerEvaluator).GetMethod(nameof(QueryAnyTypedAsync),
                BindingFlags.Instance | BindingFlags.NonPublic);
            GetEntityByKeyTypedAsyncMethod = typeof(IqlServerEvaluator).GetMethod(nameof(GetEntityByKeyTypedAsync),
                BindingFlags.Instance | BindingFlags.NonPublic);
            IsTrackedTypedMethod = typeof(IqlServerEvaluator).GetMethod(nameof(IsTrackedTyped),
                BindingFlags.Instance | BindingFlags.Public);
        }

        private static MethodInfo IsTrackedTypedMethod { get; set; }
        private static MethodInfo GetEntityByKeyTypedAsyncMethod { get; set; }
        private static MethodInfo QueryAnyTypedAsyncMethod { get; set; }

        public CrudManager CrudManager { get; set; }
        public Func<DbContext> NewDbContext { get; }
        public object[] UnsavedEntities { get; private set; }

        public void MarkAsSaved(params object[] entities)
        {
            UnsavedEntities = UnsavedEntities.Where(_ => !entities.Contains(_)).ToArray();
        }

        public void MarkAsUnsaved(params object[] entities)
        {
            var l = UnsavedEntities.ToList();
            l.AddRange(entities);
            UnsavedEntities = l.Distinct().ToArray();
        }

        public IqlServerEvaluator(CrudManager crudManager, Func<DbContext> newDbContext, params object[] unsavedEntities)
        {
            DataContext = newDbContext();
            CrudManager = crudManager;
            NewDbContext = newDbContext;
            UnsavedEntities = unsavedEntities;
        }

        public DbContext DataContext { get; set; }

        private async Task<object> GetEntityByKeyTypedAsync<TEntity>(
            EntityConfiguration<TEntity> entityConfiguration,
            CompositeKey key,
            string[] expandPaths,
            bool trackResult)
            where TEntity : class
        {
            var dic = new List<KeyValuePair<string, object>>();
            foreach (var constraint in key.Keys)
            {
                dic.Add(new KeyValuePair<string, object>(
                    constraint.Name,
                    constraint.Value));
            }

            try
            {
                IQueryable<TEntity> dbSet = DataContext.Set<TEntity>();
                if (!trackResult)
                {
                    dbSet = NewDbContext().Set<TEntity>().AsNoTracking();
                }

                var keyEqualsExpression = CrudManager.KeyEqualsExpression<TEntity>(dic);
                var entityQuery = dbSet.Where(keyEqualsExpression);
                if (expandPaths != null)
                {
                    foreach (var expand in expandPaths)
                    {
                        entityQuery = entityQuery.Include(expand);
                    }
                }
                return await entityQuery.SingleOrDefaultAsync();
            }
            catch
            {

            }

            return null;
        }

        public bool IsTracked(object entity)
        {
            return (bool) IsTrackedTypedMethod.MakeGenericMethod(entity.GetType()).Invoke(this, new object[] {entity});
        }

        public bool IsTrackedTyped<T>(T entity)
            where T : class
        {
            return DataContext.Set<T>().Local.Any(_ => _ == entity);
        }

        public Task<bool> QueryAnyAsync(IqlDataSetQueryExpression query, ITypeResolver typeResolver = null)
        {
            DotNetExpressionConverter.DisableNullPropagation = true;
            DotNetExpressionConverter.DisableCaseSensitivityHandling = true;
            var lambda = IqlConverter.Instance.ConvertIqlToLambdaExpression(query.Filter, typeResolver);
            var entityType = CrudManager.Context.Model.GetEntityTypes().SingleOrDefault(_ => _.ClrType.Name == query.EntityTypeName);
            return (Task<bool>)(QueryAnyTypedAsyncMethod.MakeGenericMethod(entityType.ClrType)
                .Invoke(this, new object[] { lambda, typeResolver }));
        }

        private Task<bool> QueryAnyTypedAsync<TEntity>(Expression<Func<TEntity, bool>> query, ITypeResolver typeResolver = null)
            where TEntity : class
        {
            var set = NewDbContext().Set<TEntity>();
            return set.AsNoTracking().AnyAsync(query);
        }

        public Task<bool> QueryAllAsync(IqlDataSetQueryExpression query, ITypeResolver typeResolver = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<long> QueryCountAsync(IqlDataSetQueryExpression query, ITypeResolver typeResolver = null)
        {
            throw new System.NotImplementedException();
        }

        public async Task<object> GetEntityByKeyAsync(IEntityConfiguration entityConfiguration, CompositeKey key, string[] expandPaths, bool trackResult)
        {
            var task = GetEntityByKeyTypedAsyncMethod.MakeGenericMethod(entityConfiguration.Type)
                .Invoke(this, new object[] { entityConfiguration, key, expandPaths, trackResult });
            var result = await (Task<object>)task;
            return result;
        }

        public IqlEntityStatus EntityStatus(object entity, IEntityConfiguration entityConfiguration = null)
        {
            return UnsavedEntities != null && UnsavedEntities.Length > 0 && UnsavedEntities.Contains(entity)
                ? IqlEntityStatus.New
                : IqlEntityStatus.Existing;
        }

        public string EntityStateKey(object entity, IEntityConfiguration entityConfiguration = null)
        {
            return Guid.NewGuid().ToString();
        }
    }
}