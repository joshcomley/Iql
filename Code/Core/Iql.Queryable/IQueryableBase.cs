using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Parsing;
using Iql.Parsing.Expressions.QueryExpressions;

namespace Iql.Queryable
{
    public interface IQueryableBase
    {
        bool? TrackEntities { get; set; }
        bool HasDefaults { get; set; }
        EvaluateContext EvaluateContext { get; }
        Type ItemType { get; }
        List<IQueryOperation> Operations { get; }
        Task<IEnumerable> ToListAsync();
        IQueryableBase Copy();
        IQueryableBase New();
        IQueryableBase Skip(int skip);
        IQueryableBase Take(int take);
        IQueryableBase Reverse();
        IQueryableBase Then(IQueryOperation operation);

        IQueryableBase WherePropertyEquals(string propertyName, object value
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        IQueryableBase WhereEquals(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        IQueryableBase WhereQuery(QueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        IQueryableBase OrderByProperty(string propetyName, bool descending = false
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        IQueryableBase OrderByDefault(bool descending = false);

//        IQueryableBase ExpandProperty(string propetyName
        //#if TypeScript
        //            , EvaluateContext evaluateContext = null
        //#endif
        //        );

        IqlPropertyExpression PropertyExpression(string propertyName);

        Task<IqlDataSetQueryExpression> ToIqlAsync(IExpressionToIqlConverter expressionConverter = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
    }
}