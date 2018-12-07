namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptCountActionParser : JavaScriptActionParserBase<IqlCountExpression>
    {
        public override IqlExpression ToQueryString(
            IqlCountExpression action,
            JavaScriptIqlParserContext parser)
        {
            return new IqlParenthesisExpression(
                new IqlAggregateExpression(
                    action.Parent,
                    new IqlFinalExpression<string>(
                        $".filter(function({parser.GetRootEntityParameterName(action.RootVariableName)}) {{ return "),
                    new IqlFinalExpression<string>(parser.Parse(action.Value).ToCodeString()),
                    new IqlFinalExpression<string>("; }).length")
                )
            );
        }
    }
}