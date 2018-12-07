using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetExpandExpressionParser : DotNetActionParserBase<IqlExpandExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlExpandExpression action, DotNetIqlParserContext parser)
        {
            return new IqlFinalExpression<Expression>(
                parser.Chain<TEntity>(null, e => e.Expand(action)));
        }
    }
}