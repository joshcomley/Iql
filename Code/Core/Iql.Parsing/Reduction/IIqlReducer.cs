namespace Iql.Parsing.Reduction
{
    public interface IIqlReducer<in TIqlExpression> : IIqlReducerBase
        where TIqlExpression : IqlExpression
    {
        IqlLiteralExpression Evaluate(TIqlExpression expression, IqlReducer reducer);
        IqlExpression ReduceStaticContent(TIqlExpression expression, IqlReducer reducer);
    }
}