using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.JavaScript.QueryToJavaScript
{
    public class OrderByOperationApplicatorJavaScript : QueryOperationApplicator<OrderByOperation,
        IJavaScriptQueryResult>
    {
        public override void Apply<TEntity>(
            IQueryOperationContext<OrderByOperation, TEntity, IJavaScriptQueryResult> context)
        {
            var orderBy = JavaScriptQueryableAdapter.GetExpression(
                context.Operation,
                false,
                context.DataContext.EntityConfigurationContext, "");
            var orderByExpression = new JavaScriptOrderByExpression(
                orderBy.RootVariableName,
                orderBy.Expression,
                context.Operation.IsDescending()
            );
            context.Data.OrderBys.Add(orderByExpression);
        }
    }
}