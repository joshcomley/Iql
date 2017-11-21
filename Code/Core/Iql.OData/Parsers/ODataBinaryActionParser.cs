using Iql.Parsing;

namespace Iql.OData.Parsers
{
    public class ODataBinaryActionParser : ODataActionParserBase<IqlBinaryExpression>
    {
        public override IqlExpression ToQueryString(IqlBinaryExpression action,
            ODataIqlParserInstance parser)
        {
            var spacer = " ";
            return new IqlParenthesisExpression(
                new IqlAggregateExpression(
                    action.Left,
                    new IqlFinalExpression(spacer),
                    new IqlFinalExpression(ResolveOperator(action)),
                    new IqlFinalExpression(spacer),
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
                case IqlExpressionType.BitwiseAnd:
                    return "has";
                default:
                    ODataErrors.OperationNotSupported(action.Type);
                    break;
            }
            return null;
        }
    }
}