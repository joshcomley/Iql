using System.Linq.Expressions;
using Iql.Extensions;

namespace Iql.DotNet.Parsers
{
    public class ConstantExpressionParser<T> : ExpressionParserBase<T, ConstantExpression>
    {
        public override bool CanHandleNode(Expression node)
        {
            return node.NodeType == ExpressionType.Constant;
        }

        public override IqlExpression PerformParse(ConstantExpression node, ExpressionParserContext context)
        {
            return new IqlLiteralExpression(node.Value, node.Type.ToIqlType());
        }
    }
}