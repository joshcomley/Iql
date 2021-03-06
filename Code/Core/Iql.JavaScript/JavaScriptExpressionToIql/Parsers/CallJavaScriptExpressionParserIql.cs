using System;
using System.Linq;
using Iql.Entities;
using Iql.Entities.Permissions;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Operators;
using Iql.Parsing.Expressions.QueryExpressions;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Parsers
{
    public class
        CallJavaScriptExpressionParserIql<T> : IqlQueryJavaScriptExpressionParser<T, CallJavaScriptExpressionNode>
        where T : class
    {
        public IqlReferenceExpression TryGetArgAt(int index,
            JavaScriptExpressionNodeParseContext<T> context,
            CallJavaScriptExpressionNode expression)
        {
            if (expression.Args.Count <= index)
            {
                return null;
            }
            return context.Parse(expression.Args[index]).Value as IqlReferenceExpression;
        }

        public override IqlParseResult Parse(
            JavaScriptExpressionNodeParseContext<T> context,
            CallJavaScriptExpressionNode expression)
        {
            var callee = context.Parse(expression.Callee);
            var iqlReferenceExpression = callee.Value as IqlReferenceExpression;
            IqlExpression method = null;
            string nativeMethodName = null;
            switch (iqlReferenceExpression.Kind)
            {
                case IqlExpressionKind.Literal:
                    nativeMethodName = (iqlReferenceExpression as IqlLiteralExpression).Value.ToString();
                    break;
                case IqlExpressionKind.Property:
                    nativeMethodName = (iqlReferenceExpression as IqlPropertyExpression).PropertyName;
                    break;
                default:
                    nativeMethodName = context.Reducer.Evaluate(iqlReferenceExpression).Value.ToString();
                    break;
            }
            var parent = iqlReferenceExpression.Parent as IqlReferenceExpression;
            if (nativeMethodName == nameof(IqlCurrentUser.Get) &&
                iqlReferenceExpression.Parent != null &&
                iqlReferenceExpression.Parent.Kind == IqlExpressionKind.Property)
            {
                IIqlLiteralExpression parentValue = null;
                try
                {
                    parentValue = context.Reducer.Evaluate(parent);
                }
                catch
                {

                }
                if (parentValue != null && parentValue.Value == typeof(IqlCurrentUser))
                {
                    method = new IqlCurrentUserExpression();
                }
            }
            switch (nativeMethodName)
            {
                case nameof(IqlPointExpression.Intersects):
                    method = new IqlIntersectsExpression(parent,
                        context.Parse(expression.Args[0]).Value as IqlReferenceExpression);
                    break;
                case nameof(IqlPointExpression.DistanceFrom):
                    method = new IqlDistanceExpression(parent,
                        context.Parse(expression.Args[0]).Value as IqlReferenceExpression);
                    break;
                case nameof(IqlLineExpression.Length):
                    method = new IqlLengthExpression(parent);
                    break;
                case "includes":
                    method = new IqlStringIncludesExpression(
                        parent,
                        TryGetArgAt(0, context, expression));
                    break;
                case "indexOf":
                    method = new IqlStringIndexOfExpression(parent,
                        context.Parse(expression.Args[0]).Value as IqlReferenceExpression);
                    break;
                case "endsWith":
                    method = new IqlStringEndsWithExpression(parent,
                        context.Parse(expression.Args[0]).Value as IqlReferenceExpression);
                    break;
                case "startsWith":
                    method = new IqlStringStartsWithExpression(parent,
                        context.Parse(expression.Args[0]).Value as IqlReferenceExpression);
                    break;
                case "toUpperCase":
                case "toLocaleUpperCase":
                    method = new IqlStringToUpperCaseExpression(parent);
                    break;
                case "toLowerCase":
                case "toLocaleLowerCase":
                    method = new IqlStringToLowerCaseExpression(parent);
                    break;
                case "trim":
                    method = new IqlStringTrimExpression(parent);
                    break;
                case "concat":
                    method = new IqlStringConcatExpression(parent,
                        context.Parse(expression.Args[0]).Value as IqlReferenceExpression);
                    break;
                case "toString":
                    method = new IqlToStringExpression(parent);
                    break;
                case "substring":
                case "substr":
                    method = new IqlStringSubStringExpression(
                        parent,
                        TryGetArgAt(0, context, expression),
                        TryGetArgAt(1, context, expression)
                    );
                    break;
                case nameof(IqlUserPermissionContext<object>.QueryAny):
                case nameof(IqlUserPermissionContext<object>.QueryAll):
                case nameof(IqlUserPermissionContext<object>.QueryCount):
                    var lambda = context.Parse(expression.Args[0]).Value;
                    var entityTypeName = context.EntityType.GenericTypeArguments[0].Name;
                    var query = new IqlDataSetQueryExpression(entityTypeName);
                    query.Filter = lambda;
                    query.Key = nativeMethodName;
                    method = query;
                    break;
                case "filter":
                    if (expression.Parent is MemberJavaScriptExpressionNode)
                    {
                        var member = expression.Parent as MemberJavaScriptExpressionNode;
                        if (member.Parent is BinaryJavaScriptExpressionNode && member.Property is PropertyIdentifierJavaScriptExpressionNode)
                        {
                            var binary = member.Parent as BinaryJavaScriptExpressionNode;
                            var property = member.Property as PropertyIdentifierJavaScriptExpressionNode;
                            if (property.Name == "length")
                            {
                                if ((binary.Operator == OperatorType.EqualsEquals ||
                                     binary.Operator == OperatorType.EqualsEqualsEquals ||
                                     binary.Operator == OperatorType.NotEquals ||
                                     binary.Operator == OperatorType.NotEqualsEquals ||
                                     binary.Operator == OperatorType.GreaterThan ||
                                     binary.Operator == OperatorType.GreaterThanOrEqualTo ||
                                     binary.Operator == OperatorType.LessThan ||
                                     binary.Operator == OperatorType.LessThanOrEqualTo
                                     ) &&
                                    expression.Args.Count == 1 &&
                                    expression.Args[0] is LambdaJavaScriptExpressionNode)
                                {
                                    var calleeIql = context.Parse((expression.Callee as MemberJavaScriptExpressionNode).Owner);
                                    var currentRootEntityType = context.RootEntities.Last().Type;
                                    Type newRootEntityType = null;
                                    if (context.TypeResolver != null && currentRootEntityType != null && calleeIql.Value is IqlPropertyExpression)
                                    {
                                        var currentRootEntityConfiguration =
                                            context.TypeResolver.FindTypeByType(currentRootEntityType);
                                        if (currentRootEntityConfiguration != null)
                                        {
                                            var path = IqlPropertyPath.FromPropertyExpression(
                                                context.TypeResolver,
                                                currentRootEntityConfiguration,
                                                calleeIql.Value as IqlPropertyExpression);
                                            if (path != null)
                                            {
                                                newRootEntityType = path.Property.ElementType;
                                            }
                                        }
                                    }
                                    var lambdaJavaScriptExpressionNode = expression.Args[0] as LambdaJavaScriptExpressionNode;
                                    var lambdaFunc = context.ParseLambda(
                                        lambdaJavaScriptExpressionNode.Expression,
                                        new RootEntity(
                                            lambdaJavaScriptExpressionNode.ParameterName,
                                            newRootEntityType,
                                            calleeIql.Value as IqlPropertyExpression)).Value;
                                    var countExpression = new IqlCountExpression(lambdaJavaScriptExpressionNode.ParameterName,
                                        null,
                                        IqlLambdaExpression.Create(lambdaFunc, IqlType.Boolean, lambdaJavaScriptExpressionNode.ParameterName));
                                    //lambdaFunc.Parent = countExpression;
                                    countExpression.Parent = calleeIql.Value;
                                    var right = context.Parse(binary.Right).Value;
                                    switch (binary.Operator)
                                    {
                                        case OperatorType.EqualsEquals:
                                        case OperatorType.EqualsEqualsEquals:
                                            method = new IqlIsEqualToExpression(
                                                countExpression,
                                                right);
                                            break;
                                        case OperatorType.NotEquals:
                                        case OperatorType.NotEqualsEquals:
                                            method = new IqlIsNotEqualToExpression(
                                                countExpression,
                                                right);
                                            break;
                                        case OperatorType.LessThan:
                                            method = new IqlIsLessThanExpression(
                                                countExpression,
                                                right);
                                            break;
                                        case OperatorType.LessThanOrEqualTo:
                                            method = new IqlIsLessThanOrEqualToExpression(
                                                countExpression,
                                                right);
                                            break;
                                        case OperatorType.GreaterThan:
                                            method = new IqlIsGreaterThanExpression(
                                                countExpression,
                                                right);
                                            break;
                                        case OperatorType.GreaterThanOrEqualTo:
                                            method = new IqlIsGreaterThanOrEqualToExpression(
                                                countExpression,
                                                right);
                                            break;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    throw new NotImplementedException("Only `Any` (.filter(...).length > 0) and `All` (.filter(...).length === [source].length) are supported in JavaScript.");
            }
            if (method == null)
            {
                //            debugger;
                // Check if this is a sub-tree
                // TODO: Support sub-trees
                var local = context.Reducer.Evaluate(iqlReferenceExpression);
                if (local.Value is WhereQueryExpression)
                {
                    return new IqlParseResult(context.ParseSubTree(local.Value as WhereQueryExpression));
                }
                if (parent != null)
                {
                    local = context.Reducer.Evaluate(parent);
                    if (local.Value is WhereQueryExpression)
                    {
                        return new IqlParseResult(context.ParseSubTree(local.Value as WhereQueryExpression));
                    }
                }
                throw new Exception("Method not supported in IQL for JavaScript: " + nativeMethodName);
            }
            return new IqlParseResult(method);
        }
    }
}