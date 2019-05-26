using Iql.Serialization;

namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataUnaryActionParser : ODataActionParserBase<IqlUnaryExpression>
    {
        public override IqlExpression ToQueryString(IqlUnaryExpression action,
            ODataIqlParserContext parser)
        {
            return new IqlAggregateExpression(
                    new IqlFinalExpression<string>(ResolveOperator(action)),
                    new IqlFinalExpression<string>(action.Value.ClaimsToBeIql()
                        ? parser.Parse((IqlExpression) action.Value).ToCodeString()
                        : action.Value.ToString())
                );
        }

        public string ResolveOperator(IqlUnaryExpression action)
        {
            switch (action.Kind)
            {
                case IqlExpressionKind.UnarySubtract:
                    return "-";
                default:
                    ODataErrors.OperationNotSupported(action.Kind);
                    break;
            }
            return null;
        }
    }
}