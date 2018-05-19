using System;
using Iql.Data.Types;
using Iql.Parsing;

namespace Iql.OData.IqlToODataExpression
{
    public class ODataIqlParserInstance : ActionParserInstance<ODataIqlData, ODataIqlExpressionAdapter, string, ODataOutput, ODataExpressionConverter>
    {
        public ODataIqlParserInstance(ODataIqlExpressionAdapter adapter, Type currentEntityType, ODataExpressionConverter expressionConverter) : base(adapter, currentEntityType, expressionConverter, new TypeResolver())
        {
        }

        public override ODataOutput ParseExpression(IqlExpression expression
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