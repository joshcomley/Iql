namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlAggregateExpressionReducer : IqlReducerBase<IqlAggregateExpression>
    {
        public override IIqlLiteralExpression Evaluate(IqlAggregateExpression expression, IqlReducer reducer)
        {
            //for (var i = 0; i < expression.Expressions.Count; i++)
            //{
            //    expression.Expressions[i] =
            //        reducer.ReduceStaticContent(expression.Expressions[i]);
            //}
            return null;
        }

        public override IqlExpression ReduceStaticContent(IqlAggregateExpression expression, IqlReducer reducer)
        {
            for (var i = 0; i < expression.Expressions.Count; i++)
            {
                expression.Expressions[i] =
                    reducer.ReduceStaticContent(expression.Expressions[i]);
            }
            return expression;
        }
    }
}