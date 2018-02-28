using Iql.Parsing;

namespace Iql.OData.IqlToODataExpression
{
    public class ODataIqlParserInstance : ActionParserInstance<ODataIqlData, ODataIqlExpressionAdapter, string, ODataOutput>
    {
        public ODataIqlParserInstance(ODataIqlExpressionAdapter adapter) : base(adapter, null)
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