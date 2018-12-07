namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptNotActionParser : JavaScriptActionParserBase<IqlNotExpression>
    {
        public override IqlExpression ToQueryString(IqlNotExpression action, JavaScriptIqlParserContext parser)
        {
            return new IqlAggregateExpression(
                new IqlFinalExpression<string>("!"),
                new IqlParenthesisExpression(action.Expression)
                );
        }
    }
}