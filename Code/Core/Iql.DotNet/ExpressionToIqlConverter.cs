using Iql.Queryable.Expressions;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.DotNet
{
    public class ExpressionToIqlConverter : IExpressionToIqlConverter
    {
        public ExpressionResult<IqlExpression> Parse<TEntity>(QueryExpression filter) where TEntity : class
        {
            var whereQueryExpression = filter.TryFlatten<TEntity>() as ExpressionQueryExpressionBase;
            var lambdaExpression = whereQueryExpression.GetExpression();
            return new ExpressionResult<IqlExpression>(
                ExpressionToIqlExpressionParser<TEntity>.Parse(
                    lambdaExpression
#if TypeScript
                    , filter.EvaluateContext
#endif
                    )
            );
        }
    }
}