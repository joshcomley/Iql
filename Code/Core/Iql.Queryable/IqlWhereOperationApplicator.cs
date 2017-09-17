using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.Queryable
{
    public class IqlWhereOperationApplicator
        : QueryOperationApplicator<WhereOperation, IqlQueryResult>
    {
        public override void Apply<TEntity>(IQueryOperationContext<WhereOperation, TEntity, IqlQueryResult> context)
        {
            context.Operation.Expression =
                context.Operation.Expression ??
                IqlQueryableAdapter.QueryOperationToIqlExpression<TEntity>(
                    context.Operation);
        }
    }
}