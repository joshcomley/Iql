namespace Iql.Parsing.Reduction
{
    public interface IIqlReducerBase
    {
        IIqlLiteralExpression Evaluate(IqlExpression expression, IqlReducer reducer);
        IqlExpression ReduceStaticContent(IqlExpression expression, IqlReducer reducer);
    }
}