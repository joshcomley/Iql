using Iql.Parsing;

namespace Iql.OData.Parsers
{
    public class ODataParenthesisParser : ActionParser<IqlParenthesisExpression, ODataIqlData, ODataIqlExpressionAdapter
    >
    {
        public override IqlExpression ToQueryString(IqlParenthesisExpression action,
            ActionParserInstance<ODataIqlData, ODataIqlExpressionAdapter> parser)
        {
            return new IqlAggregateExpression(
                new IqlFinalExpression("("),
                action.Expression,
                new IqlFinalExpression(")"));
        }
    }
}