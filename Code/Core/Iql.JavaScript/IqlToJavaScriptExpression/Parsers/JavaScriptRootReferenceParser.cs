namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptRootReferenceParser : JavaScriptActionParserBase<IqlRootReferenceExpression>
    {
        public override IqlExpression ToQueryString(IqlRootReferenceExpression action,
            JavaScriptIqlParserInstance parser)
        {
            return new IqlFinalExpression<string>(parser.Adapter.RootVariableName);
        }
    }
}