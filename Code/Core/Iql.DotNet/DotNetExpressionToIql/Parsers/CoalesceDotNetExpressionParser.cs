using System.Linq.Expressions;

namespace Iql.DotNet.DotNetExpressionToIql.Parsers
{
    public class CoalesceDotNetExpressionParser<T> : DotNetExpressionParserBase<T, BinaryExpression>
    {
        public override bool CanHandleNode(Expression node)
        {
            return node.NodeType == ExpressionType.Coalesce;
        }

        public override IqlExpression PerformParse(BinaryExpression node, DotNetExpressionParserContext context)
        {
            var left = context.Parse(node.Left, context);
            var right = context.Parse(node.Right, context);
            return new IqlConditionExpression(
                new IqlIsEqualToExpression(
                    left,
                    new IqlLiteralExpression(null, left.ReturnType)),
                right,
                left
            );
        }
    }
}