using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.JavaScript.QueryToJavaScript
{
    public class WithKeyOperationApplicatorJavaScript
        : QueryOperationApplicator<WithKeyOperation, IJavaScriptQueryResult>
    {
        public override void Apply<TEntity>(
            IQueryOperationContext<WithKeyOperation, TEntity, IJavaScriptQueryResult> context)
        {
            var javaScriptExpression = JavaScriptQueryableAdapter.GetExpression(
                context.ResolveWithKeyWhereOperation(),
                true,
                context.DataContext.EntityConfigurationContext);
            context.Data.AppendWhere(javaScriptExpression);
            context.Data.Key = context.Operation.Key;
            context.Data.HasKey = true;
        }
    }
}