namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlCollectitonQueryExpressionReducer : IqlReducerBase<IqlCollectitonQueryExpression>
    {
        public override void Traverse(IqlCollectitonQueryExpression expression, IqlTraverser reducer)
        {
            reducer.Traverse(expression.Filter);
            reducer.Traverse(expression.WithKey);
            if (expression.Expands != null)
            {
                foreach (var expand in expression.Expands)
                {
                    reducer.Traverse(expand);
                }
            }
            if (expression.OrderBys != null)
            {
                foreach (var orderBy in expression.OrderBys)
                {
                    reducer.Traverse(orderBy);
                }
            }
            reducer.Traverse(expression.Parent);
            base.Traverse(expression, reducer);
        }

        public override IIqlLiteralExpression Evaluate(IqlCollectitonQueryExpression expression, IqlReducer reducer)
        {
            return null;
        }

        public override IqlExpression ReduceStaticContent(IqlCollectitonQueryExpression expression, IqlReducer reducer)
        {
            expression.Filter = reducer.ReduceStaticContent(expression.Filter);
            expression.WithKey = (IqlWithKeyExpression)reducer.ReduceStaticContent(expression.WithKey);
            if (expression.Expands != null)
            {
                for (var i = 0; i < expression.Expands.Count; i++)
                {
                    expression.Expands[i] = (IqlExpandExpression)reducer.ReduceStaticContent(expression.Expands[i]);
                }
            }
            if (expression.OrderBys != null)
            {
                for (var i = 0; i < expression.OrderBys.Count; i++)
                {
                    expression.OrderBys[i] = (IqlOrderByExpression)reducer.ReduceStaticContent(expression.OrderBys[i]);
                }
            }
            expression.Parent = reducer.ReduceStaticContent(expression.Parent);
            return expression;
        }
    }
}