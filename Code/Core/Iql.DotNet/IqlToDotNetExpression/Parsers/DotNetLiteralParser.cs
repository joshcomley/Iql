using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetLiteralParser : DotNetActionParserBase<IqlLiteralExpression>
    {
        public override IqlExpression ToQueryString(IqlLiteralExpression action,
            DotNetIqlParserInstance parser)
        {
            IqlExpression expression =
                new IqlFinalExpression<Expression>(
                    Expression.Constant(action.Value)
                );
            return expression;
        }
    }
}