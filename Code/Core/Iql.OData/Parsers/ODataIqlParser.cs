using Iql.OData.Queryable;
using Iql.Parsing;

namespace Iql.OData.Parsers
{
    public static class ODataIqlParser
    {
        public static string GetOData(IqlExpression iql, EvaluateContext evaluateContext)
        {
            var adapter = new ODataIqlExpressionAdapter();
            var parser = new ODataIqlParserInstance(adapter);
            parser.IsFilter = true;
            var javascriptExpression = parser.Parse(iql
#if TypeScript
                , evaluateContext
#endif
                );
            return javascriptExpression.ToCodeString();
            //;
            //var javascript = new JavaScriptExpression(adapter.RootVariableName, javascriptExpression);
            //return javascript;
        }
    }
}