using System;
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
            QueryExpressionType type
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
#if TypeScript
            EvaluateContext = evaluateContext;
#endif
            Type = type;
        }

        public QueryExpressionType Type { get; set; }

#if TypeScript
        public EvaluateContext EvaluateContext { get; set; }
#endif

        public static bool IsQueryExpression(object obj)
        {
            return obj.HasPropertyValue(nameof(_guid), GuidKey);
        }

        public bool CanFlatten()
        {
            switch (Type)
            {
                case QueryExpressionType.Or:
                case QueryExpressionType.And:
                case QueryExpressionType.Where:
                    return true;
            }
            return false;
        }

        public QueryExpression TryFlatten<TEntity>()
        {
            return Flatten<TEntity>() ?? this;
        }

        public WhereQueryExpression<TEntity> Flatten<TEntity>()
        {
            switch (Type)
            {
                case QueryExpressionType.Or:
                case QueryExpressionType.And:
                    return ResolveBinary<TEntity>(
                        this as BinaryQueryExpression
                    );
                case QueryExpressionType.Where:
                    return this as WhereQueryExpression<TEntity>;
            }
            return null;
        }

        private WhereQueryExpression<TEntity> ResolveBinary<TEntity>
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
                switch (filter.Type)
                {
                    case QueryExpressionType.And:
                        expressionResolved = new WhereQueryExpression<TEntity>(
                            entity =>
                                lastExpression.Expression.Compile()(entity) &&
                                nextExpression.Expression.Compile()(entity)
#if TypeScript
                            , new EvaluateContext
                            {
                                Context = this,
                                Evaluate = n => Evaluator.Eval<Func<object, object>>(n)
                            }
#endif
                                );
                        break;
                    case QueryExpressionType.Or:
                        expressionResolved = new WhereQueryExpression<TEntity>(
                            entity =>
                                lastExpression.Expression.Compile()(entity) ||
                                nextExpression.Expression.Compile()(entity)
#if TypeScript
                            , new EvaluateContext
                            {
                                Context = this,
                                Evaluate = n => Evaluator.Eval<Func<object, object>>(n)
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