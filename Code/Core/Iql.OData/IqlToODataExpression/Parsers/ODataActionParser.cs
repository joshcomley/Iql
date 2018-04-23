namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataActionParser : ODataActionParserBase<IqlExpression>
    {
        public override IqlExpression ToQueryString(IqlExpression action,
            ODataIqlParserInstance parser)
        {
            switch (action.Type)
            {
                case IqlExpressionType.Not:
                    return new IqlFinalExpression<string>("not");
                case IqlExpressionType.Now:
                    return new IqlFinalExpression<string>("now()");
                case IqlExpressionType.TimeSpan:
                    return new IqlAggregateExpression(new IqlFinalExpression<string>("duration'"), new IqlFinalExpression<string>((action as IqlTimeSpanExpression).ToXmlString()), new IqlFinalExpression<string>("'"));
                case IqlExpressionType.StringToLowerCase:
                    return new IqlAggregateExpression(new IqlFinalExpression<string>("tolower("), action.Parent, new IqlFinalExpression<string>(")"));
                case IqlExpressionType.StringToUpperCase:
                    return new IqlAggregateExpression(new IqlFinalExpression<string>("toupper("), action.Parent, new IqlFinalExpression<string>(")"));
                default:
                    ODataErrors.OperationNotSupported(action.Type);
                    break;
            }
            return null;
        }
    }
}