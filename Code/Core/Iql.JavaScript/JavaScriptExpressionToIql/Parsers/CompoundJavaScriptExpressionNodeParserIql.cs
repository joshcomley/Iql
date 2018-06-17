using System;
using System.Linq;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Parsers
{
    public class
        CompoundJavaScriptExpressionNodeParserIql<T> : IqlQueryJavaScriptExpressionParser<T,
            CompoundJavaScriptExpressionNode>
        where T : class
    {
        public override IqlParseResult Parse(JavaScriptExpressionNodeParseContext<T> context, CompoundJavaScriptExpressionNode expression)
        {
            var parts = expression.Body.Where(b =>
                !(b is PropertyIdentifierJavaScriptExpressionNode &&
                  (b as PropertyIdentifierJavaScriptExpressionNode).Name == "return"))
                .ToList();
            if (parts.Count != 1)
            {
                throw new Exception("Unsupported number of body arguments in " + expression.GetType().Name);
            }

            return context.Parse(parts[0]);
        }
    }
}