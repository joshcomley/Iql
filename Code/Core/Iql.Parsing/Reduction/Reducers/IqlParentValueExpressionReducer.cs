namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlParentValueExpressionReducer : IqlReducerBase<IqlParentValueExpression>
    {
        public override void Traverse(IqlParentValueExpression expression, IqlTraverser reducer)
        {
            reducer.Traverse(expression.Value);
            base.Traverse(expression, reducer);
        }

        public override IIqlLiteralExpression Evaluate(IqlParentValueExpression expression, IqlReducer reducer)
        {
            return null;
        }

        public override IqlExpression ReduceStaticContent(IqlParentValueExpression expression, IqlReducer reducer)
        {
            expression.Parent = reducer.ReduceStaticContent(expression.Parent);
            expression.Value = reducer.ReduceStaticContent(expression.Value);
            return expression;
        }
    }
}