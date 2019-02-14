using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Entities;
using Iql.Entities.Rules.Relationship;
using Iql.Extensions;
using Iql.Parsing;
using Iql.Parsing.Expressions.QueryExpressions;
using Iql.Parsing.Reduction;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public abstract class Queryable<T, TQueryable, TResult> : IQueryableProvider<T, TQueryable>
        where T : class
        where TQueryable : Queryable<T, TQueryable, TResult>
        where TResult : IList
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

        public virtual Task<TQueryable> ApplyRelationshipFiltersAsync<TProperty>(IProperty relatedProperty, TProperty entity)
        {
            var ctx = new RelationshipFilterContext<TProperty>();
            ctx.Owner = entity;
#if TypeScript
            var context = new EvaluateContext(e => ctx);
#endif
            var query = this;
            var relationshipRules = relatedProperty.RelationshipFilterRules.All.ToList();
            for (var i = 0; i < relationshipRules.Count; i++)
            {
                var filterRule = relationshipRules[i];
                var filterFunction = filterRule.Run(ctx);
                query = query.Where((Expression<Func<T, bool>>)filterFunction
#if TypeScript
                    , context
#endif
                );
            }

            return Task.FromResult((TQueryable)query);
        }

        async Task<IQueryableBase> IQueryableBase.ApplyRelationshipFiltersAsync(IProperty relatedProperty, object entity)
        {
            var applyRelationshipFilters = ApplyRelationshipFiltersAsync(relatedProperty, entity);
            return await applyRelationshipFilters;
        }

        IQueryableBase IQueryableBase.Where(LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return Where((Expression<Func<T, bool>>) expression
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
            if (expression == null)
            {
                return (TQueryable)this;
            }
            var whereOperation = new WhereOperation();
#if TypeScript
            whereOperation.EvaluateContext = evaluateContext;
#endif
            if (expression.Kind != IqlExpressionKind.Lambda)
            {
                var iqlLambdaExpression = new IqlLambdaExpression
                {
                    Body = expression
                };
                var flattener = new IqlReducer();
                // Try to find the first root reference used in the expression
                var root = flattener.Traverse(expression).FirstOrDefault(e => e.Kind == IqlExpressionKind.RootReference) as IqlRootReferenceExpression;
                iqlLambdaExpression.Parameters.Add(new IqlRootReferenceExpression(root?.VariableName ?? "root"));
                expression = iqlLambdaExpression;
            }
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
        IQueryableBase IQueryableBase.OrderByDefault(bool? descending = null)
        {
            return OrderByDefault(descending);
        }

        public abstract TQueryable OrderByDefault(bool? descending = null);

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

        public abstract Task<TResult> ToListAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        public abstract Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            );
        public abstract Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            );
        public abstract Task<T> FirstAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            );
        public abstract Task<T> LastAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            );

        async Task<IEnumerable> IQueryableBase.ToListAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            return await ToListAsync((Expression<Func<T, bool>>) expression
#if TypeScript
, evaluateContext
#endif
            );
        }

        async Task<object> IQueryableBase.FirstOrDefaultAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            return await FirstOrDefaultAsync((Expression<Func<T, bool>>)expression
#if TypeScript
, evaluateContext
#endif
            );
        }
        async Task<object> IQueryableBase.LastOrDefaultAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await LastOrDefaultAsync((Expression<Func<T, bool>>)expression
#if TypeScript
, evaluateContext
#endif
            );
        }
        async Task<object> IQueryableBase.FirstAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await FirstAsync((Expression<Func<T, bool>>)expression
#if TypeScript
, evaluateContext
#endif
            );
        }
        async Task<object> IQueryableBase.LastAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await LastAsync((Expression<Func<T, bool>>)expression
#if TypeScript
, evaluateContext
#endif
                );
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