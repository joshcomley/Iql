using Iql.Queryable.Expressions;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.Queryable.Operations
{
    public class WhereOperation : ExpressionQueryOperation<IqlExpression, ExpressionQueryExpressionBase>
    {
        private readonly QueryExpression _queryExpression;

        public WhereOperation(QueryExpression queryExpression = null)
        {

            _queryExpression = queryExpression;
#if TypeScript
            EvaluateContext = _queryExpression?.EvaluateContext;
#endif
            //if (queryExpression)
            //{
            //    if (QueryExpression.IsQueryExpression(queryExpression))
            //    {
            //    }
            //    else
            //    {
            //        this.expression = queryExpression as IqlExpression;
            //    }
            //}
        }

        public override QueryExpression GetExpression()
        {
            return _queryExpression;
        }
    }
}