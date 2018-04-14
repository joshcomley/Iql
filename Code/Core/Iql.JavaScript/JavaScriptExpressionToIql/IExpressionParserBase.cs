using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;

namespace Iql.JavaScript.JavaScriptExpressionToIql
{
    public interface IExpressionParserBase
    {
        IExpressionParseResultBase Parse(IExpressionParserInstance instance, JavaScriptExpressionNode expression);
    }
}