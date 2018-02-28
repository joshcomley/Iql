using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;

namespace Iql.JavaScript.JavaScriptExpressionToIql
{
    public interface IJavaScriptExpressionAdapterBase
    {
        IExpressionParserBase
            ResolveParser<TExpression>(TExpression expression)
            where TExpression : JavaScriptExpressionNode;
    }
}