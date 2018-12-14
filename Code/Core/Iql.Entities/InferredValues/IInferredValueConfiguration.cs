using System.Linq.Expressions;

namespace Iql.Entities.InferredValues
{
    public interface IInferredValueConfiguration
    {
        IPropertyMetadata Property { get; set; }
        string Key { get; set; }
        IqlExpression InferredWithIql { get; set; }
        IqlExpression InferredWithConditionIql { get; set; }
        bool CanOverride { get; set; }
        bool ForNewOnly { get; set; }
        InferredValueMode Mode { get; set; }
        bool HasCondition { get; }
        IInferredValueConfiguration SetInferredWithExpression(LambdaExpression value, bool onlyIfNew = false, InferredValueMode mode = InferredValueMode.Always, bool canOverride = false);
        IInferredValueConfiguration SetConditionallyInferredWithExpression(LambdaExpression expression, LambdaExpression condition);
    }
}