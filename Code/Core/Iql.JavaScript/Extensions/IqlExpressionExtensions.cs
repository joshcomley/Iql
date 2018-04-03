namespace Iql.JavaScript.Extensions
{
    public static class IqlExpressionExtensions
    {
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

            var isFunc = coalesceWith == @"""""";
            return new IqlAggregateExpression(
                new IqlFinalExpression<string>(@"("),
                parent,
                new IqlFinalExpression<string>($@" || {coalesceWith}){(isFunc ? "." : "[\"")}"),
                accessorExpression,
                new IqlFinalExpression<string>($@"{(isFunc ? "" : "\"]")}")
            );
        }
    }
}