using System;
using Iql.Queryable.Operations;
using Iql.Queryable.QueryApplicator.Applicators;

namespace Iql.JavaScript.QueryableApplicator.Applicators
{
    public class TakeOperationApplicatorJavaScript
        : QueryOperationApplicator<TakeOperation, IJavaScriptQueryResult, JavaScriptQueryableAdapter>
    {
        public override void Apply<TEntity>(
            IQueryOperationContext<TakeOperation, TEntity, IJavaScriptQueryResult, JavaScriptQueryableAdapter> context)
        {
            throw new Exception("Take is not supported for JavaScript.");
        }
    }
}