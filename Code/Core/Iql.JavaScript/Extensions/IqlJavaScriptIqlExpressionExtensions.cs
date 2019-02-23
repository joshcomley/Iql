using Newtonsoft.Json;

namespace Iql.JavaScript.Extensions
{
    public static class IqlJavaScriptIqlExpressionExtensions
    {
        public static bool NormalizeJson { get; set; } = false;
        public static string SerializeDeserialize(this IqlExpression expression)
        {
            var json = JsonConvert.SerializeObject(expression);
            if (NormalizeJson)
            {
                json = json.NormalizeJson();
            }
            return $"JSON.parse('{json}')";
        }

        public static IqlAggregateExpression DotAccess(this IqlExpression parent, IqlExpression action)
        {
            var accessorExpression = new IqlAggregateExpression(
                parent,
                new IqlFinalExpression<string>("."),
                action
            );
            return accessorExpression;
        }

        public static IqlExpression Coalesce(this IqlExpression parent, IqlExpression accessorExpression, string coalesceWith = null)
        {
            if (coalesceWith == null)
            {
                coalesceWith = "{}";
            }

            var isFunc = coalesceWith == @"''";
            return new IqlAggregateExpression(
                new IqlFinalExpression<string>(@"("),
                parent,
                new IqlFinalExpression<string>($@" || {coalesceWith})."),
                accessorExpression
            );
        }
    }
}