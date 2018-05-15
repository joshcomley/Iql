namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlLambdaExpressionReducer : IqlReducerBase<IqlLambdaExpression>
    {
        public override void Traverse(IqlLambdaExpression expression, IqlTraverser reducer)
        {
            reducer.Traverse(expression.Body);
            if (expression.Parameters != null)
            {
                for (var i = 0; i < expression.Parameters.Count; i++)
                {
                    var parameter = expression.Parameters[i];
                    reducer.Traverse(parameter);
                }
            }
            base.Traverse(expression, reducer);
        }

        public override IIqlLiteralExpression Evaluate(IqlLambdaExpression expression, IqlReducer reducer)
        {
            return null;
        }

        public override IqlExpression ReduceStaticContent(IqlLambdaExpression expression, IqlReducer reducer)
        {
            expression.Body = reducer.ReduceStaticContent(expression.Body);
            if (expression.Parameters != null)
            {
                for (var i = 0; i < expression.Parameters.Count; i++)
                {
                    expression.Parameters[i] = (IqlRootReferenceExpression) reducer.ReduceStaticContent(expression.Parameters[i]);
                }
            }
            return base.ReduceStaticContent(expression, reducer);
        }
    }
}