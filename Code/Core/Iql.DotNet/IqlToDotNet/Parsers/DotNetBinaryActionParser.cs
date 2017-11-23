using System;
using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNet.Parsers
{
    public class DotNetBinaryActionParser : DotNetActionParserBase<IqlBinaryExpression>
    {
        public static bool IsString(IqlExpression expression)
        {
            var literalExpression = expression as IqlLiteralExpression;
            if (literalExpression != null)
            {
                return literalExpression.ReturnType == IqlType.String;
            }
            var propertyExpression = expression as IqlPropertyExpression;
            return propertyExpression?.ReturnType == IqlType.String;
        }

        public override IqlExpression ToQueryString(
            IqlBinaryExpression action,
            DotNetIqlParserInstance parser)
        {
            var isStringComparison =
                (action.Type == IqlExpressionType.IsEqualTo || action.Type == IqlExpressionType.IsNotEqualTo) &&
                (IsString(action.Left) || IsString(action.Right));
            var left = parser.Parse(action.Left).Expression;
            var right = parser.Parse(action.Right).Expression;
            if (isStringComparison && !parser.Data.AlreadyCoalesced.Contains(action))
            {
                left = CoalesceOrUpperCase(left);
                right = CoalesceOrUpperCase(right);
            }
            return new IqlFinalExpression<Expression>(
                Expression.MakeBinary(ResolveOperator(action), left, right));
        }

        static Expression CoalesceOrUpperCase(Expression expression)
        {
            if (expression is ConstantExpression)
            {
                return Expression.Constant(((expression as ConstantExpression).Value as string).ToUpper());
            }
            return Expression.Condition(Expression.Equal(expression, Expression.Constant(null)), expression, Expression.Call(expression, nameof(string.ToUpper), new Type[] { }));
        }

        public ExpressionType ResolveOperator(IqlBinaryExpression action)
        {
            switch (action.Type)
            {
                case IqlExpressionType.And:
                    return ExpressionType.AndAlso;
                case IqlExpressionType.Or:
                    return ExpressionType.OrElse;
                case IqlExpressionType.IsGreaterThan:
                    return ExpressionType.GreaterThan;
                case IqlExpressionType.IsGreaterThanOrEqualTo:
                    return ExpressionType.GreaterThanOrEqual;
                case IqlExpressionType.IsLessThan:
                    return ExpressionType.LessThan;
                case IqlExpressionType.IsLessThanOrEqualTo:
                    return ExpressionType.LessThanOrEqual;
                case IqlExpressionType.IsEqualTo:
                    return ExpressionType.Equal;
                case IqlExpressionType.IsNotEqualTo:
                    return ExpressionType.NotEqual;
                case IqlExpressionType.Modulo:
                    return ExpressionType.Modulo;
                case IqlExpressionType.Add:
                    return ExpressionType.Add;
                case IqlExpressionType.Subtract:
                    return ExpressionType.Subtract;
                case IqlExpressionType.Multiply:
                    return ExpressionType.Multiply;
                case IqlExpressionType.Divide:
                    return ExpressionType.Divide;
                case IqlExpressionType.AddEquals:
                    return ExpressionType.AddAssign;
                case IqlExpressionType.SubtractEquals:
                    return ExpressionType.SubtractAssign;
                case IqlExpressionType.MultiplyEquals:
                    return ExpressionType.MultiplyAssign;
                case IqlExpressionType.DivideEquals:
                    return ExpressionType.DivideAssign;
                case IqlExpressionType.BitwiseAnd:
                    return ExpressionType.And;
            }
            throw new NotSupportedException(
                $"{nameof(IqlExpressionType)} of type {action.Type} is not supported for binary operations");
            
        }
    }
}