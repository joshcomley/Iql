namespace Iql.DotNet.IqlToDotNetString.Parsers
{
    public class DotNetStringEnumLiteralParser : DotNetStringActionParserBase<IqlEnumLiteralExpression>
    {
        public override IqlExpression ToQueryString(
            IqlEnumLiteralExpression action,
            DotNetStringIqlParserContext parser)
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