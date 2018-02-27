using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNetString.Parsers
{
    public class DotNetStringPropertyReferenceParser : DotNetStringActionParserBase<IqlPropertyExpression>
    {
        public override IqlExpression ToQueryString(IqlPropertyExpression action,
            DotNetStringIqlParserInstance parser)
        {
            var exp = parser.Parse(
                action.Parent
#if TypeScript
                        , null
#endif
            ).Expression;
            IqlExpression expression =
                new IqlFinalExpression<string>(
                    $"{exp}.{action.PropertyName}"
                );
            return expression;
        }
    }
}