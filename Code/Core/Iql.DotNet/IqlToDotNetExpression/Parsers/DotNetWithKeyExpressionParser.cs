using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetWithKeyExpressionParser : DotNetActionParserBase<IqlWithKeyExpression>
    {
        public override IqlExpression ToQueryString(IqlWithKeyExpression action,
            DotNetIqlParserContext parser)
        {
            Expression exp = null;
            foreach (var keyItem in action.KeyEqualToExpressions)
            {
                var parsed = parser.Parse(keyItem).Expression;
                if (exp == null)
                {
                    exp = parsed;
                }
                else
                {
                    exp = Expression.And(exp, parsed);
                }
            }
            IqlExpression expression =
                new IqlFinalExpression<Expression>(exp);
            return expression;
        }
    }
}