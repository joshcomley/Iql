using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptParenthesisParser : ActionParser<IqlParenthesisExpression, JavaScriptIqlData,
        JavaScriptIqlExpressionAdapter>
    {
        public override IqlExpression ToQueryString(IqlParenthesisExpression action,
            ActionParserInstance<JavaScriptIqlData, JavaScriptIqlExpressionAdapter> parser)
        {
            return new IqlAggregateExpression(
                new IqlFinalExpression("("),
                action.Expression,
                new IqlFinalExpression(")")
            );
        }
    }
}