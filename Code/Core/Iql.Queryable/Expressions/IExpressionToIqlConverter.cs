using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.Queryable.Expressions
{
    public interface IExpressionToIqlConverter
    {
        ExpressionResult<IqlExpression> ConvertExpressionToIql<TEntity>(QueryExpression filter)
            where TEntity : class;
    }
}