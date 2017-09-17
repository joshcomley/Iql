using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptStringLengthParser : ActionParser<IqlStringLengthExpression, JavaScriptIqlData,
        JavaScriptIqlExpressionAdapter>
    {
        protected string Separator = ".";

        public override IqlExpression ToQueryString(IqlStringLengthExpression action,
            ActionParserInstance<JavaScriptIqlData, JavaScriptIqlExpressionAdapter> parser)
        {
            return new IqlAggregateExpression(
                action.Parent,
                new IqlFinalExpression(".length"));
        }
    }
}