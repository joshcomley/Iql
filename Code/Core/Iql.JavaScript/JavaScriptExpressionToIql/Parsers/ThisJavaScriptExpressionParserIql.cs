using System;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Parsers
{
    public class ThisJavaScriptExpressionParserIql<T> : IqlQueryJavaScriptExpressionParser<T,
        ThisJavaScriptExpressionNode>
        where T : class
    {
        public override IqlParseResult Parse(JavaScriptExpressionNodeParseContext<T> context, ThisJavaScriptExpressionNode expression)
        {
            return new IqlParseResult(new IqlVariableExpression("this"));
        }
    }
}