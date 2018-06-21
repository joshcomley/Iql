namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataActionParser : ODataActionParserBase<IqlExpression>
    {
        public override IqlExpression ToQueryString(IqlExpression action,
            ODataIqlParserInstance parser)
        {
            switch (action.Kind)
            {
                case IqlExpressionKind.Not:
                    return new IqlAggregateExpression(new IqlFinalExpression<string>("not("), (action as IqlNotExpression).Expression, new IqlFinalExpression<string>(")"));
                case IqlExpressionKind.Now:
                    return new IqlFinalExpression<string>("now()");
                case IqlExpressionKind.TimeSpan:
                    return new IqlAggregateExpression(new IqlFinalExpression<string>("duration'"), new IqlFinalExpression<string>((action as IqlTimeSpanExpression).ToXmlString()), new IqlFinalExpression<string>("'"));
                case IqlExpressionKind.StringToLowerCase:
                    return new IqlAggregateExpression(new IqlFinalExpression<string>("tolower("), action.Parent, new IqlFinalExpression<string>(")"));
                case IqlExpressionKind.StringToUpperCase:
                    return new IqlAggregateExpression(new IqlFinalExpression<string>("toupper("), action.Parent, new IqlFinalExpression<string>(")"));
                default:
                    ODataErrors.OperationNotSupported(action.Kind);
                    break;
            }
            return null;
        }
    }
}