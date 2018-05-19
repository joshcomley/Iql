using System;
using System.Linq.Expressions;
using Iql.Conversion;
using Iql.DotNet.DotNetExpressionToIql;
using Iql.DotNet.IqlToDotNetExpression;
using Iql.DotNet.IqlToDotNetString;
#if TypeScript
using Iql.Parsing;
#endif

namespace Iql.DotNet
{
    public class DotNetExpressionConverter : ExpressionConverterBase
    {
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
            var dotNetExpression = parser.Parse(iql
#if TypeScript
                , evaluateContext
#endif
            );
            return dotNetExpression.Expression as LambdaExpression;
        }

        public override string ConvertIqlToExpressionStringByType(IqlExpression iql, Type rootEntityType
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
        {
            var adapter = new DotNetStringIqlExpressionAdapter("entity");
            var parser = new DotNetStringIqlParserInstance(adapter, this);
            var dotNetExpression = parser.Parse(iql
#if TypeScript
                , evaluateContext
#endif
            );
            return dotNetExpression.ToCodeString();
        }
    }
}