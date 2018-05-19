#if TypeScript
using Iql.Parsing;    
#endif
using System;
using System.Linq.Expressions;

namespace Iql.Queryable.Expressions.Conversion
{
    public interface IIqlToExpressionConverter
    {
        LambdaExpression ConvertIqlToExpression<TEntity>(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
            where TEntity : class;

        string ConvertIqlToExpressionStringByType(IqlExpression expression,
            Type rootEntityType
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        string ConvertIqlToExpressionString<TEntity>(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
    }
}