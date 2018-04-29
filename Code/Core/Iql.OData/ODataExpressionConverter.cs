using System;
using System.Linq.Expressions;
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
        public override ExpressionResult<IqlExpression> ConvertQueryExpressionToIql<TEntity>(QueryExpression filter)
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
            , EvaluateContext evaluateContext
#endif
        )
        {
            throw new NotImplementedException();
        }

        public override string ConvertIqlToExpressionStringByType(IqlExpression expression, Type rootEnityType
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
        {
            throw new NotImplementedException();
//            var expressionString = ODataQueryableAdapter.GetExpression(expression,
//                ResolvEntityConfigurationBuilder(rootEnityType),
//                rootEnityType
//#if TypeScript
//                , evaluateContext
//#endif
//                );
//            return expressionString;
        }
    }
}