using System;
using System.Collections.Generic;
using Iql.Extensions;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;

namespace Iql.JavaScript.JavaScriptExpressionToIql
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
            ResolveParserInternal<TExpression>(TExpression expression)
            where TExpression : JavaScriptExpressionNode
        {
            var fn = _parsers[expression.GetType().Name];
            if (fn == null)
            {
                return null;
            }

            var expressionParserBase = fn();
            var javaScriptExpressionParser = expressionParserBase as IJavaScriptExpressionParser<TEntity, TExpression, TParseResult, TExpressionData,
                TExpressionResult>;
            return javaScriptExpressionParser;
        }

        IExpressionParserBase IJavaScriptExpressionAdapterBase.ResolveParser(JavaScriptExpressionNode expression)
        {
            return (IExpressionParserBase) GetType().GetMethod(nameof(ResolveParserInternal))
                .InvokeGeneric(this, new object[]
                    {
                        expression
                    },
                    expression.GetType());
        }

        public virtual void RegisterParser<TExpression>(
            Func<IJavaScriptExpressionParser<TEntity, TExpression, TParseResult, TExpressionData, TExpressionResult>>
                resolver)
            where TExpression : JavaScriptExpressionNode
        {
            _parsers[typeof(TExpression).Name] = resolver;
        }
    }
}