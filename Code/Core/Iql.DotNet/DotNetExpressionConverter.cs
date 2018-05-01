using System;
using System.Linq.Expressions;
using Iql.DotNet.DotNetExpressionToIql;
using Iql.DotNet.IqlToDotNetExpression;
using Iql.DotNet.IqlToDotNetString;
using Iql.Queryable.Data.Context;
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
        public override ExpressionResult<IqlExpression> ConvertQueryExpressionToIql<TEntity>(QueryExpression filter
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
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
            var adapter = new DotNetIqlExpressionAdapter("entity");
            var parser = new DotNetIqlParserInstance(adapter, typeof(TEntity), this);
            parser.IsFilter = true;
            var dotNetExpression = parser.Parse(iql
#if TypeScript
                , evaluateContext
#endif
            );
            return parser.ConvertToLambda ? dotNetExpression.ToLambda() : dotNetExpression.Expression as LambdaExpression;
        }

        public override string ConvertIqlToExpressionStringByType(IqlExpression iql, Type rootEntityType
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
        {
            var adapter = new DotNetStringIqlExpressionAdapter("entity");
            var parser = new DotNetStringIqlParserInstance(adapter, this);
            parser.IsFilter = true;
            var dotNetExpression = parser.Parse(iql
#if TypeScript
                , evaluateContext
#endif
            );
            return $"{adapter.RootVariableName} => {dotNetExpression.Expression}";
        }
    }
}