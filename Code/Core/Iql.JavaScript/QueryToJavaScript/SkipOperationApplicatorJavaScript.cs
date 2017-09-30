using System;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.JavaScript.QueryToJavaScript
{
    public class SkipOperationApplicatorJavaScript
        : QueryOperationApplicator<SkipOperation, IJavaScriptQueryResult>
    {
        public override void Apply<TEntity>(
            IQueryOperationContext<SkipOperation, TEntity, IJavaScriptQueryResult> context)
        {
            throw new Exception("Skip is not supported for JavaScript.");
        }
    }
}