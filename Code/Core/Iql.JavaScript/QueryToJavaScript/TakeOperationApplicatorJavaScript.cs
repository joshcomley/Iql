using System;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.JavaScript.QueryToJavaScript
{
    public class TakeOperationApplicatorJavaScript
        : QueryOperationApplicator<TakeOperation, IJavaScriptQueryResult>
    {
        public override void Apply<TEntity>(
            IQueryOperationContext<TakeOperation, TEntity, IJavaScriptQueryResult> context)
        {
            throw new Exception("Take is not supported for JavaScript.");
        }
    }
}