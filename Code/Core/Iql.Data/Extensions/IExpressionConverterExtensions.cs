using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Expressions;
using Iql.Queryable.Expressions.Conversion;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.Data.Extensions
{
    public static class IExpressionConverterExtensions
    {
        public static ExpressionResult<IqlExpression> ConvertQueryExpressionToIql<TEntity>(
            this IExpressionToIqlConverter converter,
            QueryExpression filter
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        ) where TEntity : class
        {
            var whereQueryExpression = filter.TryFlatten<TEntity>() as ExpressionQueryExpressionBase;
            var lambdaExpression = whereQueryExpression.GetExpression();
            return converter.ConvertLambdaExpressionToIql<TEntity>(
                lambdaExpression
#if TypeScript
                    , filter.EvaluateContext
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