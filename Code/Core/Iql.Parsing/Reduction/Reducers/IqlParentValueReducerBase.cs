namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlParentValueReducerBase<TIqlExpression> : IqlReducerBase<TIqlExpression>
        where TIqlExpression : IqlParentValueExpression
    {
        public override IqlExpression ReduceStaticContent(TIqlExpression expression, IqlReducer reducer)
        {
            expression.Parent = (IqlExpression)reducer.ReduceStaticContent(expression.Parent);
            expression.Value = reducer.ReduceStaticContent(expression.Value);
            return expression;
        }
    }
}