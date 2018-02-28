using Iql.Queryable.Operations;
using Iql.Queryable.QueryApplicator.Applicators;

namespace Iql.Queryable.Data.DataStores.InMemory.QueryApplicator.Applicators
{
    public class IqlWhereOperationApplicator
        : QueryOperationApplicator<WhereOperation, IqlQueryResult, IqlQueryableAdapter>
    {
        public override void Apply<TEntity>(IQueryOperationContext<WhereOperation, TEntity, IqlQueryResult, IqlQueryableAdapter> context)
        {
            context.Operation.Expression =
                context.Operation.Expression ??
                IqlQueryableAdapter.QueryOperationToIqlExpression<TEntity>(
                    context.Operation);
        }
    }
}