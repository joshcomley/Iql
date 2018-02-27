namespace Iql.DotNet.IqlToDotNetString.Parsers
{
    public class DotNetStringStringSubStringExpressionParser : DotNetStringActionParserBase<IqlStringSubStringExpression>
    {
        public override IqlExpression ToQueryString(IqlStringSubStringExpression action,
            DotNetStringIqlParserInstance parser)
        {
            var parentExpression = parser.Parse(action.Parent
#if TypeScript
                        , null
#endif
            ).Expression;
            var valueExpression = parser.Parse(
                action.Value
#if TypeScript
                        , null
#endif
            ).Expression;

            string methodCallExpression;
            if (action.Take != null)
            {
                var takeExpression = parser.Parse(
                    action.Take
#if TypeScript
                        , null
#endif
                ).Expression;
                methodCallExpression = $"{parentExpression}.Substring({valueExpression}, {takeExpression})";
            }
            else
            {
                methodCallExpression = $"{parentExpression}.Substring({valueExpression})";
            }
            IqlExpression expression =
                new IqlFinalExpression<string>(
                    methodCallExpression
                );
            return expression;
        }
    }
}