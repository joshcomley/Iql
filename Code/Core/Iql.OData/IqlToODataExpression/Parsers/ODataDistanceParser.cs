namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataDistanceParser : ODataActionParserBase<IqlDistanceExpression>
    {
        public override IqlExpression ToQueryString(IqlDistanceExpression action, ODataIqlParserInstance parser)
        {
            return new IqlAggregateExpression(
                new IqlFinalExpression<string>("geo.distance("),
                action.Left,
                new IqlFinalExpression<string>(","),
                action.Right,
                new IqlFinalExpression<string>(")"));
        }
    }
}