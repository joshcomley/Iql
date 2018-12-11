using System.Linq.Expressions;

namespace Iql.Entities.InferredValues
{
    public interface IInferredValueConfiguration
    {
        IPropertyMetadata Property { get; set; }
        string Key { get; set; }
        IqlExpression InferredWithIql { get; set; }
        IqlExpression InferredWithConditionIql { get; set; }
        bool InferredWithForNewOnly { get; set; }
        bool InferredWithForNullOnly { get; set; }
        bool HasCondition { get; }
        IInferredValueConfiguration SetInferredWithExpression(LambdaExpression value, bool onlyIfNew = false, bool onlyIfNull = false);
        IInferredValueConfiguration SetConditionallyInferredWithExpression(LambdaExpression expression, LambdaExpression condition);
    }
}