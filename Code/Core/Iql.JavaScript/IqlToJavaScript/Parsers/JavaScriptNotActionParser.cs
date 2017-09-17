using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptNotActionParser : ActionParser<IqlNotExpression, JavaScriptIqlData, JavaScriptIqlExpressionAdapter>
    {
        public override IqlExpression ToQueryString(IqlNotExpression action, ActionParserInstance<JavaScriptIqlData, JavaScriptIqlExpressionAdapter> parser)
        {
            return new IqlAggregateExpression(
                new IqlFinalExpression("!"),
                new IqlParenthesisExpression(action.Expression)
                );
        }
    }
}