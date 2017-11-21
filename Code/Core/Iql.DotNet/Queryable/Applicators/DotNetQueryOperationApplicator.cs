using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.DotNet.Queryable.Applicators
{
    public abstract class DotNetQueryOperationApplicator<T> : QueryOperationApplicator<T, IDotNetQueryResult> where T : IQueryOperation
    {
        protected void AddPropertyAction<TEntity>(IQueryOperationContext<T, TEntity, IDotNetQueryResult> context) where TEntity : class
        {
            context.Data.Actions.Add(list =>
            {
                var typedList = list as IList<TEntity>;
                var root = Expression.Parameter(typeof(TEntity));
                return Apply(context, root, typedList);
                //var result = typedList.OrderBy((Func<TEntity, object>)sort.Compile()).ToList();
                //return result;
            });
        }

        protected abstract IEnumerable<TEntity> Apply<TEntity>(
            IQueryOperationContext<T, TEntity, IDotNetQueryResult> context,
            ParameterExpression root,
            IEnumerable<TEntity> typedList)
            where TEntity : class;

        public virtual IEnumerable<TEntity> ApplyPropertyAction<TEntity, TProperty>(
            IQueryOperationContext<OrderByOperation, TEntity, IDotNetQueryResult> context,
            IEnumerable<TEntity> list,
            Func<TEntity, TProperty> propertySelector) where TEntity : class
        {
            return list;
        }

        public virtual IEnumerable<TEntity> ApplyWhereAction<TEntity, TProperty>(
            IQueryOperationContext<OrderByOperation, TEntity, IDotNetQueryResult> context,
            IEnumerable<TEntity> list,
            Func<TEntity, bool> where) where TEntity : class
        {
            return list.Where(where);
        }
    }
}