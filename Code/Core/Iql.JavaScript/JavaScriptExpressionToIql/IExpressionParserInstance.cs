using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;

namespace Iql.JavaScript.JavaScriptExpressionToIql
{
    public interface IExpressionParserInstance
    {
        JavaScriptExpressionNode Expression { get; set; }
        IJavaScriptExpressionAdapterBase Adapter { get; set; }
    }
}