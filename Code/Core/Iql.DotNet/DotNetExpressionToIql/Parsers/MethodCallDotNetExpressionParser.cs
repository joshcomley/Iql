using System;
using System.Linq;
using System.Linq.Expressions;
using Iql.Extensions;
using Iql.Parsing.Extensions;

namespace Iql.DotNet.DotNetExpressionToIql.Parsers
{
    public class MethodCallDotNetExpressionParser<T> : DotNetExpressionParserBase<T, MethodCallExpression>
    {
        public override bool CanHandleNode(Expression node)
        {
            return node.NodeType == ExpressionType.Call;
        }

        public override IqlExpression PerformParse(MethodCallExpression node, DotNetExpressionParserContext context)
        {
            if (!context.ContainsRoot(node))
            {
                return new IqlLiteralExpression(node.GetValue(), node.Method.ReturnType.ToIqlType());
            }
            IqlReferenceExpression parent;
            switch (node.Method.Name)
            {
                case nameof(string.Trim):
                    parent = context.Parse(node.Object, context) as IqlReferenceExpression;
                    return new IqlStringTrimExpression(
                        parent);
                case nameof(string.Substring):
                    parent = context.Parse(node.Object, context) as IqlReferenceExpression;
                    return new IqlStringSubStringExpression(
                        parent,
                        context.Parse(node.Arguments[0], context) as IqlReferenceExpression,
                        node.Arguments.Count == 2 ? context.Parse(node.Arguments[1], context) as IqlReferenceExpression : null);
                case nameof(string.Contains):
                    parent = context.Parse(node.Object, context) as IqlReferenceExpression;
                    return new IqlStringIncludesExpression(
                        parent,
                        context.Parse(node.Arguments[0], context) as IqlReferenceExpression);
                case nameof(string.IsNullOrEmpty):
                case nameof(string.IsNullOrWhiteSpace):
                    parent = context.Parse(node.Arguments.Single(), context) as IqlReferenceExpression;
                    IqlExpression emptyCheck = parent;
                    if (node.Method.Name == nameof(string.IsNullOrWhiteSpace))
                    {
                        emptyCheck = new IqlStringTrimExpression(
                            parent);
                    }
                    // s.Title == null || s.Title.Trim() == ""
                    return new IqlOrExpression(
                        new IqlIsEqualToExpression(
                            parent,
                            new IqlLiteralExpression(null, IqlType.String)
                        ),
                        new IqlIsEqualToExpression(
                            emptyCheck,
                            new IqlLiteralExpression("", IqlType.String)
                        )
                    );
            }
            throw new NotImplementedException();
        }
    }
}