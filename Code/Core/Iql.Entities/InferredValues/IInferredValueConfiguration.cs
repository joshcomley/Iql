using System.Linq.Expressions;

namespace Iql.Entities.InferredValues
{
    public interface IInferredValueConfiguration
    {
        string[] OnPropertyChanges { get; set; }
        IPropertyMetadata Property { get; set; }
        string Key { get; set; }
        IqlExpression InferredWithIql { get; set; }
        IqlExpression InferredWithConditionIql { get; set; }
        bool CanOverride { get; set; }
        bool ForNewOnly { get; set; }
        InferredValueKind Kind { get; set; }
        bool HasCondition { get; }
        IInferredValueConfiguration SetInferredWithExpression(LambdaExpression value, bool onlyIfNew = false, InferredValueKind kind = InferredValueKind.Always, bool canOverride = false, params string[] onlyWhenPropertyChanges);
        IInferredValueConfiguration SetConditionallyInferredWithExpression(LambdaExpression expression, LambdaExpression condition);
    }
}