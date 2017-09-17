using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.Parsing;
using Iql.Queryable.Expressions.QueryExpressions;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public abstract class Queryable<T, TQueryable> : IQueryableProvider<T, TQueryable> where T : class
        where TQueryable : IQueryable<T>
    {
        public Queryable(EvaluateContext evaluateContext = null)
        {
            EvaluateContext = evaluateContext;
            ItemType = typeof(T);
            Operations = new List<IQueryOperation>();
        }

        public EvaluateContext EvaluateContext { get; set; }

        public List<IQueryOperation> Operations { get; }

        public TQueryable Expand<TTarget>(Expression<Func<T, TTarget>> property) where TTarget : class
        {
            return ExpandQuery(new ExpandQueryExpression<T, TTarget>(
                property));
        }

        public TQueryable ExpandQuery<TTarget>(ExpandQueryExpression<T, TTarget> expression,
            EvaluateContext evaluateContext = null) where TTarget : class
        {
            return Then(new ExpandOperation<T, TTarget>(expression));
        }

        public TQueryable Reverse()
        {
            return Then(new ReverseOperation());
        }

        public TQueryable Where(Expression<Func<T, bool>> expression, EvaluateContext evaluateContext = null)
        {
            var queryExpression = new WhereQueryExpression<T>(expression);
            return WhereQuery(queryExpression, evaluateContext);
        }

        public TQueryable WhereQuery(QueryExpression expression, EvaluateContext evaluateContext = null)
        {
            expression.EvaluateContext =
                expression.EvaluateContext ?? evaluateContext ?? EvaluateContext;
            return Then(new WhereOperation(expression));
        }


        public TQueryable OrderBy<TProperty>(Expression<Func<T, TProperty>> expression,
            EvaluateContext evaluateContext = null)
        {
            return OrderByQuery(
                new PropertyQueryExpression<T, TProperty>(
                    expression, evaluateContext ?? EvaluateContext));
        }

        public TQueryable OrderByQuery<TProperty>(PropertyQueryExpression<T, TProperty> expression,
            EvaluateContext evaluateContext = null)
        {
            return Then(new OrderByOperation(expression));
        }

        public TQueryable OrderByDescending<TProperty>(Expression<Func<T, TProperty>> expression,
            EvaluateContext evaluateContext = null)
        {
            return Then(new OrderByOperation(
                new PropertyQueryExpression<T, TProperty>(expression, evaluateContext ?? EvaluateContext), true));
        }

        public TQueryable OrderByDescendingQuery<TProperty>(PropertyQueryExpression<T, TProperty> expression,
            EvaluateContext evaluateContext = null)
        {
            return Then(new OrderByOperation(expression, true));
        }

        public virtual TQueryable Then(IQueryOperation operation)
        {
            var queryable = New();
            Operations.ForEach(element => { queryable.Operations.Add(element); });
            queryable.Operations.Add(operation);
            return queryable;
        }

        public Type ItemType { get; }

        protected abstract TQueryable New();
    }
}