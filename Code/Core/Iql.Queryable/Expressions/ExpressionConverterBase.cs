using System;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Extensions;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.EntityConfiguration;
#if TypeScript
using Iql.Parsing;
#endif
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.Queryable.Expressions.Conversion
{
    public abstract class ExpressionConverterBase : IExpressionConverter
    {
        public abstract ExpressionResult<IqlExpression> ConvertQueryExpressionToIql<TEntity>(QueryExpression filter) where TEntity : class;
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

        public IEntityConfigurationBuilder ResolvEntityConfigurationBuilder(Type type)
        {
            return EntityConfigurationBuilder.FindConfigurationBuilderForEntityType(type);
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
            return new ExpressionResult<IqlPropertyExpression>(
                ConvertLambdaExpressionToIql<TEntity>(filter
#if TypeScript
                    , evaluateContext
#endif
                ).Expression as IqlPropertyExpression);
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