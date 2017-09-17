namespace Iql.Parsing.Reduction
{
    public interface IIqlReducerBase
    {
        IqlLiteralExpression Evaluate(IqlExpression expression, IqlReducer reducer);
        IqlExpression ReduceStaticContent(IqlExpression expression, IqlReducer reducer);
    }
}