using Iql.OData.Parsers;
using Iql.Parsing;

namespace Iql.OData
{
    public class ODataExpressionAdapter : ActionParserInstance<ODataIqlData, ODataIqlExpressionAdapter>
    {
        public ODataExpressionAdapter() : base(new ODataIqlExpressionAdapter())
        {
        }
    }
}