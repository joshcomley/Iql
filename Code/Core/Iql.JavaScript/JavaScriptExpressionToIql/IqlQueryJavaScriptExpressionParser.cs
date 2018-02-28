using System;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree;

namespace Iql.JavaScript.JavaScriptExpressionToIql
{
    public abstract class IqlQueryJavaScriptExpressionParser<TEntity, TExpression>
        : IJavaScriptExpressionParser<TEntity, TExpression, IqlParseResult, JavaScriptToIqlExpressionData, IqlExpression
        >
        where TExpression : JavaScriptExpressionNode where TEntity : class
    {
        public abstract IqlParseResult Parse(
            JavaScriptExpressionNodeParseContext<TEntity, TExpression> context);

        public IExpressionParseResultBase Parse(IExpressionParserInstance instance)
        {
            throw new NotImplementedException();
        }
    }
}