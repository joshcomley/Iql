using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNetString.Parsers
{
    public class DotNetStringRootReferenceParser : DotNetStringActionParserBase<IqlRootReferenceExpression>
    {
        public override IqlExpression ToQueryString(IqlRootReferenceExpression action,
            DotNetStringIqlParserContext parser)
        {
            var rootEntityName = parser.GetRootEntityName(action);
            return new IqlFinalExpression<string>(rootEntityName);
        }
    }
}