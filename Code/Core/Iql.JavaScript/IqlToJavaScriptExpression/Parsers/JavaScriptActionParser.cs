namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptActionParser : JavaScriptActionParserBase<IqlExpression>
    {
        public override IqlExpression ToQueryString(IqlExpression action,
            JavaScriptIqlParserInstance parser)
        {
            switch (action.Kind)
            {
                case IqlExpressionKind.StringToUpperCase:
                case IqlExpressionKind.StringToLowerCase:
                case IqlExpressionKind.StringTrim:
                    return new JavaScriptStringSourceActionParser().ToQueryString(action, parser);
                case IqlExpressionKind.Not:
                    return new IqlFinalExpression<string>("not");
            }
            JavaScriptErrors.OperationNotSupported(action.Kind);
            return null;
        }
    }
}