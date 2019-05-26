using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Iql.Entities.Permissions;
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
                case nameof(string.ToUpper):
                case nameof(string.ToUpperInvariant):
                    parent = context.Parse(node.Object, context) as IqlReferenceExpression;
                    return new IqlStringToUpperCaseExpression(
                        parent);
                case nameof(string.ToLower):
                case nameof(string.ToLowerInvariant):
                    parent = context.Parse(node.Object, context) as IqlReferenceExpression;
                    return new IqlStringToLowerCaseExpression(
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
                case nameof(string.IndexOf):
                    parent = context.Parse(node.Object, context) as IqlReferenceExpression;
                    return new IqlStringIndexOfExpression(
                        parent,
                        context.Parse(node.Arguments[0], context) as IqlReferenceExpression);
                case nameof(string.StartsWith):
                    parent = context.Parse(node.Object, context) as IqlReferenceExpression;
                    return new IqlStringStartsWithExpression(
                        parent,
                        context.Parse(node.Arguments[0], context) as IqlReferenceExpression);
                case nameof(string.EndsWith):
                    parent = context.Parse(node.Object, context) as IqlReferenceExpression;
                    return new IqlStringEndsWithExpression(
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
                case nameof(Enumerable.Count):
                    if (node.Arguments.Count == 1)
                    {
                        var countParent = context.Parse(node.Arguments[0], context) as IqlFilterExpression;
                        var iqlCountExpression = new IqlCountExpression(
                            countParent.RootVariableName,
                            countParent.Parent as IqlReferenceExpression, 
                            countParent.Value
                        );
                        return iqlCountExpression;
                    }
                    return new IqlCountExpression(
                        (node.Arguments[1] as LambdaExpression).Parameters[0].Name,
                        context.Parse(node.Arguments[0], context) as IqlReferenceExpression,
                        context.Parse(node.Arguments[1], context) as IqlLambdaExpression
                    );
                case nameof(Enumerable.Where):
                    return new IqlFilterExpression(
                        context.Parse(node.Arguments[0], context) as IqlReferenceExpression,
                        context.Parse(node.Arguments[1], context) as IqlLambdaExpression
                    );
                case nameof(Enumerable.Any):
                    return new IqlIsGreaterThanExpression(
                        new IqlCountExpression(
                            (node.Arguments[1] as LambdaExpression).Parameters[0].Name, 
                            context.Parse(node.Arguments[0], context) as IqlReferenceExpression,
                            context.Parse(node.Arguments[1], context) as IqlLambdaExpression),
                        new IqlLiteralExpression(0, IqlType.Integer));
                case nameof(Enumerable.All):
                    return new IqlAllExpression((node.Arguments[1] as LambdaExpression).Parameters[0].Name, context.Parse(node.Arguments[0], context) as IqlReferenceExpression,
                        context.Parse(node.Arguments[1], context) as IqlLambdaExpression);
                case nameof(IqlPointExpression.Intersects):
                    parent = context.Parse(node.Object, context) as IqlReferenceExpression;
                    var iqlIntersectsExpression = new IqlIntersectsExpression(parent, context.Parse(node.Arguments[0], context) as IqlReferenceExpression);
                    return iqlIntersectsExpression;
                case nameof(IqlPointExpression.DistanceFrom):
                    parent = context.Parse(node.Object, context) as IqlReferenceExpression;
                    var distanceExpression = new IqlDistanceExpression(parent, context.Parse(node.Arguments[0], context) as IqlReferenceExpression);
                    return distanceExpression;
                case nameof(IqlLineExpression.Length):
                    parent = context.Parse(node.Object, context) as IqlReferenceExpression;
                    var lineExpression = new IqlLengthExpression(parent);
                    return lineExpression;
                case nameof(IqlParsingObjectExtensions.NullPropagate):
                    parent = context.Parse(node.Arguments[0], context) as IqlReferenceExpression;
                    var propertyExpression = new IqlPropertyExpression(node.Arguments[1].GetValue() as string, parent);
                    return propertyExpression;
                case nameof(IqlUserPermissionContext<object>.QueryAny):
                case nameof(IqlUserPermissionContext<object>.QueryAll):
                case nameof(IqlUserPermissionContext<object>.QueryCount):
                    var lambda = context.Parse(node.Arguments[0], context);
                    var entityTypeName = ((node.Arguments[0] as UnaryExpression).Operand as LambdaExpression).Parameters[0].Type.Name;
                    var query = new IqlDataSetQueryExpression(entityTypeName);
                    query.Filter = lambda;
                    query.Key = node.Method.Name;
                    return query;

            }
            throw new NotImplementedException();
        }
    }
}