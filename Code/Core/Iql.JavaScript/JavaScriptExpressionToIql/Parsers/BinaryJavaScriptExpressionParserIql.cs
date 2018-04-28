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
            JavaScriptExpressionNodeParseContext<T> context, BinaryJavaScriptExpressionNode expression)
        {
            var leftResult = context.ParseLeft();
            if (leftResult.ReplaceParent)
            {
                return new IqlParseResult(leftResult.Value);
            }
            var left = leftResult.Value;
            if (left.Parent is IqlAnyAllExpression)
            {
                return new IqlParseResult(left.Parent);
            }
            // If the javascript has been minified, we might end up with an expression like this:
            // function(t) { return 1 == t.SomeBooleanField; }
            var right = context.ParseRight().Value;
            if (left.ReturnType == IqlType.Boolean || right.ReturnType == IqlType.Boolean)
            {
                CheckBoolean(left);
                CheckBoolean(right);
            }
            IqlExpression exp = null;
            switch (expression.Operator)
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
                case OperatorType.Multiply:
                    exp = new IqlMultiplyExpression(left, right);
                    break;
                case OperatorType.Divide:
                    exp = new IqlDivideExpression(left, right);
                    break;
                case OperatorType.AddEquals:
                    exp = new IqlAddEqualsExpression(left, right);
                    break;
                case OperatorType.SubtractEquals:
                    exp = new IqlSubtractEqualsExpression(left, right);
                    break;
                case OperatorType.MultiplyEquals:
                    exp = new IqlMultiplyEqualsExpression(left, right);
                    break;
                case OperatorType.DivideEquals:
                    exp = new IqlDivideEqualsExpression(left, right);
                    break;
                case OperatorType.BitwiseOr:
                    exp = new IqlBitwiseOrExpression(left, right);
                    break;
                case OperatorType.Has:
                    exp = new IqlHasExpression(left, right);
                    break;
                case OperatorType.BitwiseNot:
                    exp = new IqlBitwiseNotExpression(left, right);
                    break;
            }
            if (exp == null)
            {
                throw new Exception("No parser found for JavaScript binary expression operator: " +
                                    OperatorMap.OperatorTypes.ResolveName(expression.Operator) + " (" +
                                    OperatorMap.OperatorTypes.ResolveDescription(
                                        $"{expression.Operator})"));
            }
            return new IqlParseResult(
                exp
            );
        }

        private static void CheckBoolean(IqlExpression right)
        {
            var iqlExpression = right;
            if (iqlExpression is IqlLiteralExpression)
            {
                var lit = iqlExpression as IqlLiteralExpression;
                if (Equals(null, lit.Value))
                {
                    return;
                }
                // Use ToString because in .NET the number might be decimal, int, float etc. so a
                // typed comparison would be convoluted
                if (Equals(lit.Value.ToString(), "0"))
                {
                    lit.Value = false;
                    lit.ReturnType = IqlType.Boolean;
                }
                else if (Equals(lit.Value.ToString(), "1"))
                {
                    lit.Value = true;
                    lit.ReturnType = IqlType.Boolean;
                }
            }
        }
    }
}