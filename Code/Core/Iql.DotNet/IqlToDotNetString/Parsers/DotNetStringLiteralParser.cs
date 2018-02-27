namespace Iql.DotNet.IqlToDotNetString.Parsers
{
    public class DotNetStringLiteralParser : DotNetStringActionParserBase<IqlLiteralExpression>
    {
        public override IqlExpression ToQueryString(
            IqlLiteralExpression action,
            DotNetStringIqlParserInstance parser)
        {
            var value = action.Value == null ? "null" : action.Value.ToString();
            if (action.Value != null && action.ReturnType == IqlType.String)
            {
                value = $@"""{value}""";
            }
            IqlExpression expression =
                new IqlFinalExpression<string>(
                    value
                );
            return expression;
        }
    }
}