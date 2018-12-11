using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetSpecialValueParser : DotNetActionParserBase<IqlSpecialValueExpression>
    {
        public override IqlExpression ToQueryString(IqlSpecialValueExpression action, DotNetIqlParserContext parser)
        {
            if (action.Kind == IqlExpressionKind.Now)
            {
                IqlExpression expression =
                    new IqlFinalExpression<Expression>(
                        Expression.Property(null, typeof(DateTimeOffset).GetProperty(nameof(DateTimeOffset.Now), BindingFlags.Static | BindingFlags.Public))
                    );
                return expression;
            }
            return action;
        }
    }
}