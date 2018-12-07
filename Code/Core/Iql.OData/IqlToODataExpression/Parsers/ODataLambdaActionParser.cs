namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataLambdaActionParser : ODataActionParserBase<IqlLambdaExpression>
    {
        public override IqlExpression ToQueryString(IqlLambdaExpression action, ODataIqlParserContext parser)
        {
            return new IqlFinalExpression<string>(parser.Parse(action.Body).ToCodeString());
        }
    }
}