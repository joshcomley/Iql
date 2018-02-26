using System;
using System.Linq.Expressions;

namespace Iql.Queryable.Expressions
{
    public static class IIqlToExpressionConverterExtensions
    {
        public static Expression<Func<TEntity, TOut>> ConvertIqlToFunction<TEntity, TOut>(
            this IIqlToExpressionConverter expressionConverter,
            IqlExpression expression) where TEntity : class
        {
            return (Expression<Func<TEntity, TOut>>) expressionConverter.ConvertIqlToExpression<TEntity>(expression);
        }
    }
}