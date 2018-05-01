using System;
using System.Linq.Expressions;
using Iql.OData.IqlToODataExpression;
#if TypeScript
using Iql.Parsing;
#endif
using Iql.Queryable.Expressions;
using Iql.Queryable.Expressions.Conversion;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.OData
{
    public class ODataExpressionConverter : ExpressionConverterBase
    {
        public ODataConfiguration Configuration { get; }

        public ODataExpressionConverter(ODataConfiguration configuration = null)
        {
            Configuration = configuration;
        }

        public override ExpressionResult<IqlExpression> ConvertQueryExpressionToIql<TEntity>(QueryExpression filter
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            throw new NotImplementedException();
        }

        public override ExpressionResult<IqlExpression> ConvertLambdaExpressionToIql<TEntity>(LambdaExpression filter
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
        {
            throw new NotImplementedException();
        }

        public override LambdaExpression ConvertIqlToExpression<TEntity>(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            throw new NotImplementedException();
        }

        public override string ConvertIqlToExpressionStringByType(IqlExpression expression, Type rootEnityType
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var adapter = new ODataIqlExpressionAdapter();
            var parser = new ODataIqlParserInstance(adapter, rootEnityType, this);
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