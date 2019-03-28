using System;
using System.Linq.Expressions;
using Iql.Conversion;
using Iql.Parsing.Types;

#if TypeScript
using Iql.Parsing;
#endif

namespace Iql.Queryable.Extensions
{
    public static class IIqlToExpressionConverterExtensions
    {
        public static Expression<Func<TEntity, TOut>> ConvertIqlToFunction<TEntity, TOut>(
            this IIqlToExpressionConverter expressionConverter,
            IqlExpression expression,
            ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext
#endif
            ) where TEntity : class
        {
            return (Expression<Func<TEntity, TOut>>) expressionConverter.ConvertIqlToExpression<TEntity>(expression,
                typeResolver
#if TypeScript
                , evaluateContext
#endif
            );
        }
    }
}