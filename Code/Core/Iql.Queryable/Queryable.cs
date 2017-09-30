using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.Parsing;
using Iql.Queryable.Expressions.QueryExpressions;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public abstract class Queryable<T, TQueryable> : IQueryableProvider<T, TQueryable> 
        where T : class
        where TQueryable : IQueryable<T>
    {
        protected Queryable(EvaluateContext evaluateContext = null)
        {
            EvaluateContext = evaluateContext;
            ItemType = typeof(T);
            Operations = new List<IQueryOperation>();
        }

        public EvaluateContext EvaluateContext { get; set; }

        public List<IQueryOperation> Operations { get; }

        public TQueryable Reverse()
        {
            return Then(new ReverseOperation());
        }

        public TQueryable Where(Expression<Func<T, bool>> expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            var queryExpression = new WhereQueryExpression<T>(expression);
            return WhereQuery(queryExpression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
                );
        }

        public TQueryable WhereQuery(QueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
#if TypeScript
            expression.EvaluateContext =
                expression.EvaluateContext ?? evaluateContext ?? EvaluateContext;
#endif
            return Then(new WhereOperation(expression));
        }
        
        public TQueryable OrderBy<TProperty>(Expression<Func<T, TProperty>> expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            return OrderByQuery(
                new PropertyQueryExpression<T, TProperty>(
                    expression
#if TypeScript
                    , evaluateContext ?? EvaluateContext
#endif
                    ));
        }

        public TQueryable OrderByQuery<TProperty>(PropertyQueryExpression<T, TProperty> expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            return Then(new OrderByOperation(expression));
        }

        public TQueryable OrderByDescending<TProperty>(Expression<Func<T, TProperty>> expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            return Then(new OrderByOperation(
                new PropertyQueryExpression<T, TProperty>(expression
#if TypeScript
                , evaluateContext ?? EvaluateContext
#endif
                ), true));
        }

        public TQueryable OrderByDescendingQuery<TProperty>(PropertyQueryExpression<T, TProperty> expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            return Then(new OrderByOperation(expression, true));
        }

        public TQueryable Skip(int skip)
        {
            return Then(new SkipOperation(skip));
        }

        public TQueryable Take(int take)
        {
            return Then(new TakeOperation(take));
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