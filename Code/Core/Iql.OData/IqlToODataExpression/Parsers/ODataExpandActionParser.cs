namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataExpandActionParser : ODataActionParserBase<IqlExpandExpression>
    {
        public override IqlExpression ToQueryString(IqlExpandExpression action, ODataIqlParserContext parser)
        {
            var expandProperty = parser.Parse(action.NavigationProperty).ToCodeString();
            if (action.Query != null)
            {
                var expandDetails = parser.Parse(action.Query).ToCodeString();
                if (!string.IsNullOrWhiteSpace(expandDetails))
                {
                    expandProperty = $"{expandProperty}({expandDetails})";
                }
            }

            if (action.Count)
            {
                expandProperty = $"{expandProperty}/$count";
            }

            if (parser.Data.Expands.ContainsKey(expandProperty))
            {
                return null;
            }
            parser.Data.Expands.Add(expandProperty, expandProperty);
            return new IqlFinalExpression<string>(
                expandProperty);
        }
    }
}