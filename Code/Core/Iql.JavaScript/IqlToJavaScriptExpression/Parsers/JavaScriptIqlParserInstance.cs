using Iql.JavaScript.JavaScriptExpressionToIql;
using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptIqlParserInstance : ActionParserInstance<JavaScriptIqlData, JavaScriptIqlExpressionAdapter, string, JavaScriptOutput, JavaScriptExpressionConverter>
    {
        public JavaScriptIqlParserInstance(JavaScriptIqlExpressionAdapter adapter, JavaScriptExpressionConverter expressionConverter) : base(adapter, null, expressionConverter)
        {
        }

        public override JavaScriptOutput Parse(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            return new JavaScriptOutput(ParseAsString(expression
#if TypeScript
            , evaluateContext
#endif
                ));
        }
    }
}