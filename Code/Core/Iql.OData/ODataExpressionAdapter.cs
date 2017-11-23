using Iql.OData.Parsers;
using Iql.Parsing;

namespace Iql.OData
{
    public class ODataExpressionAdapter : ODataIqlParserInstance
    {
        public ODataExpressionAdapter() : base(new ODataIqlExpressionAdapter())
        {
        }
    }
}