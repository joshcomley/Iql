using Iql.Parsing.Expressions.QueryExpressions;

namespace Iql.Queryable.Operations
{
    public interface IExpressionQueryOperation : IQueryOperation
    {
        QueryExpression QueryExpression { get; set; }
        IqlExpression Expression { get; set; }
    }
}