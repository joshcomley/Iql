namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptStringLengthParser : JavaScriptActionParserBase<IqlStringLengthExpression>
    {
        protected string Separator = ".";

        public override IqlExpression ToQueryString(IqlStringLengthExpression action,
            JavaScriptIqlParserInstance parser)
        {
            return new IqlAggregateExpression(
                action.Parent,
                new IqlFinalExpression<string>(".length"));
        }
    }
}