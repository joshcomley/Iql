namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlParentValueReducerBase<TIqlExpression> : IqlReducerBase<TIqlExpression>
        where TIqlExpression : IqlParentValueExpression
    {
        public override void Traverse(TIqlExpression expression, IqlTraverser reducer)
        {
            reducer.Traverse(expression.Value);
            base.Traverse(expression, reducer);
        }

        public override IqlExpression ReduceStaticContent(TIqlExpression expression, IqlReducer reducer)
        {
            expression.Parent = reducer.ReduceStaticContent(expression.Parent);
            expression.Value = reducer.ReduceStaticContent(expression.Value);
            return expression;
        }
    }
}