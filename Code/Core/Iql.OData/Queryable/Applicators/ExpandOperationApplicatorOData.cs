using Iql.Queryable;
using Iql.Queryable.Data;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.OData.Queryable.Applicators
{
    public class ExpandOperationApplicatorOData : QueryOperationApplicator<IExpandOperation, IODataQuery, ODataQueryableAdapter>
    {
        public override void Apply<TEntity>(IQueryOperationContext<IExpandOperation, TEntity, IODataQuery, ODataQueryableAdapter> context)
        {
            context.Data.AddQueryPart(ODataQueryPart.Expand,
                ToExpandQuery(
                    context,
                    0));
        }

        private string ToExpandQuery<TEntity>(
            IQueryOperationContext<IExpandOperation, TEntity, IODataQuery, ODataQueryableAdapter> context, 
            int index) where TEntity : class
        {
            if (index >= context.Operation.ExpandDetails.Count)
            {
                return "";
            }
            var detail = context.Operation.ExpandDetails[index];
            var expandProperty = detail.GetExpandProperty().Name;
            var expandOperations = "";
            var nested = ToExpandQuery(context, index + 1);
            if (!string.IsNullOrWhiteSpace(nested))
            {
                expandOperations += "$expand=";
                expandOperations += nested;
            }
            //        if (index === expand.ExpandDetails.Count - 1 && expand.queryExpression.queryable) {
            expandOperations +=
                detail.TargetQueryable.ToQueryWithAdapter<IODataQuery, ODataQueryableAdapter>(
                        new ODataQueryableAdapter(),
                        context.DataContext,
                        context,
                        context.Data)
                    .ToODataQuery(true);
                //detail.IsTarget
                //    ? detail.SourceQueryable.ToQueryWithAdapter(new ODataQueryableAdapter(), context).ToODataQuery(true)
                //    : ;
            //        }
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