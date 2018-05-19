using System.Linq.Expressions;
using Iql.Parsing.Extensions;

namespace Iql.Parsing.Expressions.QueryExpressions
{
    public class PropertyQueryExpression : ExpressionQueryExpression
    {
        public static string PropertyQueryExpressionGuidKey = "301e2db4-0132-422d-82cb-e7ce9ac95717";

        public PropertyQueryExpression(
            LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        ) : base(expression, QueryExpressionKind.NonBinary
#if TypeScript
            , evaluateContext
#endif
            )
        {
        }

        private string PropertyGuid { get; } = PropertyQueryExpressionGuidKey;

        public static bool IsWhereQueryExpression(object obj)
        {
            return obj.HasPropertyValue(nameof(PropertyGuid), PropertyQueryExpressionGuidKey);
        }
    }
}