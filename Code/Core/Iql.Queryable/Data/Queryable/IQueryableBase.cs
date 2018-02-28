using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Parsing;
using Iql.Queryable.Expressions.QueryExpressions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.Queryable
{
    public interface IQueryableBase
    {
        bool HasDefaults { get; set; }
        EvaluateContext EvaluateContext { get; }
        Type ItemType { get; }
        List<IQueryOperation> Operations { get; }
        Task<IList> ToList();
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

//        IQueryableBase ExpandProperty(string propetyName
//#if TypeScript
//            , EvaluateContext evaluateContext = null
//#endif
//        );

        IqlPropertyExpression PropertyExpression(string propertyName);
    }
}