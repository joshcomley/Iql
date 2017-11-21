namespace Iql.OData.Parsers
{
    public class ODataParenthesisParser : ODataActionParserBase<IqlParenthesisExpression>
    {
        public override IqlExpression ToQueryString(IqlParenthesisExpression action,
            ODataIqlParserInstance parser)
        {
            return new IqlAggregateExpression(
                new IqlFinalExpression("("),
                action.Expression,
                new IqlFinalExpression(")"));
        }
    }
}