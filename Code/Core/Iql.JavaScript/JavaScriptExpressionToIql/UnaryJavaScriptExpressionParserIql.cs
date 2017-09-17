using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToIql.Expressions;
using Iql.JavaScript.JavaScriptExpressionToIql.Expressions.JavaScript;

namespace Iql.JavaScript.JavaScriptExpressionToIql
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