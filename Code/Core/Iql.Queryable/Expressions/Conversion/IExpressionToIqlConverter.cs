using System;
using System.Linq.Expressions;
#if TypeScript
using Iql.Parsing;
#endif
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.Queryable.Expressions.Conversion
{
    public interface IExpressionToIqlConverter
    {
        ExpressionResult<IqlExpression> ConvertQueryExpressionToIql<TEntity>(QueryExpression filter)
            where TEntity : class;
        ExpressionResult<IqlExpression> ConvertLambdaExpressionToIql<TEntity>(LambdaExpression filter
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
            where TEntity : class;
        ExpressionResult<IqlExpression> ConvertLambdaExpressionToIqlByType(LambdaExpression filter,
            Type entityType
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        );
        ExpressionResult<IqlExpression> ConvertLambdaToIql<TEntity>(Expression<Func<TEntity, object>> filter
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
            where TEntity : class;
    }
}