namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptRootReferenceParser : JavaScriptActionParserBase<IqlRootReferenceExpression>
    {
        public override IqlExpression ToQueryString(
            IqlRootReferenceExpression action,
            JavaScriptIqlParserInstance parser)
        {
            var rootEntityName = parser.GetRootEntityName(action);
            return new IqlFinalExpression<string>(rootEntityName);
        }
    }
}