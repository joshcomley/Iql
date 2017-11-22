using Iql.Parsing;

namespace Iql.OData.Parsers
{
    public class ODataIqlParserInstance : ActionParserInstance<ODataIqlData, ODataIqlExpressionAdapter, string, ODataOutput>
    {
        public ODataIqlParserInstance(ODataIqlExpressionAdapter adapter) : base(adapter, null)
        {
        }

        public override ODataOutput Parse(IqlExpression expression)
        {
            return new ODataOutput(ParseAsString(expression));
        }
    }
}