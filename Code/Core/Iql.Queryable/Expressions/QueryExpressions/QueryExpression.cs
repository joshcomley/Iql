using System;
using System.Linq.Expressions;
using Iql.Parsing;
using Iql.Parsing.Extensions;

namespace Iql.Queryable.Expressions.QueryExpressions
{
    public abstract class QueryExpression
    {
        public static string Eval =
            "this.queryableExpressionEvalContext = { evaluate: function (n) {return eval(n);}, context: this }";

        private static readonly string GuidKey = "12694f14-4fb1-4a38-963c-ac851113d6e7";

        private string _guid = GuidKey;

        // In JavaScript any variable can be "truthy" so allow a return of any
        protected QueryExpression(
            QueryExpressionKind kind
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
#if TypeScript
            EvaluateContext = evaluateContext;
#endif
            Kind = kind;
        }

        public QueryExpressionKind Kind { get; set; }

#if TypeScript
        public EvaluateContext EvaluateContext { get; set; }
#endif

        public static bool IsQueryExpression(object obj)
        {
            return obj.HasPropertyValue(nameof(_guid), GuidKey);
        }

        public bool CanFlatten()
        {
            switch (Kind)
            {
                case QueryExpressionKind.Or:
                case QueryExpressionKind.And:
                case QueryExpressionKind.Where:
                    return true;
            }
            return false;
        }

        public QueryExpression TryFlatten<TEntity>()
        {
            return Flatten<TEntity>() ?? this;
        }

        public WhereQueryExpression Flatten<TEntity>()
        {
            switch (Kind)
            {
                case QueryExpressionKind.Or:
                case QueryExpressionKind.And:
                    return ResolveBinary<TEntity>(
                        this as BinaryQueryExpression
                    );
                case QueryExpressionKind.Where:
                    return this as WhereQueryExpression;
            }
            return null;
        }

        private WhereQueryExpression ResolveBinary<TEntity>
        (
            BinaryQueryExpression filter
        )
        {
            var expression = filter;
            var expressionResolved = expression.Left.Flatten<TEntity>();
            var otherExpressions = expression.Right;
            for (var i = 0; i < otherExpressions.Count; i++)
            {
                var lastExpression = expressionResolved;
                var nextExpression = otherExpressions[i].Flatten<TEntity>();
                switch (filter.Kind)
                {
                    case QueryExpressionKind.And:
                        Expression<Func<TEntity, bool>> andLambda = entity =>
                            ((Func<TEntity, bool>)lastExpression.Expression.Compile())(entity) &&
                            ((Func<TEntity, bool>)nextExpression.Expression.Compile())(entity);
                        expressionResolved = new WhereQueryExpression(
                            andLambda
#if TypeScript
                            , 
                            new EvaluateContext
                            {
                                Context = this,
                                Evaluate = n => (Func<object, object>)Evaluator.Eval(n)
                            }
#endif
                                );
                        break;
                    case QueryExpressionKind.Or:
                        Expression<Func<TEntity, bool>> orLambda = entity =>
                            ((Func<TEntity, bool>)lastExpression.Expression.Compile())(entity) ||
                            ((Func<TEntity, bool>)nextExpression.Expression.Compile())(entity);
                        expressionResolved = new WhereQueryExpression(
                            orLambda
#if TypeScript
                            , new EvaluateContext
                            {
                                Context = this,
                                Evaluate = n => (Func<object, object>)Evaluator.Eval(n)
                            }
#endif
                            );
                        break;
                }
            }
            return expressionResolved;
        }
    }
}