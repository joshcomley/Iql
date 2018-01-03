using Iql.Queryable;
using Iql.Queryable.Data;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.OData.Queryable.Applicators
{
    public class ExpandCountOperationApplicatorOData : QueryOperationApplicator<IExpandCountOperation, IODataQuery>
    {
        public override void Apply<TEntity>(IQueryOperationContext<IExpandCountOperation, TEntity, IODataQuery> context)
        {
            context.Data.AddQueryPart(ODataQueryPart.Expand,
                ToExpandQuery(
                    context.DataContext,
                    context.Operation,
                    0));
        }

        private string ToExpandQuery(
            IDataContext context,
            IExpandOperation expand, int index)
        {
            if (index >= expand.ExpandDetails.Count)
            {
                return "";
            }
            var detail = expand.ExpandDetails[index];
            var expandProperty = detail.GetExpandProperty().Name + "/$count";
            var expandOperations = "";
            expandOperations +=
                detail.TargetQueryable.ToQueryWithAdapter(new ODataQueryableAdapter(), context).ToODataQuery(true);
            expandOperations = expandOperations.Trim();
            if (!string.IsNullOrWhiteSpace(expandOperations))
            {
                if (expandOperations.StartsWith("?"))
                {
                    expandOperations = expandOperations.Substring(1);
                }
                expandProperty += "(" + expandOperations + ")";
            }
            return expandProperty;
        }
    }
}