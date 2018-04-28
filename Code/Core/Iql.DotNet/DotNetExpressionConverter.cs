using System;
using System.Linq.Expressions;
using Iql.DotNet.DotNetExpressionToIql;
using Iql.DotNet.IqlToDotNetExpression;
using Iql.Queryable.Data.EntityConfiguration;
#if TypeScript
using Iql.Parsing;
#endif
using Iql.Queryable.Expressions;
using Iql.Queryable.Expressions.Conversion;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.DotNet
{
    public class DotNetExpressionConverter : ExpressionConverterBase
    {
        public override ExpressionResult<IqlExpression> ConvertQueryExpressionToIql<TEntity>(QueryExpression filter)
        {
            var whereQueryExpression = filter.TryFlatten<TEntity>() as ExpressionQueryExpressionBase;
            var lambdaExpression = whereQueryExpression.GetExpression();
            return ConvertLambdaExpressionToIql<TEntity>(
                    lambdaExpression
#if TypeScript
                    , filter.EvaluateContext
#endif
                    );
        }

        public override ExpressionResult<IqlExpression> ConvertLambdaExpressionToIql<TEntity>(LambdaExpression lambdaExpression
#if TypeScript
, EvaluateContext evaluateContext
#endif
        )
        {
            return new ExpressionResult<IqlExpression>(
                DotNetExpressionToIqlExpressionParser<TEntity>.Parse(
                    lambdaExpression
#if TypeScript
                    , evaluateContext
#endif
                )
            );
        }

        public override LambdaExpression ConvertIqlToExpression<TEntity>(IqlExpression iql
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
        {
            return new IqlToDotNetConverter().ConvertIqlToExpression<TEntity>(iql
#if TypeScript
                , evaluateContext
#endif
            );
        }

        public override string ConvertIqlToExpressionStringByType(IqlExpression iql, Type rootEntityType
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
        {
            return new IqlToDotNetConverter().ConvertIqlToExpressionStringByType(iql, rootEntityType
#if TypeScript
                , evaluateContext
#endif
            );
        }

        public DotNetExpressionConverter(IEntityConfigurationBuilder entityConfigurationBuilder = null) : base(entityConfigurationBuilder)
        {
        }
    }
}