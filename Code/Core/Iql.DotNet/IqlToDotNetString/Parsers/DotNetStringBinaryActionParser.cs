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
                (action.Kind == IqlExpressionKind.IsEqualTo || action.Kind == IqlExpressionKind.IsNotEqualTo) &&
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
                $"({left} {@operator} {right})");
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
            switch (action.Kind)
            {
                case IqlExpressionKind.And:
                    return "&&";
                case IqlExpressionKind.Or:
                    return "||";
                case IqlExpressionKind.IsGreaterThan:
                    return ">";
                case IqlExpressionKind.IsGreaterThanOrEqualTo:
                    return ">=";
                case IqlExpressionKind.IsLessThan:
                    return "<";
                case IqlExpressionKind.IsLessThanOrEqualTo:
                    return "<=";
                case IqlExpressionKind.IsEqualTo:
                    return "==";
                case IqlExpressionKind.IsNotEqualTo:
                    return "!=";
                case IqlExpressionKind.Modulo:
                    return "%";
                case IqlExpressionKind.Add:
                    return "+";
                case IqlExpressionKind.Subtract:
                    return "-";
                case IqlExpressionKind.Multiply:
                    return "*";
                case IqlExpressionKind.Divide:
                    return "/";
                case IqlExpressionKind.AddEquals:
                    return "+=";
                case IqlExpressionKind.SubtractEquals:
                    return "-=";
                case IqlExpressionKind.MultiplyEquals:
                    return "*=";
                case IqlExpressionKind.DivideEquals:
                    return "/=";
                case IqlExpressionKind.Has:
                    return "&";
            }
            throw new NotSupportedException(
                $"{nameof(IqlExpressionKind)} of type {action.Kind} is not supported for binary operations");

        }
    }
}