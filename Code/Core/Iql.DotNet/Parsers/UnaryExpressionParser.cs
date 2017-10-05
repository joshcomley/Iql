using System;
using System.Linq.Expressions;

namespace Iql.DotNet.Parsers
{
    public class UnaryExpressionParser<T> : ExpressionParserBase<T, UnaryExpression>
    {
        public override bool CanHandleNode(Expression node)
        {
            return node.NodeType == ExpressionType.Not || node.NodeType == ExpressionType.Convert;
        }

        public override IqlExpression PerformParse(UnaryExpression node, ExpressionParserContext context)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Not:
                    return new IqlNotExpression(
                        context.Parse(node.Operand, context)
                    );
                    case ExpressionType.Convert:
                        return context.Parse(node.Operand, context);
            }
            throw new NotImplementedException();
        }
    }
}