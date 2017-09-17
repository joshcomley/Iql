using Iql.Parsing;

namespace Iql.OData.Parsers
{
    public class ODataUnaryActionParser : ActionParser<IqlUnaryExpression, ODataIqlData, ODataIqlExpressionAdapter>
    {
        public override IqlExpression ToQueryString(IqlUnaryExpression action,
            ActionParserInstance<ODataIqlData, ODataIqlExpressionAdapter> parser)
        {
            return new IqlParenthesisExpression(
                new IqlAggregateExpression(
                    new IqlFinalExpression(ResolveOperator(action)),
                    new IqlFinalExpression(action.Value.ToString())
                )
            );
        }

        public string ResolveOperator(IqlUnaryExpression action)
        {
            switch (action.Type)
            {
                case IqlExpressionType.UnarySubtract:
                    return "-";
                default:
                    ODataErrors.OperationNotSupported(action.Type);
                    break;
            }
            return null;
        }
    }
}