using Iql.OData.Queryable;
using Iql.Parsing;

namespace Iql.OData.Parsers
{
    public static class ODataIqlParser
    {
        public static string GetOData(IqlExpression iql, EvaluateContext evaluateContext)
        {
            var adapter = new ODataIqlExpressionAdapter();
            var parser = new ActionParserInstance<ODataIqlData, ODataIqlExpressionAdapter>(adapter);
            parser.IsFilter = true;
            var javascriptExpression = parser.Parse(iql, evaluateContext);
            return javascriptExpression;
            //;
            //var javascript = new JavaScriptExpression(adapter.RootVariableName, javascriptExpression);
            //return javascript;
        }
    }
}