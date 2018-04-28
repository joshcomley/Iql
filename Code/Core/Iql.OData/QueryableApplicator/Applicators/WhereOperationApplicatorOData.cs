using Iql.Queryable.Operations;
using Iql.Queryable.QueryApplicator.Applicators;

namespace Iql.OData.QueryableApplicator.Applicators
{
    public class WhereOperationApplicatorOData : QueryOperationApplicator<WhereOperation, IODataQuery, ODataQueryableAdapter>
    {
        public override void Apply<TEntity>(IQueryOperationContext<WhereOperation, TEntity, IODataQuery, ODataQueryableAdapter> context)
        {
            
            var expression = ODataQueryableAdapter.GetExpression(context.Operation.Expression,
                context.DataContext.EntityConfigurationContext,
                context.Queryable.ItemType
#if TypeScript
                , context.Operation.EvaluateContext
#endif
                );
            context.Data.AddQueryPart(ODataQueryPart.Filter, expression);
        }
    }
}