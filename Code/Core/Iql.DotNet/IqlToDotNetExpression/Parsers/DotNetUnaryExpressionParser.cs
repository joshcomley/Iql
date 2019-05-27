using System;
using System.Linq.Expressions;
using Iql.Serialization;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetUnaryExpressionParser : DotNetActionParserBase<IqlUnaryExpression>
    {
        public override IqlExpression ToQueryString(IqlUnaryExpression action,
            DotNetIqlParserContext parser)
        {
            switch (action.Kind)
            {
                case IqlExpressionKind.UnarySubtract:
                    var value = action.Value.ClaimsToBeIql()
                        ? parser.Parse((IqlExpression) action.Value)
                        : parser.Parse(new IqlLiteralExpression(action.Value));
                    var unaryExpression = Expression.Negate(value.Expression);
                    return new IqlFinalExpression<Expression>(unaryExpression);
            }
            throw new NotImplementedException();
        }
    }
}