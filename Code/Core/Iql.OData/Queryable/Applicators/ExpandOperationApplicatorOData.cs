using Iql.Queryable;
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
            if (detail.IsCount)
            {
                return ReturnCountExpand(context, detail);
            }
            return ReturnNormalExpand(context, index, detail);
        }

        private static string ReturnCountExpand<TEntity>(IQueryOperationContext<IExpandOperation, TEntity, IODataQuery, ODataQueryableAdapter> context, ExpandDetail detail)
            where TEntity : class
        {
            var expandPropertyName = $"{detail.GetExpandProperty().Name}/$count";
            var expandOperations = "";
            expandOperations +=
                detail.TargetQueryable.ToQueryWithAdapter<IODataQuery, ODataQueryableAdapter>(
                        new ODataQueryableAdapter(),
                        context.DataContext,
                        context,
                        context.Data)
                    .ToODataQuery(true);
            expandOperations = expandOperations.Trim();
            if (!string.IsNullOrWhiteSpace(expandOperations))
            {
                if (expandOperations.StartsWith("?"))
                {
                    expandOperations = expandOperations.Substring(1);
                }

                expandPropertyName += "(" + expandOperations + ")";
            }

            return expandPropertyName;
        }


        private string ReturnNormalExpand<TEntity>(IQueryOperationContext<IExpandOperation, TEntity, IODataQuery, ODataQueryableAdapter> context, int index, ExpandDetail detail) where TEntity : class
        {
            var expandProperty = detail.GetExpandProperty();
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
            var expandPropertyName = expandProperty.Name;
            if (!string.IsNullOrWhiteSpace(expandOperations))
            {
                if (expandOperations.StartsWith("?"))
                {
                    expandOperations = expandOperations.Substring(1);
                }

                expandPropertyName += "(" + expandOperations + ")";
            }

            return expandPropertyName;
        }
    }
}