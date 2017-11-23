using Iql.Parsing;

namespace Iql.OData.Parsers
{
    public class ODataActionParserBase<TIqlExpression> : ActionParser<TIqlExpression, ODataIqlData,
        ODataIqlExpressionAdapter, string, ODataOutput, ODataIqlParserInstance> where TIqlExpression : IqlExpression
    {
        
    }
}