using System.Linq.Expressions;
using Iql.Extensions;

namespace Iql.DotNet.DotNetExpressionToIql.Parsers
{
    public class ConstantDotNetExpressionParser<T> : DotNetExpressionParserBase<T, ConstantExpression>
    {
        public override bool CanHandleNode(Expression node)
        {
            return node.NodeType == ExpressionType.Constant;
        }

        public override IqlExpression PerformParse(ConstantExpression node, DotNetExpressionParserContext context)
        {
            return new IqlLiteralExpression(node.Value, node.Type.ToIqlType());
        }
    }
}