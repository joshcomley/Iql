using Iql.Parsing;

namespace Iql.Queryable.Operations
{
    public interface IQueryOperation
    {
        EvaluateContext EvaluateContext { get; set; }
    }
}