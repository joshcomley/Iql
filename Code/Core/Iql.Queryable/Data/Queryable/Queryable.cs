using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Extensions;
using Iql.Parsing;
using Iql.Queryable.Data.Lists;
using Iql.Queryable.Expressions;
using Iql.Queryable.Expressions.Conversion;
using Iql.Queryable.Expressions.QueryExpressions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.Queryable
{
    public abstract class Queryable<T, TQueryable> : IQueryableProvider<T, TQueryable> 
        where T : class
        where TQueryable : Queryable<T, TQueryable>
    {
        protected Queryable(EvaluateContext evaluateContext = null)
        {
            EvaluateContext = evaluateContext;
            ItemType = typeof(T);
            Operations = new List<IQueryOperation>();
        }

        public bool? TrackEntities { get; set; } = null;

        public EvaluateContext EvaluateContext { get; set; }

        public List<IQueryOperation> Operations { get; }

        public abstract void AddEntity(object entity);
        public abstract void DeleteEntity(object entity);
        public abstract Task<object> GetWithKeyAsync(object key);
        public abstract Task<IList> GetWithKeysAsync(IEnumerable<object> key);

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
            var queryExpression = new WhereQueryExpression(expression);
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
                new PropertyQueryExpression(
                    expression
#if TypeScript
                    , evaluateContext ?? EvaluateContext
#endif
                    ));
        }

        public TQueryable OrderByQuery(PropertyQueryExpression expression
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
        IQueryableBase IQueryableBase.OrderByDefault(bool descending = false)
        {
            return OrderByDefault(descending);
        }

        public abstract TQueryable OrderByDefault(bool descending = false);

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
                new PropertyQueryExpression(expression
#if TypeScript
                , evaluateContext ?? EvaluateContext
#endif
                ), true));
        }

        public TQueryable OrderByDescendingQuery(PropertyQueryExpression expression
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

        public abstract Task<DbList<T>> ToListAsync();

        async Task<IDbList> IQueryableBase.ToListAsync()
        {
            return await ToListAsync();
        }
        public abstract TQueryable New();
        public bool HasDefaults { get; set; }

        public virtual TQueryable Copy()
        {
            var queryable = New();
            queryable.HasDefaults = HasDefaults;
            queryable.TrackEntities = TrackEntities;
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
        public abstract Task<IqlDataSetQueryExpression> ToIqlAsync(IExpressionToIqlConverter expressionConverter = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
    }
}