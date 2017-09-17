using Iql.Parsing;

namespace Iql.OData.Parsers
{
    public class ODataActionParser : ActionParser<IqlExpression, ODataIqlData, ODataIqlExpressionAdapter>
    {
        public override IqlExpression ToQueryString(IqlExpression action,
            ActionParserInstance<ODataIqlData, ODataIqlExpressionAdapter> parser)
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