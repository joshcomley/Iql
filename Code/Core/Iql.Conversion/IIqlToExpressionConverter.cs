#if TypeScript
using Iql.Parsing;    
#endif
using System;
using System.Linq.Expressions;

namespace Iql.Conversion
{
    public interface IIqlToExpressionConverter
    {
        LambdaExpression ConvertIqlToExpression<TEntity>(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
            where TEntity : class;

        LambdaExpression ConvertIqlToLambdaExpression(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        string ConvertIqlToExpressionStringByType(IqlExpression expression,
            Type rootEntityType
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        string ConvertIqlToExpressionString(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        string ConvertIqlToExpressionStringAs<TEntity>(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
    }
}