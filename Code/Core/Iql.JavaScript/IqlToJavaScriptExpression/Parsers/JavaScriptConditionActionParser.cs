namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptConditionActionParser : JavaScriptActionParserBase<IqlConditionExpression>
    {
        public override IqlExpression ToQueryString(IqlConditionExpression action, JavaScriptIqlParserContext parser)
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