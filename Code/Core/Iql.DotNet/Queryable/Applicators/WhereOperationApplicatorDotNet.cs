using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.DotNet.Queryable.Applicators
{
    public class WhereOperationApplicatorDotNet : DotNetQueryOperationApplicator<WhereOperation>
    {
        public override void Apply<TEntity>(
            IQueryOperationContext<WhereOperation, TEntity, IDotNetQueryResult> context)
        {
        }

        protected override IEnumerable<TEntity> Apply<TEntity>(
            IQueryOperationContext<WhereOperation, TEntity, IDotNetQueryResult> context,
            ParameterExpression root,
            IEnumerable<TEntity> typedList)
        {
            return typedList;
        }
    }
}