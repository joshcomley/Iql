using System;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Expressions.JavaScript
{
    public interface IJavaScriptExpressionAdapter<TEntity, TParseResult, TExpressionData, TExpressionResult>
        : IJavaScriptExpressionAdapterBase
        where TParseResult : class, IExpressionParseResultBase
        where TEntity : class
        where TExpressionData : class
        where TExpressionResult : class
    {
        void RegisterParser<TExpression>(
            Func<IJavaScriptExpressionParser<TEntity, TExpression, TParseResult, TExpressionData, TExpressionResult>>
                resolver)
            where TExpression : JavaScriptExpressionNode;

        new IJavaScriptExpressionParser<TEntity, TExpression, TParseResult, TExpressionData, TExpressionResult>
            ResolveParser<TExpression>(TExpression expression)
            where TExpression : JavaScriptExpressionNode;

        TExpressionData NewData();
    }
}