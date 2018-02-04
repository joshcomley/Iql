using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.JavaScript.QueryToJavaScript
{
    public class IncludeCountOperationApplicatorJavaScript : QueryOperationApplicator<IncludeCountOperation, IJavaScriptQueryResult, JavaScriptQueryableAdapter>
    {
        public override void Apply<TEntity>(IQueryOperationContext<IncludeCountOperation, TEntity, IJavaScriptQueryResult, JavaScriptQueryableAdapter> context)
        {
        }
    }
}