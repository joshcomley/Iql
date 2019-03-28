using System;
using System.Linq.Expressions;
using Iql.Parsing.Types;

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
        ExpressionResult<IqlExpression> ConvertLambdaExpressionToIql<TEntity>(LambdaExpression expression
            , ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
            where TEntity : class;
        ExpressionResult<IqlExpression> ConvertLambdaExpressionToIqlByType(LambdaExpression expression
            , ITypeResolver typeResolver
            , Type entityType = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        ExpressionResult<IqlExpression> ConvertLambdaToIql<TEntity>(Expression<Func<TEntity, object>> expression
            , ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
            where TEntity : class;
        ExpressionResult<IqlPropertyExpression> ConvertPropertyLambdaToIql<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> expression
            , ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
            where TEntity : class;
        ExpressionResult<IqlPropertyExpression> ConvertPropertyLambdaExpressionToIql<TEntity>(LambdaExpression expression
            , ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
            where TEntity : class;
    }
}