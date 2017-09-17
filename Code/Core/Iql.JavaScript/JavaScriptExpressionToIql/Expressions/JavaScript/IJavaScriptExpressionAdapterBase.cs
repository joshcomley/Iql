using Iql.JavaScript.JavaScriptExpressionToExpressionTree;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Expressions.JavaScript
{
    public interface IJavaScriptExpressionAdapterBase
    {
        IExpressionParserBase
            ResolveParser<TExpression>(TExpression expression)
            where TExpression : JavaScriptExpressionNode;
    }
}