using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetEnumLiteralParser : DotNetActionParserBase<IqlEnumLiteralExpression>
    {
        public override IqlExpression ToQueryString(
            IqlEnumLiteralExpression action,
            DotNetIqlParserContext parser)
        {
            long? num = null;
            if (action.Value != null)
            {
                foreach (var item in action.Value)
                {
                    num = num ?? 0;
                    num = num | item.Value;
                }
            }
            IqlExpression expression =
                new IqlFinalExpression<Expression>(
                    Expression.Constant(num)
                );
            return expression;
        }
    }
}