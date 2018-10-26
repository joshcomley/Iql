using System;
using System.Linq;
using Iql.Entities;
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
                                if (binary.Operator == OperatorType.GreaterThan &&
                                    expression.Args.Count == 1 &&
                                    expression.Args[0] is LambdaJavaScriptExpressionNode)
                                {
                                    var calleeIql = context.Parse((expression.Callee as MemberJavaScriptExpressionNode).Owner);
                                    var currentRootEntityType = context.RootEntities.Last().Type;
                                    Type newRootEntityType = null;
                                    if (currentRootEntityType != null && calleeIql.Value is IqlPropertyExpression)
                                    {
                                        var currentRootEntityConfiguration = 
                                            EntityConfigurationBuilder
                                            .FindConfigurationForEntityType(currentRootEntityType);
                                        if (currentRootEntityConfiguration != null)
                                        {
                                            var path = IqlPropertyPath.FromPropertyExpression(
                                                currentRootEntityConfiguration,
                                                calleeIql.Value as IqlPropertyExpression);
                                            if (path != null)
                                            {
                                                newRootEntityType = path.Property.TypeDefinition.ElementType;
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
                                        lambdaFunc);
                                    //lambdaFunc.Parent = countExpression;
                                    countExpression.Parent = calleeIql.Value;
                                    method = new IqlIsGreaterThanExpression(
                                        countExpression,
                                        context.Parse(binary.Right).Value);
                                    break;
                                }
                                else if (
                                   (binary.Operator == OperatorType.EqualsEquals ||
                                    binary.Operator == OperatorType.EqualsEqualsEquals))
                                {
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
                throw new Exception("Method not supported in IQL: " + nativeMethodName);
            }
            return new IqlParseResult(method);
        }
    }
}