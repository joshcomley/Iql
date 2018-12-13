using Iql.Parsing.Evaluation;

namespace Iql.Data.IqlToIql
{
    public class IqlExpessionResult : IqlEvaluationResult<IqlExpression>
    {
        public IqlExpessionResult(bool success, IqlExpression result) : base(success, result)
        {
        }
    }
}