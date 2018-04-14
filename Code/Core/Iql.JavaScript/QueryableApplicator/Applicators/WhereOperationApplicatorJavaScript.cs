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
            WithRelationships(context,
                context.Operation.Expression,
                (path, relationship) =>
                {
                    var expandOperation = new ExpandOperation();
                    var source = context.DataContext.GetDbSetByEntityType(relationship.Source.Type);
                    var target = context.DataContext.GetDbSetByEntityType(relationship.Target.Type);
                    expandOperation.ExpandDetails.Add(
                        new ExpandDetail(
                            source,
                            target,
                            relationship,
                            false,
                            false));
                    ExpandOperationApplicatorJavaScript.ApplyExpand<TEntity>(expandOperation, context.Data, true);
                });
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