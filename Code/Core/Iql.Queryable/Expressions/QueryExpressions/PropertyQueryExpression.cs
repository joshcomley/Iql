using System;
using System.Linq.Expressions;
using Iql.Parsing;
using Iql.Parsing.Extensions;

namespace Iql.Queryable.Expressions.QueryExpressions
{
    public class PropertyQueryExpression<T, TProperty> : ExpressionQueryExpression<T, TProperty>
    {
        public static string PropertyQueryExpressionGuidKey = "301e2db4-0132-422d-82cb-e7ce9ac95717";

        public PropertyQueryExpression(
            Expression<Func<T, TProperty>> expression,
            EvaluateContext evaluateContext = null
        ) : base(expression, QueryExpressionType.NonBinary, evaluateContext)
        {
        }

        private string PropertyGuid { get; } = PropertyQueryExpressionGuidKey;

        public static bool IsWhereQueryExpression(object obj)
        {
            return obj.HasPropertyValue(nameof(PropertyGuid), PropertyQueryExpressionGuidKey);
        }
    }
}