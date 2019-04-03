using System;
using System.Linq.Expressions;
using Iql.Conversion;
using Iql.DotNet.DotNetExpressionToIql;
using Iql.DotNet.IqlToDotNetExpression;
using Iql.DotNet.IqlToDotNetString;
using Iql.Parsing.Types;
#if TypeScript
using Iql.Parsing;
#endif

namespace Iql.DotNet
{
    public class DotNetExpressionConverter : ExpressionConverterBase
    {
        public static bool DisableNullPropagation { get; set; }
        public static bool DisableCaseSensitivityHandling { get; set; }
        public static void Use()
        {
            IqlExpressionConversion.DefaultExpressionConverter = () => new DotNetExpressionConverter();
        }

        protected override ExpressionResult<IqlExpression> ConvertLambdaExpressionToIqlInternal<TEntity>(LambdaExpression lambda
            , ITypeResolver typeResolver
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
            , ITypeResolver typeResolver
#if TypeScript
                , EvaluateContext evaluateContext
#endif
        )
        {
            return ConvertIql(iql, typeResolver, typeof(TEntity));
        }

        public LambdaExpression ConvertIql(IqlExpression expression, ITypeResolver typeResolver, Type type = null)
        {
            var adapter = new DotNetIqlExpressionAdapter("entity");
            var parser = new DotNetIqlParserContext(typeResolver, adapter, type, this);
            var dotNetExpression = parser.Parse(expression
#if TypeScript
                , null
#endif
            );
            return dotNetExpression.ToLambda();
        }

        public override LambdaExpression ConvertIqlToLambdaExpression(IqlExpression expression
            , ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return ConvertIql(expression, typeResolver);
        }

        public override string ConvertIqlToExpressionStringByType(IqlExpression iql
            , ITypeResolver typeResolver
            , Type rootEntityType
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
        {
            var adapter = new DotNetStringIqlExpressionAdapter("entity");
            var parser = new DotNetStringIqlParserContext(adapter, this, typeResolver);
            var dotNetExpression = parser.Parse(iql
#if TypeScript
                , evaluateContext
#endif
            );
            return dotNetExpression.ToCodeString();
        }
    }
}