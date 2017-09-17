using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToIql.Expressions.JavaScript;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Expressions
{
    public interface IExpressionParserInstance
    {
        JavaScriptExpressionNode Expression { get; set; }
        IJavaScriptExpressionAdapterBase Adapter { get; set; }
    }
}