namespace Iql.DotNet.IqlToDotNetString.Parsers
{
    public class DotNetStringConditionExpressionParser : DotNetStringActionParserBase<IqlConditionExpression>
    {
        public override IqlExpression ToQueryString(IqlConditionExpression action,
            DotNetStringIqlParserInstance parser)
        {
            return new IqlAggregateExpression(
                new IqlFinalExpression<string>("("),
                action.Test,
                new IqlFinalExpression<string>("?"),
                action.IfTrue,
                new IqlFinalExpression<string>(":"),
                action.IfFalse,
                new IqlFinalExpression<string>(")")
            );
        }
    }
}