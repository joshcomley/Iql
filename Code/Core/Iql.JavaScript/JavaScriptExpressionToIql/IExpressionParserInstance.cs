using Iql.JavaScript.JavaScriptExpressionToExpressionTree;

namespace Iql.JavaScript.JavaScriptExpressionToIql
{
    public interface IExpressionParserInstance
    {
        JavaScriptExpressionNode Expression { get; set; }
        IJavaScriptExpressionAdapterBase Adapter { get; set; }
    }
}