namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataParenthesisParser : ODataActionParserBase<IqlParenthesisExpression>
    {
        public override IqlExpression ToQueryString(IqlParenthesisExpression action,
            ODataIqlParserContext parser)
        {
            return new IqlAggregateExpression(
                new IqlFinalExpression<string>("("),
                action.Expression,
                new IqlFinalExpression<string>(")"));
        }
    }
}