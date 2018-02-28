using Iql.JavaScript.JavaScriptExpressionToExpressionTree;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Parsers
{
    public class UnaryJavaScriptExpressionParserIql<T> : IqlQueryJavaScriptExpressionParser<T,
        UnaryJavaScriptExpressionNode>
        where T : class
    {
        public override IqlParseResult Parse(
            JavaScriptExpressionNodeParseContext<T, UnaryJavaScriptExpressionNode>
                context)
        {
            var value = context.Parse(context.Expression.Argument).Value;
            switch (context.Expression.Operator)
            {
                case OperatorType.Subtract:
                    return new IqlParseResult(new IqlUnarySubtractExpression(value));
            }
            return new IqlParseResult(value);
        }
    }
}