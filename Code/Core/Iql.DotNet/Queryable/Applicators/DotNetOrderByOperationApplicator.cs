using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.DotNet.Queryable.Applicators
{
    public class DotNetOrderByOperationApplicator : QueryOperationApplicator<OrderByOperation, IDotNetQueryResult>
    {
        public override void Apply<TEntity>(
            IQueryOperationContext<OrderByOperation, TEntity, IDotNetQueryResult> context)
        {
            context.Data.Actions.Add(list =>
            {
                var typedList = list as IList<TEntity>;
                var root = Expression.Parameter(typeof(TEntity));
                var property = Expression.Property(root,
                    (context.Operation.Expression as IqlPropertyExpression).PropertyName);
                var sort = Expression.Lambda(property, root);
                var methodInfo = GetType().GetMethod("OrderBy");
                return (IList) methodInfo.MakeGenericMethod(typeof(TEntity), property.Type)
                    .Invoke(this, new object[] {typedList, sort.Compile()});
                //var result = typedList.OrderBy((Func<TEntity, object>)sort.Compile()).ToList();
                //return result;
            });
        }

        public List<TEntity> OrderBy<TEntity, TProperty>(IList<TEntity> list, Func<TEntity, TProperty> orderBy)
        {
            return list.OrderBy(orderBy).ToList();
        }
    }
}