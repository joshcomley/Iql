using System;
using System.Collections;
using System.Threading.Tasks;
using Iql.Entities;
using Iql.Parsing.Types;

namespace Iql.Data.Evaluation
{
    public interface IIqlDataEvaluator
    {
        Task<bool> QueryAnyAsync(IqlDataSetQueryExpression query, ITypeResolver typeResolver = null);
        Task<bool> QueryAllAsync(IqlDataSetQueryExpression query, ITypeResolver typeResolver = null);
        Task<long> QueryCountAsync(IqlDataSetQueryExpression query, ITypeResolver typeResolver = null);
        Task<object> GetEntityByKeyAsync(
            IEntityConfiguration entityConfiguration,
            CompositeKey key,
            string[] expandPaths);

        IqlEntityStatus EntityStatus(object entity, IEntityConfiguration entityConfiguration = null);
    }
}