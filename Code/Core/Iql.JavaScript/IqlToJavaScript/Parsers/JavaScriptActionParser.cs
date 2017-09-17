using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptActionParser : ActionParser<IqlExpression, JavaScriptIqlData, JavaScriptIqlExpressionAdapter>
    {
        public override IqlExpression ToQueryString(IqlExpression action,
            ActionParserInstance<JavaScriptIqlData, JavaScriptIqlExpressionAdapter> parser)
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