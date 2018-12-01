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
        public static void Use()
        {
            IqlExpressionConversion.DefaultExpressionConverter = () => new DotNetExpressionConverter();
        }

        protected override ExpressionResult<IqlExpression> ConvertLambdaExpressionToIqlInternal<TEntity>(LambdaExpression lambda
#if TypeScript
, EvaluateContext evaluateContext
#endif
        )
        {
            return new ExpressionResult<IqlExpression>(DotNetExpressionToIqlExpressionConverter.Parse(lambda));
            //return new ExpressionResult<IqlExpression>(
            //    DotNetExpressionToIqlExpressionParser<TEntity>.Parse(
            //        lambda
            //    )
            //);
        }

        public override LambdaExpression ConvertIqlToExpression<TEntity>(IqlExpression iql
#if TypeScript
                , EvaluateContext evaluateContext
#endif
        )
        {
            return ConvertIql(iql, typeof(TEntity));
        }

        public LambdaExpression ConvertIql(IqlExpression expression, Type type = null)
        {
            var adapter = new DotNetIqlExpressionAdapter("entity");
            var parser = new DotNetIqlParserInstance(adapter, type, this);
            var dotNetExpression = parser.Parse(expression
#if TypeScript
                , null
#endif
            );
            return dotNetExpression.ToLambda();
        }

        public override LambdaExpression ConvertIqlToLambdaExpression(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return ConvertIql(expression);
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