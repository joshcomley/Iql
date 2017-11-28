using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptIqlParserInstance : ActionParserInstance<JavaScriptIqlData, JavaScriptIqlExpressionAdapter, string, JavaScriptOutput>
    {
        public JavaScriptIqlParserInstance(JavaScriptIqlExpressionAdapter adapter) : base(adapter, null)
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