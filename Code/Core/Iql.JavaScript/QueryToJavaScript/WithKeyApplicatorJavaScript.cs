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
            context.Data.AppendWhere(JavaScriptQueryableAdapter.GetExpression(
                context.ResolveWithKkeyWhereOperation(),
                true,
                context.DataContext.EntityConfigurationContext));
            context.Data.Key = context.Operation.Key;
            context.Data.HasKey = true;
        }
    }
}