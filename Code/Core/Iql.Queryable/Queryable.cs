using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Extensions;
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

        public abstract void AddEntity(object entity);
        public abstract void DeleteEntity(object entity);
        public abstract Task<object> WithKey(object key);

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
            , evaluateContext
#endif
                );
        }

        public TQueryable WherePropertyEquals(string propertyName, object value
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var expression = new IqlIsEqualToExpression(
                PropertyExpression(propertyName), new IqlLiteralExpression(value, value.GetType().ToIqlType()));
            return WhereEquals(expression
#if TypeScript
                , evaluateContext
#endif
);
        }

        public TQueryable WhereEquals(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var whereOperation = new WhereOperation();
#if TypeScript
            whereOperation.EvaluateContext = evaluateContext;
#endif
            whereOperation.Expression = expression;
            return Then(whereOperation);
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

        public TQueryable OrderByProperty(string propetyName, bool descending = false
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var orderByOperation = new OrderByOperation(null, descending);
            orderByOperation.Expression = PropertyExpression(propetyName);
            return Then(orderByOperation);
        }

//        public TQueryable ExpandProperty(string propetyName
//#if TypeScript
//            , EvaluateContext evaluateContext = null
//#endif
//        )
//        {
//            var orderByOperation = new ExpandOperation<>(null, descending);
//            orderByOperation.Expression = PropertyExpression(propetyName);
//            return Then();
//        }

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

        public virtual TQueryable Then(IQueryOperation operation = null)
        {
            var queryable = Copy();
            if (operation != null)
            {
                queryable.Operations.Add(operation);
            }
            return queryable;
        }

        public Type ItemType { get; }

        public abstract Task<DbList<T>> ToList();

        async Task<IList> IQueryableBase.ToList()
        {
            return await ToList();
        }
        public abstract TQueryable New();
        public bool HasDefaults { get; set; }

        public virtual TQueryable Copy()
        {
            var queryable = New();
            queryable.HasDefaults = HasDefaults;
            Operations.ForEach(element => { queryable.Operations.Add(element); });
            return queryable;
        }

        IQueryableBase IQueryableBase.New()
        {
            return New();
        }
        IQueryableBase IQueryableBase.Copy()
        {
            return Copy();
        }
        IQueryableBase IQueryableBase.Skip(int skip)
        {
            return Skip(skip);
        }
        IQueryableBase IQueryableBase.Take(int take)
        {
            return Take(take);
        }
        IQueryableBase IQueryableBase.Reverse()
        {
            return Reverse();
        }
        IQueryableBase IQueryableBase.Then(IQueryOperation operation)
        {
            return Then(operation);
        }
        public virtual IqlPropertyExpression PropertyExpression(string propertyName)
        {
            //var property = this.Configuration.GetEntityByType(typeof(T)).Properties.Single(p => p.Name == propertyName);
            return IqlExpression.GetPropertyExpression(propertyName);
        }

        IQueryableBase IQueryableBase.WherePropertyEquals(string propertyName, object value
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return WherePropertyEquals(propertyName, value
#if TypeScript
                , evaluateContext
#endif
            );
        }

        IQueryableBase IQueryableBase.WhereEquals(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return WhereEquals(expression
#if TypeScript
                , evaluateContext
#endif
            );
        }

        IQueryableBase IQueryableBase.WhereQuery(QueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return WhereQuery(expression
#if TypeScript
                , evaluateContext
#endif
            );
        }

        IQueryableBase IQueryableBase.OrderByProperty(string propetyName, bool descending = false
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return OrderByProperty(propetyName, descending
#if TypeScript
                , evaluateContext
#endif
            );
        }

//        IQueryableBase IQueryableBase.ExpandProperty(string propetyName
//#if TypeScript
//            , EvaluateContext evaluateContext = null
//#endif
//        )
//        {
//            return OrderByProperty(propetyName, descending
//#if TypeScript
//                , evaluateContext
//#endif
//            );
//        }
    }
}