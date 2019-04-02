using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Brandless.Data.EntityFramework.Crud;
using Iql.Data.Evaluation;
using Iql.Entities;
using Iql.Parsing.Types;
using Microsoft.EntityFrameworkCore;

namespace Iql.Server.OData.Net
{
    public class IqlServerEvaluator : IIqlDataEvaluator
    {
        static IqlServerEvaluator()
        {
            GetEntityByKeyTypedAsyncMethod = typeof(IqlServerEvaluator).GetMethod(nameof(GetEntityByKeyTypedAsync),
                BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private static MethodInfo GetEntityByKeyTypedAsyncMethod { get; set; }

        private readonly bool _isEntityNew;
        public CrudManager CrudManager { get; set; }

        public IqlServerEvaluator(CrudManager crudManager, bool isEntityNew)
        {
            _isEntityNew = isEntityNew;
            CrudManager = crudManager;
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
                var entityQuery = CrudManager.FindQuery<TEntity>(dic);
                return await entityQuery.SingleOrDefaultAsync();
            }
            catch
            {

            }

            return null;
        }

        public Task<bool> QueryAnyAsync(IqlDataSetQueryExpression query, ITypeResolver typeResolver = null)
        {
            throw new System.NotImplementedException();
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
            return _isEntityNew ? IqlEntityStatus.New : IqlEntityStatus.Existing;
        }
    }
}