using Iql.Queryable.Expressions;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.DotNet
{
    public class ExpressionToIqlConverter : IExpressionToIqlConverter
    {
        public ExpressionResult<IqlExpression> Parse<TEntity>(QueryExpression filter) where TEntity : class
        {
            return new ExpressionResult<IqlExpression>(
                ExpressionToIqlExpressionParser<TEntity>.Parse(
                    filter.Flatten<TEntity>().GetExpression(),
                    filter.EvaluateContext)
            );
        }
    }
}