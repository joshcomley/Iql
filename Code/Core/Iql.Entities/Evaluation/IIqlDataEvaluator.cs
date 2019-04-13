using System;
using System.Collections;
using System.Threading.Tasks;
using Iql.Entities;
#if TypeScript
using Iql.Parsing;
#endif
using Iql.Parsing.Types;

namespace Iql.Data.Evaluation
{
    public interface IIqlDataEvaluator
    {
        Task<bool> QueryAnyAsync(IqlDataSetQueryExpression query, ITypeResolver typeResolver = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<bool> QueryAllAsync(IqlDataSetQueryExpression query, ITypeResolver typeResolver = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<long> QueryCountAsync(IqlDataSetQueryExpression query, ITypeResolver typeResolver = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<object> GetEntityByKeyAsync(
            IEntityConfiguration entityConfiguration,
            CompositeKey key,
            string[] expandPaths,
            bool trackResult);

        IqlEntityStatus EntityStatus(object entity, IEntityConfiguration entityConfiguration = null);
        string EntityStateKey(object entity, IEntityConfiguration entityConfiguration = null);
    }
}