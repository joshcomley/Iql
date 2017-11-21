namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptNotActionParser : JavaScriptActionParserBase<IqlNotExpression>
    {
        public override IqlExpression ToQueryString(IqlNotExpression action, JavaScriptIqlParserInstance parser)
        {
            return new IqlAggregateExpression(
                new IqlFinalExpression<string>("!"),
                new IqlParenthesisExpression(action.Expression)
                );
        }
    }
}