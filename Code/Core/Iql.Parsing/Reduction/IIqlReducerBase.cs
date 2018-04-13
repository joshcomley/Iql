namespace Iql.Parsing.Reduction
{
    public interface IIqlReducerBase
    {
        void Traverse(IqlExpression expression, IqlTraverser reducer);
        IIqlLiteralExpression Evaluate(IqlExpression expression, IqlReducer reducer);
        IqlExpression ReduceStaticContent(IqlExpression expression, IqlReducer reducer);
    }
}