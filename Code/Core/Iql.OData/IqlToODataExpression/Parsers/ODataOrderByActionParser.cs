namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataOrderByActionParser : ODataActionParserBase<IqlOrderByExpression>
    {
        public override IqlExpression ToQueryString(IqlOrderByExpression action, ODataIqlParserContext parser)
        {
            var orderBy = parser.Parse(action.OrderExpression).ToCodeString();
            if (action.Descending)
            {
                orderBy = $"{orderBy} desc";
            }
            return new IqlFinalExpression<string>(
                orderBy);
        }
    }
}