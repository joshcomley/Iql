namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptActionParser : JavaScriptActionParserBase<IqlExpression>
    {
        public override IqlExpression ToQueryString(IqlExpression action,
            JavaScriptIqlParserInstance parser)
        {
            switch (action.Type)
            {
                case IqlExpressionType.StringToUpperCase:
                case IqlExpressionType.StringToLowerCase:
                case IqlExpressionType.StringTrim:
                    return new JavaScriptStringSourceActionParser().ToQueryString(action, parser);
                case IqlExpressionType.Not:
                    return new IqlFinalExpression("not");
            }
            JavaScriptErrors.OperationNotSupported(action.Type);
            return null;
        }
    }
}