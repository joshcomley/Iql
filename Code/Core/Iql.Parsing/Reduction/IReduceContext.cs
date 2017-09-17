namespace Iql.Parsing.Reduction
{
    public interface IReduceContext
    {
        IqlExpression Expression { get; set; }
        IqlReducer Reducer { get; set; }
        EvaluateContext EvaluateContext { get; set; }
    }
}