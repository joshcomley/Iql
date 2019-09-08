using System;
using Iql.Extensions;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Parsers
{
    public class LiteralJavaScriptExpressionParserIql<T> : IqlQueryJavaScriptExpressionParser<T,
        LiteralJavaScriptExpressionNode>
        where T : class
    {
        public override IqlParseResult Parse(
            JavaScriptExpressionNodeParseContext<T> context, LiteralJavaScriptExpressionNode expression)
        {
            IqlExpression exp = null;
            // If we have a parent, then:
            // .. if the value is a number, then we're indexing an array
            // .. if the value is a string, then we're indexing a property
            if(Equals(expression.Value, nameof(IqlCurrentUser)))
            {
                exp = new IqlPropertyExpression(nameof(IqlCurrentUser));
            }
            else if (context.ObjectStack().Count > 0)
            {
                var type = expression.Value.GetType();
                if (type.Name == "string")
                {
                    exp = new IqlPropertyExpression(expression.Value.ToString());
                }
                else if (type.Name == "number")
                {
                    throw new Exception("Indexing arrays not yet implemented.");
                }
            }
            else
            {
                exp = new IqlLiteralExpression(expression.Value,
                    expression.Value?.GetType().ToIqlType() ?? IqlType.Unknown);
            }
            return new IqlParseResult(exp);
        }
    }
}