using System;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Operators;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Parsers
{
    public class BinaryJavaScriptExpressionParserIql<T> : IqlQueryJavaScriptExpressionParser<T,
        BinaryJavaScriptExpressionNode>
        where T : class
    {
        public override IqlParseResult Parse(
            JavaScriptExpressionNodeParseContext<T, BinaryJavaScriptExpressionNode> context)
        {
            var left = context.ParseLeft().Value;
            var right = context.ParseRight().Value;
            IqlExpression exp = null;
            switch (context.Expression.Operator)
            {
                case OperatorType.And:
                    exp = new IqlAndExpression(left, right);
                    break;
                case OperatorType.Or:
                    exp = new IqlOrExpression(left, right);
                    break;
                case OperatorType.GreaterThan:
                    exp = new IqlIsGreaterThanExpression(left, right);
                    break;
                case OperatorType.GreaterThanOrEqualTo:
                    exp = new IqlIsGreaterThanOrEqualToExpression(left, right);
                    break;
                case OperatorType.LessThan:
                    exp = new IqlIsLessThanExpression(left, right);
                    break;
                case OperatorType.LessThanOrEqualTo:
                    exp = new IqlIsLessThanOrEqualToExpression(left, right);
                    break;
                case OperatorType.Assign:
                    exp = new IqlAssignExpression(left, right);
                    break;
                case OperatorType.EqualsEqualsEquals:
                case OperatorType.EqualsEquals:
                    exp = new IqlIsEqualToExpression(left, right);
                    break;
                case OperatorType.NotEquals:
                case OperatorType.NotEqualsEquals:
                    exp = new IqlIsNotEqualToExpression(left, right);
                    break;
                case OperatorType.Modulo:
                    exp = new IqlModuloExpression(left, right);
                    break;
                case OperatorType.Add:
                    exp = new IqlAddExpression(left, right);
                    break;
                case OperatorType.Subtract:
                    exp = new IqlSubtractExpression(left, right);
                    break;
                case OperatorType.AddEquals:
                    exp = new IqlAddEqualsExpression(left, right);
                    break;
                case OperatorType.SubtractEquals:
                    exp = new IqlSubtractEqualsExpression(left, right);
                    break;
                case OperatorType.BitwiseOr:
                    exp = new IqlBitwiseOrExpression(left, right);
                    break;
                case OperatorType.BitwiseAnd:
                    exp = new IqlBitwiseAndExpression(left, right);
                    break;
                case OperatorType.BitwiseNot:
                    exp = new IqlBitwiseNotExpression(left, right);
                    break;
            }
            if (exp == null)
            {
                throw new Exception("No parser found for JavaScript binary expression operator: " +
                                    OperatorMap.OperatorTypes.ResolveName(context.Expression.Operator) + " (" +
                                    OperatorMap.OperatorTypes.ResolveDescription(
                                        $"{context.Expression.Operator})"));
            }
            return new IqlParseResult(
                exp
            );
        }
    }
}