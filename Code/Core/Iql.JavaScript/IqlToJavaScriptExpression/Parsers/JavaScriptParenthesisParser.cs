namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptParenthesisParser : JavaScriptActionParserBase<IqlParenthesisExpression>
    {
        public override IqlExpression ToQueryString(IqlParenthesisExpression action,
            JavaScriptIqlParserContext parser)
        {
            return new IqlAggregateExpression(
                new IqlFinalExpression<string>("("),
                action.Expression,
                new IqlFinalExpression<string>(")")
            );
        }
    }
}