using Iql.Queryable.Operations;
using Iql.Queryable.QueryApplicator.Applicators;

namespace Iql.JavaScript.QueryableApplicator.Applicators
{
    public class IncludeCountOperationApplicatorJavaScript : QueryOperationApplicator<IncludeCountOperation, IJavaScriptQueryResult, JavaScriptQueryableAdapter>
    {
        public override void Apply<TEntity>(IQueryOperationContext<IncludeCountOperation, TEntity, IJavaScriptQueryResult, JavaScriptQueryableAdapter> context)
        {
        }
    }
}