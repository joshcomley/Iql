using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.DotNet.QueryableApplicator.Applicators
{
    public abstract class QueryOperationApplicatorDotNet<T> : QueryOperationApplicator<T, IDotNetQueryResult, DotNetQueryableAdapter> where T : IQueryOperation
    {
        public override void Apply<TEntity>(IQueryOperationContext<T, TEntity, IDotNetQueryResult, DotNetQueryableAdapter> context)
        {
            context.Data.Actions.Add(list =>
            {
                var typedList = list as IEnumerable<TEntity>;
                var root = Expression.Parameter(typeof(TEntity));
                return ApplyTyped(context, root, typedList);
                //var result = typedList.OrderBy((Func<TEntity, object>)sort.Compile()).ToList();
                //return result;
            });
        }

        protected abstract IEnumerable<TEntity> ApplyTyped<TEntity>(
            IQueryOperationContext<T, TEntity, IDotNetQueryResult, DotNetQueryableAdapter> context,
            ParameterExpression root,
            IEnumerable<TEntity> typedList)
            where TEntity : class;

        public virtual IEnumerable<TEntity> ApplyPropertyAction<TEntity, TProperty>(
            IQueryOperationContext<OrderByOperation, TEntity, IDotNetQueryResult, DotNetQueryableAdapter> context,
            IEnumerable<TEntity> list,
            Func<TEntity, TProperty> propertySelector) where TEntity : class
        {
            return list;
        }

        public virtual IEnumerable<TEntity> ApplyWhereAction<TEntity, TProperty>(
            IQueryOperationContext<OrderByOperation, TEntity, IDotNetQueryResult, DotNetQueryableAdapter> context,
            IEnumerable<TEntity> list,
            Func<TEntity, bool> where) where TEntity : class
        {
            return list.Where(where);
        }
    }
}