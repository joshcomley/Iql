namespace Iql.JavaScript.Extensions
{
    public static class IqlExpressionExtensions
    {
        public static IqlAggregateExpression DotAccess(this IqlExpression parent, IqlExpression action)
        {
            var accessorExpression = new IqlAggregateExpression(
                parent,
                new IqlFinalExpression("."),
                action
            );
            return accessorExpression;
        }

        public static IqlExpression Coalesce(this IqlExpression parent, IqlExpression accessorExpression)
        {
            return new IqlAggregateExpression(
                new IqlFinalExpression(@"(function() { return "),
                parent,
                new IqlFinalExpression(@" === null || "),
                parent,
                new IqlFinalExpression(@" === undefined ? null : "),
                accessorExpression,
                new IqlFinalExpression(";})()")
            );
        }
    }
}