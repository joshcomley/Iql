using System;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.JavaScript.QueryToJavaScript
{
    public class ReverseOperationApplicatorJavaScript
        : QueryOperationApplicator<ReverseOperation, IJavaScriptQueryResult>
    {
        public override void Apply<TEntity>(
            IQueryOperationContext<ReverseOperation, TEntity, IJavaScriptQueryResult> context)
        {
            throw new Exception("Reverse is not supported for JavaScript.");
        }
    }
}