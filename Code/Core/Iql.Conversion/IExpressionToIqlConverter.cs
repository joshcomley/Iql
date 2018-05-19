using System;
using System.Linq.Expressions;

#if TypeScript
using Iql.Parsing;
#endif

namespace Iql.Conversion
{
    public interface IExpressionToIqlConverter
    {
//        ExpressionResult<IqlExpression> ConvertQueryExpressionToIql<TEntity>(QueryExpression filter
//#if TypeScript
//            , EvaluateContext evaluateContext = null
//#endif
//        )
//            where TEntity : class;
        ExpressionResult<IqlExpression> ConvertLambdaExpressionToIql<TEntity>(LambdaExpression filter
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
            where TEntity : class;
        ExpressionResult<IqlExpression> ConvertLambdaExpressionToIqlByType(LambdaExpression filter,
            Type entityType
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        ExpressionResult<IqlExpression> ConvertLambdaToIql<TEntity>(Expression<Func<TEntity, object>> filter
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
            where TEntity : class;
        ExpressionResult<IqlPropertyExpression> ConvertPropertyLambdaToIql<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> filter
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
            where TEntity : class;
        ExpressionResult<IqlPropertyExpression> ConvertPropertyLambdaExpressionToIql<TEntity>(LambdaExpression filter
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
            where TEntity : class;
    }
}