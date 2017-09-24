using System;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Extensions;

namespace Iql.DotNet.Parsers
{
    public class MemberAccessExpressionParser<T> : ExpressionParserBase<T, MemberExpression>
    {
        public override bool CanHandleNode(Expression node)
        {
            return node.NodeType == ExpressionType.MemberAccess;
        }

        public override IqlExpression PerformParse(MemberExpression node, ExpressionParserContext context)
        {
            if (node.Expression.Type == typeof(string) && node.Member.Name == nameof(string.Length))
            {
                return new IqlStringLengthExpression(
                    context.ToIqlExpression(node.Expression) as IqlReferenceExpression);
            }
            if (node.Expression.Type != typeof(T))
            {
                if (node.Expression is ConstantExpression)
                {
                    var field = node.Member as FieldInfo;
                    return new IqlLiteralExpression(field.GetValue((node.Expression as ConstantExpression).Value),
                        field.FieldType.ToIqlType()
                    );
                }
                throw new Exception(string.Format("Attempting to access member of type other than source type: {0}",
                    node.Expression.Type.Name));
            }
                var iqlPropertyExpression = new IqlPropertyExpression(
                node.Member.Name, "", node.Member.ReflectedType.ToIqlType());
            iqlPropertyExpression.Parent = context.ToIqlExpression(node.Expression);
            return iqlPropertyExpression;
        }
    }
}