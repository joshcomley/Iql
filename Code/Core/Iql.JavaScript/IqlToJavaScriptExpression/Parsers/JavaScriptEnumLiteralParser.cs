namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptEnumLiteralParser : JavaScriptActionParserBase<IqlEnumLiteralExpression>
    {
        public override IqlExpression ToQueryString(IqlEnumLiteralExpression action, JavaScriptIqlParserContext parser)
        {
            long? value = null;
            if (action.Value != null)
            {
                foreach (var item in action.Value)
                {
                    value = value ?? 0;
                    value = value | item.Value;
                }
            }
            IqlExpression expression =
                new IqlFinalExpression<string>(
                    value == null ? "null" : value.ToString()
                );
            return expression;
        }
    }
}