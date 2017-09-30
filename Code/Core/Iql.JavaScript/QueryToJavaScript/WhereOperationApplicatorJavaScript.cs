using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.JavaScript.QueryToJavaScript
{
    public class WhereOperationApplicatorJavaScript : QueryOperationApplicator<WhereOperation, IJavaScriptQueryResult>
    {
        public override void Apply<TEntity>(
            IQueryOperationContext<WhereOperation, TEntity, IJavaScriptQueryResult> context)
        {
            context.Data.AppendWhere(JavaScriptQueryableAdapter.GetExpression(
                context.Operation,
                true,
                context.DataContext.EntityConfigurationContext));
        }
    }
}