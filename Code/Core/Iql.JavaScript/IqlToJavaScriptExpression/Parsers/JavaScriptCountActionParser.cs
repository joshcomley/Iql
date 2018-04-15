namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptCountActionParser : JavaScriptActionParserBase<IqlCountExpression>
    {
        public override IqlExpression ToQueryString(
            IqlCountExpression action,
            JavaScriptIqlParserInstance parser)
        {
            return new IqlParenthesisExpression(
                new IqlAggregateExpression(
                    action.Parent,
                    new IqlFinalExpression<string>($".filter(function({parser.GetRootEntityParameterName(action.RootVariableName)}) {{ return "),
                    action.Value,
                    new IqlFinalExpression<string>("; }).length")
                )
            );
        }
    }
}