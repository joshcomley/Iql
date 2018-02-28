using System;
using System.Linq.Expressions;
using Iql.Parsing;
using Iql.Parsing.Extensions;

namespace Iql.Queryable.Expressions.QueryExpressions
{
    public class WhereQueryExpression : ExpressionQueryExpression
    {
        public static string WhereGuidKey = "4c8e97bd-77a6-4ee6-8617-8922802f676d";

        public WhereQueryExpression(
            LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        ) : base(expression, QueryExpressionType.Where
#if TypeScript
            , evaluateContext
#endif
            )
        {
        }

        public string WhereGuid { get; } = WhereGuidKey;

        public static bool IsWhereQueryExpression(object obj)
        {
            return obj.HasPropertyValue(nameof(WhereGuid), WhereGuidKey);
        }
    }
}