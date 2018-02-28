using System;
using System.Linq.Expressions;
using Iql.Queryable.Expressions.Conversion;

#if TypeScript
using Iql.Parsing;
#endif

namespace Iql.Queryable.Extensions
{
    public static class IIqlToExpressionConverterExtensions
    {
        public static Expression<Func<TEntity, TOut>> ConvertIqlToFunction<TEntity, TOut>(
            this IIqlToExpressionConverter expressionConverter,
            IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext
#endif
            ) where TEntity : class
        {
            return (Expression<Func<TEntity, TOut>>) expressionConverter.ConvertIqlToExpression<TEntity>(expression
#if TypeScript
                , evaluateContext
#endif
            );
        }
    }
}