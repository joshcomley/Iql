using System;
using System.Linq;
using System.Linq.Expressions;
using Iql.DotNet.Extensions;

namespace Iql.DotNet.DotNetExpressionToIql.Parsers
{
    public class BinaryDotNetExpressionParser<T> : DotNetExpressionParserBase<T, BinaryExpression>
        {
        public override bool CanHandleNode(Expression node)
        {
            var supportedTypes = new[]
            {
                ExpressionType.Equal,
                ExpressionType.NotEqual,
                ExpressionType.GreaterThan,
                ExpressionType.GreaterThanOrEqual,
                ExpressionType.LessThan,
                ExpressionType.LessThanOrEqual,
                ExpressionType.Or, ExpressionType.OrElse,
                ExpressionType.And, ExpressionType.AndAlso,
                ExpressionType.Add, ExpressionType.AddAssign,
                ExpressionType.Subtract, ExpressionType.SubtractAssign,
                ExpressionType.Divide, ExpressionType.DivideAssign,
                ExpressionType.Multiply, ExpressionType.MultiplyAssign,
                ExpressionType.Modulo, ExpressionType.ModuloAssign
            };
            return supportedTypes.Contains(node.NodeType);
        }

        public override IqlExpression PerformParse(BinaryExpression node, DotNetExpressionParserContext context)
        {
            //Action<Expression, Expression> ctor;
            var type = ResolveType(node.NodeType);
            var binary = (IqlBinaryExpression) Activator.CreateInstance(type);
            binary.Left = context.ToIqlExpression(node.Left);
            binary.Right = context.ToIqlExpression(node.Right);

            bool CheckEnum(Expression sideA, IqlExpression sideB)
            {
                if (sideA is UnaryExpression && (sideA as UnaryExpression).Operand.Type.IsEnumOrNullableEnum() && sideB is IqlLiteralExpression)
                {
                    var value = Convert.ToInt64((sideB as IqlLiteralExpression).Value);
                    binary.Right = new IqlEnumLiteralExpression((sideA as UnaryExpression).Operand.Type)
                        .AddValue(value, "");
                    return true;
                }

                return false;
            }

            if (node.NodeType == ExpressionType.Equal)
            {
                if (!CheckEnum(node.Left, binary.Right))
                {
                    CheckEnum(node.Right, binary.Left);
                }
            }
            return binary;
        }

        private Type ResolveType(ExpressionType nodeType)
        {
            switch (nodeType)
            {
                case ExpressionType.Equal:
                    return typeof(IqlIsEqualToExpression);
                case ExpressionType.NotEqual:
                    return typeof(IqlIsNotEqualToExpression);
                case ExpressionType.GreaterThan:
                    return typeof(IqlIsGreaterThanExpression);
                case ExpressionType.GreaterThanOrEqual:
                    return typeof(IqlIsGreaterThanOrEqualToExpression);
                case ExpressionType.LessThan:
                    return typeof(IqlIsLessThanExpression);
                case ExpressionType.LessThanOrEqual:
                    return typeof(IqlIsLessThanOrEqualToExpression);
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return typeof(IqlOrExpression);
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return typeof(IqlAndExpression);
                case ExpressionType.Add:
                    return typeof(IqlAddExpression);
                case ExpressionType.AddAssign:
                    return typeof(IqlAddEqualsExpression);
                case ExpressionType.Subtract:
                    return typeof(IqlSubtractExpression);
                case ExpressionType.SubtractAssign:
                    return typeof(IqlSubtractEqualsExpression);
                case ExpressionType.Divide:
                    return typeof(IqlDivideExpression);
                case ExpressionType.DivideAssign:
                    return typeof(IqlDivideEqualsExpression);
                case ExpressionType.Multiply:
                    return typeof(IqlMultiplyExpression);
                case ExpressionType.MultiplyAssign:
                    return typeof(IqlMultiplyEqualsExpression);
                case ExpressionType.Modulo:
                    return typeof(IqlModuloExpression);
                case ExpressionType.ModuloAssign:
                    return typeof(IqlModuloEqualsExpression);
            }
            throw new NotSupportedException($"Unsupported expression type: {nodeType}");
        }
    }
}