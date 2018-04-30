using System;
using Iql.Parsing;

namespace Iql.OData.IqlToODataExpression
{
    public class ODataIqlParserInstance : ActionParserInstance<ODataIqlData, ODataIqlExpressionAdapter, string, ODataOutput, ODataExpressionConverter>
    {
        public bool Nested { get; set; }

        public ODataIqlParserInstance(ODataIqlExpressionAdapter adapter, Type rootEntityType, ODataExpressionConverter expressionConverter) : base(adapter, rootEntityType, expressionConverter)
        {
        }

        public string ParseAsStringNested(IqlExpression expression)
        {
            var wasNested = Nested;
            Nested = true;
            var result = ParseAsString(expression);
            Nested = wasNested;
            return result;
        }

        public ODataOutput ParseNested(IqlExpression expression)
        {
            var wasNested = Nested;
            Nested = true;
            var result = Parse(expression);
            Nested = wasNested;
            return result;
        }

        public override ODataOutput Parse(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            return new ODataOutput(ParseAsString(expression
#if TypeScript
            , evaluateContext
#endif
                ));
        }
    }
}