using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.JavaScript.QueryToJavaScript
{
    public class ExpandOperationApplicatorJavaScript
        : QueryOperationApplicator<IExpandOperation, IJavaScriptQueryResult>
    {
        public override void Apply<TEntity>(
            IQueryOperationContext<IExpandOperation, TEntity, IJavaScriptQueryResult> context)
        {
            context.Data.Expands.Add(new JavaScriptExpand(context.Operation));
            //console.log("EXPANDING: " + propertyToExpand.propertyName);
            //data.filters.push(JavaScriptQueryableAdapter.GetExpression(operation));
        }
    }
}