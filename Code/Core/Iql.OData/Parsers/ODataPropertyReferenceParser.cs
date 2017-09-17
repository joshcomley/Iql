using Iql.Parsing;

namespace Iql.OData.Parsers
{
    public class ODataPropertyReferenceParser : ActionParser<IqlPropertyExpression, ODataIqlData,
        ODataIqlExpressionAdapter>
    {
        protected string Separator { get; } = "/";

        public override IqlExpression ToQueryString(IqlPropertyExpression action,
            ActionParserInstance<ODataIqlData, ODataIqlExpressionAdapter> parser)
        {
            var property = new IqlFinalExpression(action.PropertyName);
            if (action.Parent != null && action.Parent.Type != IqlExpressionType.RootReference)
            {
                return new IqlAggregateExpression(action.Parent, new IqlFinalExpression(Separator), property);
            }
            return property;
        }
    }
}