using Iql.Parsing;

namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataActionParserBase<TIqlExpression> : ActionParser<
        TIqlExpression,
        ODataIqlData,
        ODataIqlExpressionAdapter,
        string,
        ODataOutput,
        ODataIqlParserInstance,
        ODataExpressionConverter>
        where TIqlExpression : IqlExpression { }
}