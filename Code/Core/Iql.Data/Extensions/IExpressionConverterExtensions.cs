using Iql.Conversion;
#if TypeScript
using Iql.Parsing;
#endif
using Iql.Parsing.Expressions;
using Iql.Parsing.Expressions.QueryExpressions;
using Iql.Parsing.Types;

namespace Iql.Data.Extensions
{
    public static class IExpressionConverterExtensions
    {
        public static ExpressionResult<IqlExpression> ConvertQueryExpressionToIql<TEntity>(
            this IExpressionToIqlConverter converter,
            QueryExpression filter,
            ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        ) where TEntity : class
        {
            var whereQueryExpression = filter.TryFlatten<TEntity>() as ExpressionQueryExpressionBase;
            var lambdaExpression = whereQueryExpression.GetExpression();
            return converter.ConvertLambdaExpressionToIql<TEntity>(
                lambdaExpression,
                typeResolver
#if TypeScript
                    , filter.EvaluateContext ?? evaluateContext
#endif
            );
        }
//        public override ExpressionResult<IqlExpression> ConvertQueryExpressionToIql<TEntity>
//        (
//            QueryExpression filter
//#if TypeScript
//            , EvaluateContext evaluateContext = null
//#endif
//        )
//        {
//            ExpressionQueryExpressionBase expression;
//            if (filter.CanFlatten())
//            {
//                expression = filter.Flatten<TEntity>();
//            }
//            else
//            {
//                expression = filter as ExpressionQueryExpressionBase;
//            }
//            //var whereExpression = filter.CanFlatten();

        //            var lambdaExpression = expression.GetExpression();
        //            return ConvertLambdaExpressionToIql<TEntity>(lambdaExpression
        //#if TypeScript
        //                , expression.EvaluateContext ?? filter.EvaluateContext ?? evaluateContext
        //#endif
        //            );
        //        }
    }
}