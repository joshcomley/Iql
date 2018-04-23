namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataPropertyReferenceParser : ODataActionParserBase<IqlPropertyExpression>
    {
        protected string Separator { get; } = "/";

        public override IqlExpression ToQueryString(IqlPropertyExpression action,
            ODataIqlParserInstance parser)
        {
            var property = new IqlFinalExpression<string>(action.PropertyName);
            if (action.Parent != null)
            {
                if (action.Parent is IqlRootReferenceExpression && parser.Nested)
                {
                    return property;
                }
                return new IqlAggregateExpression(action.Parent, new IqlFinalExpression<string>(Separator), property);
            }
            return property;
        }
    }
}