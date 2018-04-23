namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataRootReferenceParser : ODataActionParserBase<IqlRootReferenceExpression>
    {
        public override IqlExpression ToQueryString(IqlRootReferenceExpression action,
            ODataIqlParserInstance parser)
        {
            var str = "";
            if (!parser.Nested)
            {
                str = "$it";
            }
            return new IqlFinalExpression<string>(str);
        }
    }
}