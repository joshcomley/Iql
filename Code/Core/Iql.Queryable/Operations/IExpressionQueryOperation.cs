using Iql.Queryable.Expressions;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.Queryable.Operations
{
    public interface IExpressionQueryOperation : IQueryOperation
    {
        QueryExpression QueryExpession { get; set; }
        IqlExpression Expression { get; set; }
    }
}