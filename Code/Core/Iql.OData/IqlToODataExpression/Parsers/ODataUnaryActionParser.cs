namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataUnaryActionParser : ODataActionParserBase<IqlUnaryExpression>
    {
        public override IqlExpression ToQueryString(IqlUnaryExpression action,
            ODataIqlParserInstance parser)
        {
            return new IqlParenthesisExpression(
                new IqlAggregateExpression(
                    new IqlFinalExpression<string>(ResolveOperator(action)),
                    new IqlFinalExpression<string>(action.Value.ToString())
                )
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