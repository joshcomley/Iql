namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptParenthesisParser : JavaScriptActionParserBase<IqlParenthesisExpression>
    {
        public override IqlExpression ToQueryString(IqlParenthesisExpression action,
            JavaScriptIqlParserInstance parser)
        {
            return new IqlAggregateExpression(
                new IqlFinalExpression<string>("("),
                action.Expression,
                new IqlFinalExpression<string>(")")
            );
        }
    }
}