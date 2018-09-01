using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Parsers
{
    public class ConditionalJavaScriptExpressionParserIql<T> : IqlQueryJavaScriptExpressionParser<T,
        ConditionalJavaScriptExpressionNode>
        where T : class
    {
        public override IqlParseResult Parse(JavaScriptExpressionNodeParseContext<T> context, ConditionalJavaScriptExpressionNode expression)
        {
            return new IqlParseResult(new IqlConditionExpression(
                context.Parse(expression.Test).Value,
                context.Parse(expression.Consequent).Value,
                context.Parse(expression.Alternate).Value));
        }
    }
}