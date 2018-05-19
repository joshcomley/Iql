using Iql.Parsing;

namespace Iql.Queryable.Operations
{
    public interface IQueryOperation
    {
#if TypeScript
        EvaluateContext EvaluateContext { get; set; }
#endif
    }
}