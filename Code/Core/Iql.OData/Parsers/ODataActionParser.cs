namespace Iql.OData.Parsers
{
    public class ODataActionParser : ODataActionParserBase<IqlExpression>
    {
        public override IqlExpression ToQueryString(IqlExpression action,
            ODataIqlParserInstance parser)
        {
            switch (action.Type)
            {
                case IqlExpressionType.Not:
                    return new IqlFinalExpression("not");
                default:
                    ODataErrors.OperationNotSupported(action.Type);
                    break;
            }
            return null;
        }
    }
}