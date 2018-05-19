using System;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Extensions;
#if TypeScript
using Iql.Parsing;
#endif

namespace Iql.Queryable.Expressions.Conversion
{
    public abstract class ExpressionConverterBase : IExpressionConverter
    {
//        public abstract ExpressionResult<IqlExpression> ConvertQueryExpressionToIql<TEntity>(QueryExpression filter
//#if TypeScript
//            , EvaluateContext evaluateContext
//#endif
//        ) where TEntity : class;

        public abstract ExpressionResult<IqlExpression> ConvertLambdaExpressionToIql<TEntity>(LambdaExpression filter
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        ) where TEntity : class;

        static ExpressionConverterBase()
        {
            ConvertLambdaExpressionToIqlByTypeMethod = typeof(ExpressionConverterBase)
                .GetMethod(nameof(ConvertLambdaExpressionToIqlByType));
        }

        public static MethodInfo ConvertLambdaExpressionToIqlByTypeMethod { get; set; }

        public virtual ExpressionResult<IqlExpression> ConvertLambdaExpressionToIqlByType(
            LambdaExpression filter, 
            Type entityType
#if TypeScript
            , EvaluateContext evaluateContext
#endif
            )
        {
            return (ExpressionResult<IqlExpression>) ConvertLambdaExpressionToIqlByTypeMethod
                .InvokeGeneric(this, new object[]
                {
                    filter
#if TypeScript
                    , evaluateContext
#endif
                }, entityType);
        }

        public ExpressionResult<IqlExpression> ConvertLambdaToIql<TEntity>(Expression<Func<TEntity, object>> lambdaExpression
#if TypeScript
, EvaluateContext evaluateContext
#endif
        ) where TEntity : class
        {
            return ConvertLambdaExpressionToIql<TEntity>(lambdaExpression
#if TypeScript
                , evaluateContext
#endif
            );
        }

        public virtual ExpressionResult<IqlPropertyExpression> ConvertPropertyLambdaToIql<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> filter
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        ) where TEntity : class
        {
            var lambdaExpression = filter as LambdaExpression;
            return ConvertPropertyLambdaExpressionToIql<TEntity>(lambdaExpression
#if TypeScript
                , evaluateContext
#endif
            );
        }

        public virtual ExpressionResult<IqlPropertyExpression> ConvertPropertyLambdaExpressionToIql<TEntity>(LambdaExpression filter
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
            where TEntity : class
        {
            var exp = ConvertLambdaExpressionToIql<TEntity>(filter
#if TypeScript
                    , evaluateContext
#endif
            ).Expression;
            var lambda = exp as IqlLambdaExpression;
            return new ExpressionResult<IqlPropertyExpression>(
                lambda.Body as IqlPropertyExpression);
        }

        public abstract LambdaExpression ConvertIqlToExpression<TEntity>(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        ) where TEntity : class;

        public string ConvertIqlToExpressionString<TEntity>(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return ConvertIqlToExpressionStringByType(expression, typeof(TEntity)
#if TypeScript
            , evaluateContext
#endif
            );
        }

        public abstract string ConvertIqlToExpressionStringByType(IqlExpression expression, Type rootEntityType
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
    }
}