using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptRootReferenceParser : ActionParser<IqlRootReferenceExpression, JavaScriptIqlData,
        JavaScriptIqlExpressionAdapter>
    {
        public override IqlExpression ToQueryString(IqlRootReferenceExpression action,
            ActionParserInstance<JavaScriptIqlData, JavaScriptIqlExpressionAdapter> parser)
        {
            return new IqlFinalExpression(parser.Adapter.RootVariableName);
        }
    }
}