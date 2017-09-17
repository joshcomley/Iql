using Iql.Parsing;

namespace Iql.OData.Parsers
{
    public class ODataRootReferenceParser : ActionParser<IqlRootReferenceExpression, ODataIqlData,
        ODataIqlExpressionAdapter>
    {
        public override IqlExpression ToQueryString(IqlRootReferenceExpression action,
            ActionParserInstance<ODataIqlData, ODataIqlExpressionAdapter> parser)
        {
            return new IqlFinalExpression("");
        }
    }
}