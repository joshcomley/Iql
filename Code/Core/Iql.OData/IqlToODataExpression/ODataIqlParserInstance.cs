using System;
using Iql.Parsing;

namespace Iql.OData.IqlToODataExpression
{
    public class ODataIqlParserInstance : ActionParserInstance<ODataIqlData, ODataIqlExpressionAdapter, string, ODataOutput, ODataExpressionConverter>
    {
        public ODataIqlParserInstance(ODataIqlExpressionAdapter adapter, Type rootEntityType, ODataExpressionConverter expressionConverter) : base(adapter, rootEntityType, expressionConverter)
        {
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