using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetLiteralParser : DotNetActionParserBase<IqlLiteralExpression>
    {
        public override IqlExpression ToQueryString(IqlLiteralExpression action,
            DotNetIqlParserContext parser)
        {
            if (action.Value is IqlExpression)
            {
                var exp = action.Value as IqlExpression;
                if (exp.Kind == IqlExpressionKind.Now)
                {
                    var value = parser.Parse(action.Value as IqlExpression).Expression;
                    var result = Expression.Lambda(value).Compile().DynamicInvoke();
                    action.Value = result;
                }
            }
            IqlExpression expression =
                new IqlFinalExpression<Expression>(
                    Expression.Constant(action.Value)
                );
            return expression;
        }
    }
}