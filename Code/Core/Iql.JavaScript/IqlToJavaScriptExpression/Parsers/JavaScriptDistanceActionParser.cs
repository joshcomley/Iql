namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptDistanceActionParser : JavaScriptActionParserBase<IqlDistanceExpression>
    {
        public override IqlExpression ToQueryString(IqlDistanceExpression action, JavaScriptIqlParserInstance parser)
        {
            var left = parser.Parse(action.Left).ToCodeString();
            var right = parser.Parse(action.Right).ToCodeString();
            return new IqlFinalExpression<string>(
                $"({left} != null && {left}).{nameof(IqlPointExpression.DistanceFrom)}({right})");
        }
    }
}