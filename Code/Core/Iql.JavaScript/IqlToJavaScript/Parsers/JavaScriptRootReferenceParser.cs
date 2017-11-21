using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptRootReferenceParser : JavaScriptActionParserBase<IqlRootReferenceExpression>
    {
        public override IqlExpression ToQueryString(IqlRootReferenceExpression action,
            JavaScriptIqlParserInstance parser)
        {
            return new IqlFinalExpression(parser.Adapter.RootVariableName);
        }
    }
}