using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNet.Parsers
{
    public class DotNetNotExpressionParser : DotNetActionParserBase<IqlNotExpression>
    {
        public override IqlExpression ToQueryString(IqlNotExpression action,
            DotNetIqlParserInstance parser)
        {
            IqlExpression expression =
                new IqlFinalExpression<Expression>(
                    Expression.Not(parser.Parse(action.Expression
#if TypeScript
                        , null
#endif
                    ).Expression)
                );
            return expression;
        }
    }
}