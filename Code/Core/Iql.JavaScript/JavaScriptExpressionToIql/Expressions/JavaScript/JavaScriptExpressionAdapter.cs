using System;
using System.Collections.Generic;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Expressions.JavaScript
{
    public class JavaScriptExpressionAdapter<
            TEntity,
            TParseResult,
            TExpressionData,
            TExpressionResult>
        : IJavaScriptExpressionAdapter<TEntity, TParseResult, TExpressionData, TExpressionResult>
        where TParseResult : class, IExpressionParseResultBase
        where TEntity : class
        where TExpressionData : class
        where TExpressionResult : class
    {
        private readonly Dictionary<string, Func<IExpressionParserBase>> _parsers =
            new Dictionary<string, Func<IExpressionParserBase>>();

        public virtual TExpressionData NewData()
        {
            throw new NotImplementedException();
        }

        public virtual IJavaScriptExpressionParser<TEntity, TExpression, TParseResult, TExpressionData,
                TExpressionResult>
            ResolveParser<TExpression>(TExpression expression)
            where TExpression : JavaScriptExpressionNode
        {
            var fn = _parsers[expression.GetType().Name];
            if (fn == null)
            {
                return null;
            }
            return fn() as IJavaScriptExpressionParser<TEntity, TExpression, TParseResult, TExpressionData,
                TExpressionResult>;
        }

        public virtual void RegisterParser<TExpression>(
            Func<IJavaScriptExpressionParser<TEntity, TExpression, TParseResult, TExpressionData, TExpressionResult>>
                resolver)
            where TExpression : JavaScriptExpressionNode
        {
            _parsers[typeof(TExpression).Name] = resolver;
        }

        IExpressionParserBase IJavaScriptExpressionAdapterBase.ResolveParser<TExpression>(TExpression expression)
        {
            return ResolveParser(expression);
        }
    }
}