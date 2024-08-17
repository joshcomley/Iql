namespace Iql.DotNet.IqlToDotNetString.Parsers
{
    public class DotNetStringLiteralParser : DotNetStringActionParserBase<IqlLiteralExpression>
    {
        public override IqlExpression ToQueryString(
            IqlLiteralExpression action,
            DotNetStringIqlParserContext parser)
        {
            var value = action.Value == null ? "null" : action.Value.ToString();
            if (action.Value != null)
            {
                if (action.ReturnType == IqlType.String)
                {
                    value = $@"""{value}""";
                }
                else if (action.ReturnType == IqlType.Guid)
                {
                    value = $@"new System.Guid(""{value}"")";
                }
                else
                {
                    value = value.ToLower();
                }
            }
            IqlExpression expression =
                new IqlFinalExpression<string>(
                    value
                );
            return expression;
        }
    }
}