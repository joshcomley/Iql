using Iql.JavaScript.QueryableApplicator;
using Iql.Parsing;
#if TypeScript

#endif

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
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
            var parser = new JavaScriptIqlParserInstance(adapter);
            parser.IsFilter = true;
            var javascriptExpression = parser.Parse(iql
#if TypeScript
                , evaluateContext
#endif
                );
            var javascript = new JavaScriptExpression(adapter.RootVariableName, javascriptExpression.ToCodeString());
            return javascript;
        }
    }
}