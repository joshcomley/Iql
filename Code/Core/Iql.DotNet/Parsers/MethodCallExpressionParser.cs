using System.Linq.Expressions;
using Iql.Extensions;
using Iql.Parsing.Extensions;

namespace Iql.DotNet.Parsers
{
    public class MethodCallExpressionParser<T> : ExpressionParserBase<T, MethodCallExpression>
    {
        public override bool CanHandleNode(Expression node)
        {
            return node.NodeType == ExpressionType.Call;
        }

        public override IqlExpression PerformParse(MethodCallExpression node, ExpressionParserContext context)
        {
            return new IqlLiteralExpression(node.GetValue(), node.Method.ReturnType.ToIqlType());
        }
    }
}