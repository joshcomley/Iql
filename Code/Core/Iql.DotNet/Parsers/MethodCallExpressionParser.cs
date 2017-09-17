using System;
using System.Linq;
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
                case nameof(string.IsNullOrEmpty):
                case nameof(string.IsNullOrWhiteSpace):
                    parent = context.Parse(node.Arguments.Single(), context) as IqlReferenceExpression;
                    IqlExpression emptyCheck = parent;
                    if (node.Method.Name == nameof(string.IsNullOrWhiteSpace))
                    {
                        emptyCheck = new IqlStringTrimExpression(
                            parent);
                    }
                    return new IqlOrExpression(
                        new IqlIsEqualToExpression(
                            emptyCheck,
                            new IqlLiteralExpression("", IqlType.String)
                        ),
                        new IqlIsEqualToExpression(
                            emptyCheck,
                            new IqlLiteralExpression(null, IqlType.String)
                        )
                    );
            }
            throw new NotImplementedException();
        }
    }
}