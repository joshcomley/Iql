using Iql.JavaScript.QueryToJavaScript;
using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public static class JavaScriptIqlParser
    {
        public static JavaScriptExpression GetJavaScript(IqlExpression iql
#if TypeScript
            , EvaluateContext evaluateContext
#endif
            )
        {
            var adapter = new JavaScriptIqlExpressionAdapter("entity");
            var parser = new ActionParserInstance<JavaScriptIqlData, JavaScriptIqlExpressionAdapter>(adapter);
            parser.IsFilter = true;
            var javascriptExpression = parser.Parse(iql
#if TypeScript
                , evaluateContext
#endif
                );
            var javascript = new JavaScriptExpression(adapter.RootVariableName, javascriptExpression);
            return javascript;
        }
    }
}