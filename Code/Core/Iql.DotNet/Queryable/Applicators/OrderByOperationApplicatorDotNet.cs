using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.DotNet.Queryable.Applicators
{
    public class OrderByOperationApplicatorDotNet : DotNetQueryOperationApplicator<OrderByOperation>
    {
        public override void Apply<TEntity>(
            IQueryOperationContext<OrderByOperation, TEntity, IDotNetQueryResult> context)
        {
            AddPropertyAction(context);
        }

        protected override IEnumerable<TEntity> Apply<TEntity>(IQueryOperationContext<OrderByOperation, TEntity, IDotNetQueryResult> context, ParameterExpression root, IEnumerable<TEntity> typedList)
        {
            var property = Expression.Property(root,
                (context.Operation.Expression as IqlPropertyExpression).PropertyName);
            var lambda = Expression.Lambda(property, root);
            var methodInfo = GetType().GetMethod(nameof(ApplyPropertyAction));
            return (IEnumerable<TEntity>)methodInfo.MakeGenericMethod(typeof(TEntity), property.Type)
                .Invoke(this, new object[] { typedList, lambda.Compile() });
        }

        public override IEnumerable<TEntity> ApplyPropertyAction<TEntity, TProperty>(
            IQueryOperationContext<OrderByOperation, TEntity, IDotNetQueryResult> context,
            IEnumerable<TEntity> list,
            Func<TEntity, TProperty> propertySelector)
        {
            if (context.Operation.IsDescending())
            {
                return list.OrderByDescending(propertySelector);
            }
            return list.OrderBy(propertySelector);
        }
    }
}