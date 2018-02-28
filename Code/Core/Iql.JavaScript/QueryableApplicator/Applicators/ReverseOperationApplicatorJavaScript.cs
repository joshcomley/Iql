using System;
using Iql.Queryable.Operations;
using Iql.Queryable.QueryApplicator.Applicators;

namespace Iql.JavaScript.QueryableApplicator.Applicators
{
    public class ReverseOperationApplicatorJavaScript
        : QueryOperationApplicator<ReverseOperation, IJavaScriptQueryResult, JavaScriptQueryableAdapter>
    {
        public override void Apply<TEntity>(
            IQueryOperationContext<ReverseOperation, TEntity, IJavaScriptQueryResult, JavaScriptQueryableAdapter> context)
        {
            throw new Exception("Reverse is not supported for JavaScript.");
        }
    }
}