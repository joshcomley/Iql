using System;
using System.Linq.Expressions;
using Iql.Conversion;
using Iql.OData.IqlToODataExpression;
using Iql.Parsing.Types;
#if TypeScript
using Iql.Parsing;
#endif

namespace Iql.OData
{
    public class ODataExpressionConverter : ExpressionConverterBase
    {
        public ODataConfiguration Configuration { get; }

        public ODataExpressionConverter(ODataConfiguration configuration = null)
        {
            Configuration = configuration;
        }

        protected override ExpressionResult<IqlExpression> ConvertLambdaExpressionToIqlInternal<TEntity>(LambdaExpression lambda
            , ITypeResolver typeResolver
#if TypeScript
                , EvaluateContext evaluateContext = null
#endif
        )
        {
            throw new NotImplementedException();
        }

        public override ExpressionResult<IqlExpression> ConvertLambdaExpressionToIql<TEntity>(LambdaExpression lambda
            , ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            throw new NotImplementedException();
        }

        public override LambdaExpression ConvertIqlToExpression<TEntity>(IqlExpression expression
            , ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            throw new NotImplementedException();
        }

        public override LambdaExpression ConvertIqlToLambdaExpression(IqlExpression expression
            , ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
        {
            throw new NotImplementedException();
        }

        public override string ConvertIqlToExpressionStringByType(IqlExpression expression
            , ITypeResolver typeResolver
            , Type rootEnityType
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var adapter = new ODataIqlExpressionAdapter();
            var parser = new ODataIqlParserContext(adapter, rootEnityType, this);
            var result = parser.Parse(
                expression
#if TypeScript
                , evaluateContext
#endif
            );
            return result.ToCodeString();
        }
    }
}