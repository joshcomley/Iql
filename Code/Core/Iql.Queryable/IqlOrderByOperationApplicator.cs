using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.Queryable
{
    public class IqlOrderByOperationApplicator
        : QueryOperationApplicator<OrderByOperation, IqlQueryResult, IqlQueryableAdapter>
    {
        public override void Apply<TEntity>(IQueryOperationContext<OrderByOperation, TEntity, IqlQueryResult, IqlQueryableAdapter> context)
        {
            context.Operation.Expression =
                context.Operation.Expression ??
                IqlQueryableAdapter.QueryOperationToIqlExpression<TEntity>(
                    context.Operation) as IqlPropertyExpression;
        }
    }
}