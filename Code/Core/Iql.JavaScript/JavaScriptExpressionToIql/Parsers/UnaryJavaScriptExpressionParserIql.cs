using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Operators;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Parsers
{
    public class UnaryJavaScriptExpressionParserIql<T> : IqlQueryJavaScriptExpressionParser<T,
        UnaryJavaScriptExpressionNode>
        where T : class
    {
        public override IqlParseResult Parse(
            JavaScriptExpressionNodeParseContext<T> context,
            UnaryJavaScriptExpressionNode expression)
        {
            var value = context.Parse(expression.Argument).Value;
            switch (expression.Operator)
            {
                case OperatorType.Subtract:
                    return new IqlParseResult(new IqlUnarySubtractExpression(value));
            }
            return new IqlParseResult(value);
        }
    }
}