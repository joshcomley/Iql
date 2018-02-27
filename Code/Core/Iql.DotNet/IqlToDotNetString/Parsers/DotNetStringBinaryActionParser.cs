using System;

namespace Iql.DotNet.IqlToDotNetString.Parsers
{
    public class DotNetStringBinaryActionParser : DotNetStringActionParserBase<IqlBinaryExpression>
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
            DotNetStringIqlParserInstance parser)
        {
            var isStringComparison =
                (action.Type == IqlExpressionType.IsEqualTo || action.Type == IqlExpressionType.IsNotEqualTo) &&
                (IsString(action.Left) || IsString(action.Right));
            var left = parser.Parse(action.Left
#if TypeScript
                , null
#endif
            ).Expression;
            var right = parser.Parse(action.Right
#if TypeScript
                , null
#endif
            ).Expression;

            var @operator = ResolveOperator(action);
            if (isStringComparison && !parser.Data.AlreadyCoalesced.Contains(action))
            {
                left = CoalesceOrUpperCase(left);
                right = CoalesceOrUpperCase(right);
            }

            return new IqlFinalExpression<string>(
                $"{left} {@operator} {right}");
        }

        static string CoalesceOrUpperCase(string expression)
        {
            if (expression == "null")
            {
                return expression;
            }
            return $"({expression} == null ? null : {expression}.ToUpper())";
        }

        public string ResolveOperator(IqlBinaryExpression action)
        {
            switch (action.Type)
            {
                case IqlExpressionType.And:
                    return "&&";
                case IqlExpressionType.Or:
                    return "||";
                case IqlExpressionType.IsGreaterThan:
                    return ">";
                case IqlExpressionType.IsGreaterThanOrEqualTo:
                    return ">=";
                case IqlExpressionType.IsLessThan:
                    return "<";
                case IqlExpressionType.IsLessThanOrEqualTo:
                    return "<=";
                case IqlExpressionType.IsEqualTo:
                    return "==";
                case IqlExpressionType.IsNotEqualTo:
                    return "!=";
                case IqlExpressionType.Modulo:
                    return "%";
                case IqlExpressionType.Add:
                    return "+";
                case IqlExpressionType.Subtract:
                    return "-";
                case IqlExpressionType.Multiply:
                    return "*";
                case IqlExpressionType.Divide:
                    return "/";
                case IqlExpressionType.AddEquals:
                    return "+=";
                case IqlExpressionType.SubtractEquals:
                    return "-=";
                case IqlExpressionType.MultiplyEquals:
                    return "*=";
                case IqlExpressionType.DivideEquals:
                    return "/=";
                case IqlExpressionType.BitwiseAnd:
                    return "&";
            }
            throw new NotSupportedException(
                $"{nameof(IqlExpressionType)} of type {action.Type} is not supported for binary operations");

        }
    }
}