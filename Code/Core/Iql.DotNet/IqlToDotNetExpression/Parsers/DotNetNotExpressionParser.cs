using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetNotExpressionParser : DotNetActionParserBase<IqlNotExpression>
    {
        public override IqlExpression ToQueryString(IqlNotExpression action,
            DotNetIqlParserContext parser)
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