namespace Iql.Parsing.Reduction
{
    public interface IIqlReducer<in TIqlExpression> : IIqlReducerBase
        where TIqlExpression : IqlExpression
    {
        void Traverse(TIqlExpression expression, IqlTraverser reducer);
        IIqlLiteralExpression Evaluate(TIqlExpression expression, IqlReducer reducer);
        IqlExpression ReduceStaticContent(TIqlExpression expression, IqlReducer reducer);
    }
}