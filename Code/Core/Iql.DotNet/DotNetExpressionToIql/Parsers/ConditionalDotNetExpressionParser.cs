using System.Linq.Expressions;

namespace Iql.DotNet.DotNetExpressionToIql.Parsers
{
    public class ConditionalDotNetExpressionParser<T> : DotNetExpressionParserBase<T, ConditionalExpression>
    {
        public override bool CanHandleNode(Expression node)
        {
            return node.NodeType == ExpressionType.Conditional;
        }

        public override IqlExpression PerformParse(ConditionalExpression node, DotNetExpressionParserContext context)
        {
            return new IqlConditionExpression(
                context.Parse(node.Test, context),
                context.Parse(node.IfTrue, context),
                context.Parse(node.IfFalse, context));
        }
    }
}