using System;
using Iql.Extensions;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Parsers
{
    public class LiteralJavaScriptExpressionParserIql<T> : IqlQueryJavaScriptExpressionParser<T,
        LiteralJavaScriptExpressionNode>
        where T : class
    {
        public override IqlParseResult Parse(
            JavaScriptExpressionNodeParseContext<T, LiteralJavaScriptExpressionNode> context)
        {
            IqlExpression exp = null;
            // If we have a parent, then:
            // .. if the value is a number, then we're indexing an array
            // .. if the value is a string, then we're indexing a property
            if (context.ObjectStack().Count > 0)
            {
                var type = context.Expression.Value.GetType();
                if (type.Name == "string")
                {
                    exp = new IqlPropertyExpression(context.Expression.Value.ToString());
                }
                else if (type.Name == "number")
                {
                    throw new Exception("Indexing arrays not yet implemented.");
                }
            }
            else
            {
                exp = new IqlLiteralExpression(context.Expression.Value,
                    context.Expression.Value?.GetType().ToIqlType() ?? IqlType.Unknown);
            }
            return new IqlParseResult(exp);
        }
    }
}