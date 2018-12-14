using System.Collections.Generic;

namespace Iql.Parsing.Evaluation
{
    public class IqlObjectEvaluationResult : IqlEvaluationResult<object>
    {
        public IqlObjectEvaluationResult(bool success, object result) : base(success, result)
        {
        }
    }
}