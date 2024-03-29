using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data.Context;
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
        public Type MappedFrom { get; set; }
        public bool? AllowOffline { get; set; } = null;

        public EvaluateContext EvaluateContext { get; set; }

        public virtual List<IQueryOperation> Operations { get; }

        public abstract void AddEntity(object entity);
        public abstract void DeleteEntity(object entity);
        public abstract Task<object> GetWithKeyAsync(object key);
        public abstract Task<IList> GetWithKeysAsync(IEnumerable<object> key);

        public TQueryable Reverse()
        {
            return Then(new ReverseOperation());
        }

        public IQueryableBase ClearOperations()
        {
            var copy = Copy();
            copy.Operations.Clear();
            return copy;
        }

        public abstract Task<bool> AnyAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        public abstract Task<bool> AllAsync(Expression<Func<T, bool>> expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        public abstract Task<long> CountAsync(Expression<Func<T, bool>> expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

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

        Task<bool> IQueryableBase.AnyAsync(LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return AnyAsync((Expression<Func<T, bool>>)expression
#if TypeScript
            , evaluateContext
#endif
            );
        }

        Task<bool> IQueryableBase.AllAsync(LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return AllAsync((Expression<Func<T, bool>>)expression
#if TypeScript
            , evaluateContext
#endif
            );
        }

        Task<long> IQueryableBase.CountAsync(LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return CountAsync((Expression<Func<T, bool>>)expression
#if TypeScript
            , evaluateContext
#endif
            );
        }

        public abstract Task<bool> AllQueryAsync(IqlLambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        public abstract Task<bool> AnyQueryAsync(IqlLambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        public abstract Task<long> CountQueryAsync(IqlLambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

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
        
        public TQueryable OrderByQuery(PropertyQueryExpression expression,
            bool? descending = null
        )
        {
            return Then(new OrderByOperation(expression, descending ?? false));
        }

        public TQueryable OrderByProperty(string propertyName, bool? descending = null)
        {
            var propertyExpression = PropertyExpression(propertyName, "entity");
            return OrderByPropertyExpression(propertyExpression, descending
            );
        }
        
        public TQueryable OrderByPropertyExpression(IqlPropertyExpression propertyExpression, bool? descending = null)
        {
            var orderByOperation = new OrderByOperation(null, descending == true);
            var lambdaExpression = new IqlLambdaExpression
            {
                Body = propertyExpression
            };
            
            var parent = propertyExpression.Parent;
            while (parent.Parent != null)
            {
                parent = parent.Parent;
            }
            lambdaExpression.Parameters.Add(parent as IqlRootReferenceExpression);
            orderByOperation.Expression = lambdaExpression;
            return Then(orderByOperation);
        }
        IQueryableBase IQueryableBase.OrderByDefault(bool? descending = null, IqlDefaultOrderKind? orderKind = null)
        {
            return OrderByDefault(descending);
        }

        public abstract TQueryable OrderByDefault(bool? descending = null, IqlDefaultOrderKind? orderKind = null);

        //        public TQueryable ExpandProperty(string propertyName
        //#if TypeScript
        //            , EvaluateContext evaluateContext = null
        //#endif
        //        )
        //        {
        //            var orderByOperation = new ExpandOperation<>(null, descending);
        //            orderByOperation.Expression = PropertyExpression(propertyName);
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

        public TQueryable Skip(int amount)
        {
            return Then(new SkipOperation(amount));
        }

        public TQueryable Take(int amount)
        {
            return Then(new TakeOperation(amount));
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
        public abstract Task<TResult> AllPagesToListAsync(ProgressNotifier progressNotifier = null,
            Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        public abstract Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> expression = null
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
        public abstract Task<T> SingleAsync(Expression<Func<T, bool>> expression = null
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
            return await ToListAsync((Expression<Func<T, bool>>)expression
#if TypeScript
, evaluateContext
#endif
            );
        }

        async Task<IEnumerable> IQueryableBase.AllPagesToListAsync(ProgressNotifier progressNotifier = null,
            LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await AllPagesToListAsync(progressNotifier, (Expression<Func<T, bool>>)expression
#if TypeScript
, evaluateContext
#endif
            );
        }

        async Task<object> IQueryableBase.SingleAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await SingleAsync((Expression<Func<T, bool>>)expression
#if TypeScript
, evaluateContext
#endif
            );
        }
        async Task<object> IQueryableBase.SingleOrDefaultAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return await SingleOrDefaultAsync((Expression<Func<T, bool>>)expression
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
            queryable.AllowOffline = AllowOffline;
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
        IQueryableBase IQueryableBase.Skip(int amount)
        {
            return Skip(amount);
        }
        IQueryableBase IQueryableBase.Take(int amount)
        {
            return Take(amount);
        }
        IQueryableBase IQueryableBase.Reverse()
        {
            return Reverse();
        }
        IQueryableBase IQueryableBase.Then(IQueryOperation operation)
        {
            return Then(operation);
        }
        public virtual IqlPropertyExpression PropertyExpression(string propertyName, string rootReferenceName = null)
        {
            //var property = this.Configuration.GetEntityByType(typeof(T)).Properties.Single(p => p.Name == propertyName);
            return IqlExpression.GetPropertyExpression(propertyName, rootReferenceName);
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

        IQueryableBase IQueryableBase.OrderByProperty(string propertyName, bool? descending = null)
        {
            return OrderByProperty(propertyName, descending);
        }

        IQueryableBase IQueryableBase.OrderByPropertyExpression(IqlPropertyExpression property, bool? descending = null)
        {
            return OrderByPropertyExpression(property, descending);
        }

        //        IQueryableBase IQueryableBase.ExpandProperty(string propertyName
        //#if TypeScript
        //            , EvaluateContext evaluateContext = null
        //#endif
        //        )
        //        {
        //            return OrderByProperty(propertyName, descending
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