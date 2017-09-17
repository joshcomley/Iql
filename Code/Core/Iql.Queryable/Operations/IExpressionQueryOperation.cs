using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.Queryable.Operations
{
    public interface IExpressionQueryOperation : IQueryOperation
    {
        IqlExpression Expression { get; set; }
        QueryExpression GetExpression();
    }
}