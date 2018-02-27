using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNetString.Parsers
{
    public class DotNetStringNotExpressionParser : DotNetStringActionParserBase<IqlNotExpression>
    {
        public override IqlExpression ToQueryString(IqlNotExpression action,
            DotNetStringIqlParserInstance parser)
        {
            var exp = parser.Parse(
                action.Expression
#if TypeScript
                        , null
#endif
            ).Expression;
            IqlExpression expression =
                new IqlFinalExpression<string>(
                    $"!({exp})"
                );
            return expression;
        }
    }
}