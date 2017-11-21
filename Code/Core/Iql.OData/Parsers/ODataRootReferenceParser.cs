namespace Iql.OData.Parsers
{
    public class ODataRootReferenceParser : ODataActionParserBase<IqlRootReferenceExpression>
    {
        public override IqlExpression ToQueryString(IqlRootReferenceExpression action,
            ODataIqlParserInstance parser)
        {
            return new IqlFinalExpression<string>("");
        }
    }
}