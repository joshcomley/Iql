using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.DotNet.Queryable.Applicators
{
    public class WithKeyOperationApplicatorDotNet : DotNetQueryOperationApplicator<WithKeyOperation>
    {
        protected override IEnumerable<TEntity> ApplyTyped<TEntity>(
            IQueryOperationContext<WithKeyOperation, TEntity, IDotNetQueryResult, DotNetQueryableAdapter> context,
            ParameterExpression root,
            IEnumerable<TEntity> typedList)
        {
            var expressions = new List<BinaryExpression>();
            foreach (var key in context.Operation.Key.Keys)
            {
                var property = Expression.Property(root,
                    key.Name);
                var equals = Expression.Equal(property, Expression.Constant(key.Value));
                expressions.Add(equals);
            }
            Expression body = null;
            if (expressions.Count == 1)
            {
                body = expressions[0];
            }
            else if (expressions.Count > 1)
            {
                var and = Expression.And(expressions[0], expressions[1]);
                for (var i = 2; i < expressions.Count; i++)
                {
                    and = Expression.And(and, expressions[i]);
                }
                body = and;
            }
            if (body != null)
            {
                var predicate = (Func<TEntity, bool>)Expression.Lambda(body, root).Compile();
                typedList = typedList.Where(predicate);
            }
            return typedList.ToList();
        }
    }
}