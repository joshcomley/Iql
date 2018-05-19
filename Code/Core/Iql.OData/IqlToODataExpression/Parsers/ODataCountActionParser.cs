namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataCountActionParser : ODataActionParserBase<IqlCountExpression>
    {
        public override IqlExpression ToQueryString(IqlCountExpression action, ODataIqlParserInstance parser)
        {
            ODataOutput filter = null;
            if (action.Value != null)
            {
                filter = parser.Parse(action.Value);
            }

            var filterString = filter?.ToCodeString();
            if (string.IsNullOrWhiteSpace(filterString))
            {
                filterString = null;
            }
            else
            {
                filterString = $"($filter={filterString})";
            }
            var path = parser.Parse(action.Parent);
            return new IqlFinalExpression<string>($"{path.ToCodeString()}/$count{(filterString == null ? "" : filterString)}");
        }
    }
}