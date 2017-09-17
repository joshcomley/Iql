using System;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToIql.Expressions;
using Iql.JavaScript.JavaScriptExpressionToIql.Expressions.JavaScript;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.JavaScript.JavaScriptExpressionToIql
{
    public class
        CallJavaScriptExpressionParserIql<T> : IqlQueryJavaScriptExpressionParser<T, CallJavaScriptExpressionNode>
        where T : class
    {
        public IqlReferenceExpression TryGetArgAt(int index,
            JavaScriptExpressionNodeParseContext<T, CallJavaScriptExpressionNode>
                context)
        {
            if (context.Expression.Args.Count <= index)
            {
                return null;
            }
            return context.Parse(context.Expression.Args[index]).Value as IqlReferenceExpression;
        }

        public override IqlParseResult Parse(
            JavaScriptExpressionNodeParseContext<T, CallJavaScriptExpressionNode>
                context)
        {
            var callee = context.Parse(context.Expression.Callee);
            var member = callee.Value as IqlReferenceExpression;
            IqlExpression method = null;
            string nativeMethodName = null;
            switch (member.Type)
            {
                case IqlExpressionType.Literal:
                    nativeMethodName = (member as IqlLiteralExpression).Value.ToString();
                    break;
                case IqlExpressionType.Property:
                    nativeMethodName = (member as IqlPropertyExpression).PropertyName;
                    break;
                default:
                    nativeMethodName = context.Reducer.Evaluate(member).Value.ToString();
                    break;
            }
            var parent = member.Parent as IqlReferenceExpression;
            switch (nativeMethodName)
            {
                case "includes":
                    method = new IqlStringIncludesExpression(
                        parent,
                        TryGetArgAt(0, context));
                    break;
                case "indexOf":
                    method = new IqlStringIndexOfExpression(parent,
                        context.Parse(context.Expression.Args[0]).Value as IqlReferenceExpression);
                    break;
                case "endsWith":
                    method = new IqlStringEndsWithExpression(parent,
                        context.Parse(context.Expression.Args[0]).Value as IqlReferenceExpression);
                    break;
                case "startsWith":
                    method = new IqlStringStartsWithExpression(parent,
                        context.Parse(context.Expression.Args[0]).Value as IqlReferenceExpression);
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
                        context.Parse(context.Expression.Args[0]).Value as IqlReferenceExpression);
                    break;
                case "toString":
                    method = new IqlToStringExpression(parent);
                    break;
                case "substring":
                case "substr":
                    method = new IqlStringSubStringExpression(
                        parent,
                        TryGetArgAt(0, context),
                        TryGetArgAt(1, context)
                    );
                    break;
            }
            if (method == null)
            {
                //            debugger;
                // Check if this is a sub-tree
                // TODO: Support sub-trees
                var local = context.Reducer.Evaluate(member);
                if (local.Value is WhereQueryExpression<object>)
                {
                    return new IqlParseResult(context.ParseSubTree(local.Value as WhereQueryExpression<T>));
                }
                if (parent != null)
                {
                    local = context.Reducer.Evaluate(parent);
                    if (local.Value is WhereQueryExpression<object>)
                    {
                        return new IqlParseResult(context.ParseSubTree(local.Value as WhereQueryExpression<T>));
                    }
                }
                throw new Exception("Method not supported in IQL: " + nativeMethodName);
            }
            return new IqlParseResult(method);
        }
    }
}