namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlParentValueExpressionReducer : IqlReducerBase<IqlParentValueExpression>
    {
        public override IqlExpression ReduceStaticContent(IqlParentValueExpression expression, IqlReducer reducer)
        {
            expression.Parent = (IqlExpression)reducer.ReduceStaticContent(expression.Parent);
            expression.Value = reducer.ReduceStaticContent(expression.Value);
            return expression;
        }
    }
}