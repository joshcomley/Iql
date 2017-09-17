using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.OData.Queryable.Applicators
{
    public class WhereOperationApplicatorOData : QueryOperationApplicator<WhereOperation, IODataQuery>
    {
        public override void Apply<TEntity>(IQueryOperationContext<WhereOperation, TEntity, IODataQuery> context)
        {
            context.Data.Filters.Add(ODataQueryableAdapter.GetExpression(context.Operation,
                context.DataContext.EntityConfigurationContext));
        }
    }
}