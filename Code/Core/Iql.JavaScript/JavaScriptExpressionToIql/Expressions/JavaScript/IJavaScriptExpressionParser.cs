using Iql.JavaScript.JavaScriptExpressionToExpressionTree;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Expressions.JavaScript
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