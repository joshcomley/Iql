using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptStringLengthParser : JavaScriptActionParserBase<IqlStringLengthExpression>
    {
        protected string Separator = ".";

        public override IqlExpression ToQueryString(IqlStringLengthExpression action,
            JavaScriptIqlParserInstance parser)
        {
            return new IqlAggregateExpression(
                action.Parent,
                new IqlFinalExpression(".length"));
        }
    }
}