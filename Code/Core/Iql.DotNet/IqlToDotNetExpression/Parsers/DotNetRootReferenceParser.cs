using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetRootReferenceParser : DotNetActionParserBase<IqlRootReferenceExpression>
    {
        public override IqlExpression ToQueryString(IqlRootReferenceExpression action,
            DotNetIqlParserInstance parser)
        {
            IqlExpression expression =
                new IqlFinalExpression<Expression>(
                    parser.RootEntity
                );
            return expression;
        }
    }
}