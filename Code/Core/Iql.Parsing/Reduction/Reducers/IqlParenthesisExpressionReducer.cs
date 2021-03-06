namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlParenthesisExpressionReducer : IqlReducerBase<IqlParenthesisExpression>
    {
        public override void Traverse(IqlParenthesisExpression expression, IqlTraverser reducer)
        {
            reducer.Traverse(expression.Expression);
            base.Traverse(expression, reducer);
        }

        public override IIqlLiteralExpression Evaluate(IqlParenthesisExpression expression, IqlReducer reducer)
        {
            //for (var i = 0; i < expression.Expressions.Count; i++)
            //{
            //    expression.Expressions[i] =
            //        reducer.ReduceStaticContent(expression.Expressions[i]);
            //}
            return null;
        }

        public override IqlExpression ReduceStaticContent(IqlParenthesisExpression expression, IqlReducer reducer)
        {
            if (expression.Parent != null)
            {
                expression.Parent = (IqlExpression)reducer.ReduceStaticContent(expression.Parent);
            }
            expression.Expression = reducer.ReduceStaticContent(expression.Expression);
            return expression;
        }
    }
}