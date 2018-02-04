using Iql.Queryable.Data;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.JavaScript.QueryToJavaScript
{
    public class ExpandCountOperationApplicatorJavaScript : QueryOperationApplicator<IExpandCountOperation, IJavaScriptQueryResult, JavaScriptQueryableAdapter>
    {
        public override void Apply<TEntity>(IQueryOperationContext<IExpandCountOperation, TEntity, IJavaScriptQueryResult, JavaScriptQueryableAdapter> context)
        {
            ExpandOperationApplicatorJavaScript.ApplyExpand<TEntity>(
                context.Operation,
                context.Data);
        }
    }
}