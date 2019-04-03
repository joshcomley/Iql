using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Brandless.Data.EntityFramework.Crud;
using Iql.Conversion;
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
        }

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
            CrudManager = crudManager;
            NewDbContext = newDbContext;
            UnsavedEntities = unsavedEntities;
        }

        private async Task<object> GetEntityByKeyTypedAsync<TEntity>(
            EntityConfiguration<TEntity> entityConfiguration,
            CompositeKey key,
            string[] expandPaths)
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
                var entityQuery = NewDbContext().Set<TEntity>().AsNoTracking().Where(CrudManager.KeyEqualsExpression<TEntity>(dic));
                if (expandPaths != null)
                {
                    foreach (var expand in expandPaths)
                    {
                        entityQuery = entityQuery.Include(expand);
                    }
                }
                return await entityQuery.AsNoTracking().SingleOrDefaultAsync();
            }
            catch
            {

            }

            return null;
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

        public async Task<object> GetEntityByKeyAsync(IEntityConfiguration entityConfiguration, CompositeKey key, string[] expandPaths)
        {
            return await (Task<object>)(GetEntityByKeyTypedAsyncMethod.MakeGenericMethod(entityConfiguration.Type)
                .Invoke(this, new object[] { entityConfiguration, key, expandPaths }));
        }

        public IqlEntityStatus EntityStatus(object entity, IEntityConfiguration entityConfiguration = null)
        {
            return UnsavedEntities != null && UnsavedEntities.Length > 0 && UnsavedEntities.Contains(entity)
                ? IqlEntityStatus.New
                : IqlEntityStatus.Existing;
        }
    }
}