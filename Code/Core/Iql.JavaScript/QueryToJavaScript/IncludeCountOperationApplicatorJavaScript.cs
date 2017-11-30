using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.JavaScript.QueryToJavaScript
{
    public class IncludeCountOperationApplicatorJavaScript : QueryOperationApplicator<IncludeCountOperation, IJavaScriptQueryResult>
    {
        public override void Apply<TEntity>(IQueryOperationContext<IncludeCountOperation, TEntity, IJavaScriptQueryResult> context)
        {
        }
    }
}