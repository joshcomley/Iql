using System;
using System.Linq;

namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataBinaryActionParser : ODataActionParserBase<IqlBinaryExpression>
    {
        public override IqlExpression ToQueryString(IqlBinaryExpression action,
            ODataIqlParserInstance parser)
        {
            if (action.Left is IqlPropertyExpression &&
                action.Right is IqlEnumLiteralExpression &&
                action.Kind == IqlExpressionKind.Has)
            {
                var rightLiteral = action.Right as IqlEnumLiteralExpression;
                var valueString = string.Join(",", rightLiteral.Value.Select(v => v.Name).OrderBy(o => o));
                var enumString = $"\'{valueString}\'";
                action.Right = new IqlFinalExpression<string>(
                    enumString);
            }
            else if (action.Left is IqlPropertyExpression && 
                action.Right is IqlLiteralExpression &&
                action.Kind == IqlExpressionKind.Has &&
                (action.Right.ReturnType == IqlType.Integer || (action.Right as IqlLiteralExpression).InferredReturnType == IqlType.Integer))
            {
                var type = action.Left.ResolveType(parser.CurrentEntityType);
                if (type.IsDefined(typeof(FlagsAttribute), true))
                {
                    var rightLiteral = action.Right as IqlLiteralExpression;
                    var value = Enum.ToObject(type, rightLiteral.Value);
                    var valueString = 
                        string.Join(",", value.ToString().Split(',')
                        .Select(s => s.Trim()).OrderBy(o => o));
                    var enumString = $"\'{valueString}\'";
                    action.Right = new IqlFinalExpression<string>(
                        enumString);
                }
            }
            var spacer = " ";
            return new IqlParenthesisExpression(
                new IqlAggregateExpression(
                    action.Left,
                    new IqlFinalExpression<string>($"{spacer}{ResolveOperator(action)}{spacer}"),
                    action.Right
                )
            );
        }

        public string ResolveOperator(IqlBinaryExpression action)
        {
            switch (action.Kind)
            {
                case IqlExpressionKind.And:
                    return "and";
                case IqlExpressionKind.Or:
                    return "or";
                case IqlExpressionKind.IsGreaterThan:
                    return "gt";
                case IqlExpressionKind.IsGreaterThanOrEqualTo:
                    return "ge";
                case IqlExpressionKind.IsLessThan:
                    return "lt";
                case IqlExpressionKind.IsLessThanOrEqualTo:
                    return "le";
                case IqlExpressionKind.IsEqualTo:
                    return "eq";
                case IqlExpressionKind.IsNotEqualTo:
                    return "ne";
                case IqlExpressionKind.Modulo:
                    return "mod";
                case IqlExpressionKind.Add:
                    return "add";
                case IqlExpressionKind.Subtract:
                    return "sub";
                case IqlExpressionKind.Has:
                    return "has";
                case IqlExpressionKind.Multiply:
                    return "mul";
                case IqlExpressionKind.Divide:
                    return "div";
                default:
                    ODataErrors.OperationNotSupported(action.Kind);
                    break;
            }
            return null;
        }
    }
}