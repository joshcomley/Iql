namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlParenthesisExpressionReducer : IqlReducerBase<IqlParenthesisExpression>
    {
        public override IqlLiteralExpression Evaluate(IqlParenthesisExpression expression, IqlReducer reducer)
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
                expression.Parent = reducer.ReduceStaticContent(expression.Parent);
            }
            expression.Expression = reducer.ReduceStaticContent(expression.Expression);
            return expression;
        }
    }
}