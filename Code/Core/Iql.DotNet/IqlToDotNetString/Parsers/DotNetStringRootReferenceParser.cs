using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNetString.Parsers
{
    public class DotNetStringRootReferenceParser : DotNetStringActionParserBase<IqlRootReferenceExpression>
    {
        public override IqlExpression ToQueryString(IqlRootReferenceExpression action,
            DotNetStringIqlParserInstance parser)
        {
            IqlExpression expression =
                new IqlFinalExpression<string>(
                    parser.RootVariableName
                );
            return expression;
        }
    }
}