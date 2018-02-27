namespace Iql.DotNet.IqlToDotNetString.Parsers
{
    public class DotNetStringStringTrimExpressionParser : DotNetStringActionParserBase<IqlStringTrimExpression>
    {
        public override IqlExpression ToQueryString(IqlStringTrimExpression action,
            DotNetStringIqlParserInstance parser)
        {
            var parentExpression = parser.Parse(action.Parent
#if TypeScript
                        , null
#endif
            ).Expression;
            IqlExpression expression =
                new IqlFinalExpression<string>(
                    $"{parentExpression}.Trim()"
                );
            return expression;
        }
    }
}