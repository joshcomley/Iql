using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Extensions;

namespace Iql.DotNet.DotNetExpressionToIql.Parsers
{
    public class MemberAccessDotNetExpressionParser<T> : DotNetExpressionParserBase<T, MemberExpression>
    {
        public override bool CanHandleNode(Expression node)
        {
            return node.NodeType == ExpressionType.MemberAccess;
        }

        public override IqlExpression PerformParse(MemberExpression node, DotNetExpressionParserContext context)
        {
            if (node.Expression.Type == typeof(string) && node.Member.Name == nameof(string.Length))
            {
                return new IqlStringLengthExpression(
                    context.ToIqlExpression(node.Expression) as IqlReferenceExpression);
            }

            if (node.Expression.Type.IsEnumerableType() &&
                (node.Member.Name == nameof(Enumerable.Count) || node.Member.Name == nameof(Enumerable.LongCount)
                                                              || node.Member.Name == nameof(Array.Length) ||
                                                              node.Member.Name == nameof(Array.LongLength)))
            {
                var path = context.ToIqlExpression(node.Expression) as IqlReferenceExpression;
                return new IqlCountExpression(path.GetRootEntity().VariableName, path, null);
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
                //throw new Exception(string.Format("Attempting to access member of type other than source type: {0}",
                //    node.Expression.Type.Name));
            }
                var iqlPropertyExpression = new IqlPropertyExpression(
                node.Member.Name,
                (IqlReferenceExpression)context.ToIqlExpression(node.Expression),
                node.Member.ReflectedType.ToIqlType());
            return iqlPropertyExpression;
        }
    }
}