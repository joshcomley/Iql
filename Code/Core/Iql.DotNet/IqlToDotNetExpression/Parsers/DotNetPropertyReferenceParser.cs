using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetPropertyReferenceParser : DotNetActionParserBase<IqlPropertyExpression>
    {
        public override IqlExpression ToQueryString(IqlPropertyExpression action,
            DotNetIqlParserInstance parser)
        {
            IqlExpression expression =
                new IqlFinalExpression<Expression>(
                    Expression.PropertyOrField(parser.Parse(action.Parent
#if TypeScript
                        , null
#endif
                    ).Expression, action.PropertyName)
                );
            return expression;
        }
    }
}