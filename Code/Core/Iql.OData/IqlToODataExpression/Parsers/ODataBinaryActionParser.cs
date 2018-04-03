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
                action.Type == IqlExpressionType.Has)
            {
                var rightLiteral = action.Right as IqlEnumLiteralExpression;
                var valueString = string.Join(",", rightLiteral.Value.Select(v => v.Name).OrderBy(o => o));
                var enumString = $"\'{valueString}\'";
                action.Right = new IqlFinalExpression<string>(
                    enumString);
            }
            else if (action.Left is IqlPropertyExpression && 
                action.Right is IqlLiteralExpression &&
                action.Type == IqlExpressionType.Has &&
                (action.Right.ReturnType == IqlType.Integer || (action.Right as IqlLiteralExpression).InferredReturnType == IqlType.Integer))
            {
                var type = action.Left.ResolveType(parser.RootEntityType);
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
            switch (action.Type)
            {
                case IqlExpressionType.And:
                    return "and";
                case IqlExpressionType.Or:
                    return "or";
                case IqlExpressionType.IsGreaterThan:
                    return "gt";
                case IqlExpressionType.IsGreaterThanOrEqualTo:
                    return "ge";
                case IqlExpressionType.IsLessThan:
                    return "lt";
                case IqlExpressionType.IsLessThanOrEqualTo:
                    return "le";
                case IqlExpressionType.IsEqualTo:
                    return "eq";
                case IqlExpressionType.IsNotEqualTo:
                    return "ne";
                case IqlExpressionType.Modulo:
                    return "mod";
                case IqlExpressionType.Add:
                    return "add";
                case IqlExpressionType.Subtract:
                    return "sub";
                case IqlExpressionType.Has:
                    return "has";
                default:
                    ODataErrors.OperationNotSupported(action.Type);
                    break;
            }
            return null;
        }
    }
}