using Iql.JavaScript.JavaScriptExpressionToExpressionTree;

namespace Iql.JavaScript.JavaScriptExpressionToIql
{
    public interface IJavaScriptExpressionAdapterBase
    {
        IExpressionParserBase
            ResolveParser<TExpression>(TExpression expression)
            where TExpression : JavaScriptExpressionNode;
    }
}