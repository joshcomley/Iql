namespace Iql.OData.Parsers
{
    public class ODataPropertyReferenceParser : ODataActionParserBase<IqlPropertyExpression>
    {
        protected string Separator { get; } = "/";

        public override IqlExpression ToQueryString(IqlPropertyExpression action,
            ODataIqlParserInstance parser)
        {
            var property = new IqlFinalExpression<string>(action.PropertyName);
            if (action.Parent != null && action.Parent.Type != IqlExpressionType.RootReference)
            {
                return new IqlAggregateExpression(action.Parent, new IqlFinalExpression<string>(Separator), property);
            }
            return property;
        }
    }
}