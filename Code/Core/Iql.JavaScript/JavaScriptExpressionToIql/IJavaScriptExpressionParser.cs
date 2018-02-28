using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;

namespace Iql.JavaScript.JavaScriptExpressionToIql
{
    public interface IJavaScriptExpressionParser<
            TEntity,
            TExpression,
            TParseResult,
            TExpressionData,
            TExpressionResult>
        : IExpressionParserBase
        where TExpression : JavaScriptExpressionNode
        where TParseResult : class, IExpressionParseResultBase
        where TEntity : class
        where TExpressionData : class
        where TExpressionResult : class
    {
        TParseResult Parse(
            JavaScriptExpressionNodeParseContext<TEntity, TExpression> context);
    }
}