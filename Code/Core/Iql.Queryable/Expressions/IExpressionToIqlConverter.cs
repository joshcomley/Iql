using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.Queryable.Expressions
{
    public interface IExpressionToIqlConverter
    {
        ExpressionResult<IqlExpression> Parse<TEntity>(QueryExpression filter)
            where TEntity : class;
    }
}