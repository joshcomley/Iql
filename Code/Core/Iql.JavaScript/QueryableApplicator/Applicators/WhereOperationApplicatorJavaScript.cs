using Iql.Queryable.Operations;
using Iql.Queryable.QueryApplicator.Applicators;

namespace Iql.JavaScript.QueryableApplicator.Applicators
{
    public class WhereOperationApplicatorJavaScript : QueryOperationApplicator<WhereOperation, IJavaScriptQueryResult, JavaScriptQueryableAdapter>
    {
        public override void Apply<TEntity>(
            IQueryOperationContext<WhereOperation, TEntity, IJavaScriptQueryResult, JavaScriptQueryableAdapter> context)
        {
            var dataSetObjectName = context.Data.GetDataSetObjectName(context.Queryable.ItemType);
            context.Data.Query.AppendLine();
            context.Data.Query.Append(
                $"{dataSetObjectName} = {dataSetObjectName}");
            context.Data.AppendWhere(                
                JavaScriptQueryableAdapter.GetExpression(
                context.Operation,
                true,
                context.DataContext.EntityConfigurationContext));
        }
    }
}