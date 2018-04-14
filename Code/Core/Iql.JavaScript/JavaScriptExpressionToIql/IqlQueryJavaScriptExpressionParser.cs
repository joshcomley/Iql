using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;

namespace Iql.JavaScript.JavaScriptExpressionToIql
{
    public abstract class IqlQueryJavaScriptExpressionParser<TEntity, TExpression>
        : IJavaScriptExpressionParser<TEntity, TExpression, IqlParseResult, JavaScriptToIqlExpressionData, IqlExpression>
        where TExpression : JavaScriptExpressionNode where TEntity : class
    {
        public abstract IqlParseResult Parse(
            JavaScriptExpressionNodeParseContext<TEntity> context, TExpression expression);

        IExpressionParseResultBase IExpressionParserBase.Parse(IExpressionParserInstance instance, JavaScriptExpressionNode expression)
        {
            return Parse((JavaScriptExpressionNodeParseContext<TEntity>) instance, (TExpression) expression);
        }
    }
}