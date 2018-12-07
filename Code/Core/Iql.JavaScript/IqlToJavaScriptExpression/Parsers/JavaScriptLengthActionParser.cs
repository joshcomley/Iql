namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptLengthActionParser : JavaScriptActionParserBase<IqlLengthExpression>
    {
        public override IqlExpression ToQueryString(IqlLengthExpression action, JavaScriptIqlParserContext parser)
        {
            var line = parser.Parse(action.Parent).ToCodeString();
            return new IqlFinalExpression<string>(
                $"({line} != null && {line}).{nameof(IqlLineExpression.Length)}()");
        }
    }
}