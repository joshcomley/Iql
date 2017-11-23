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

        public static IqlExpression Coalesce(this IqlExpression parent, IqlExpression accessorExpression)
        {
            return new IqlAggregateExpression(
                new IqlFinalExpression<string>(@"(function() { return "),
                parent,
                new IqlFinalExpression<string>(@" === null || "),
                parent,
                new IqlFinalExpression<string>(@" === undefined ? null : "),
                accessorExpression,
                new IqlFinalExpression<string>(";})()")
            );
        }
    }
}