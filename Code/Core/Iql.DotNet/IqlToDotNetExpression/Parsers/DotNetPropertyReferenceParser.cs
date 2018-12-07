using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetPropertyReferenceParser : DotNetActionParserBase<IqlPropertyExpression>
    {
        public override IqlExpression ToQueryString(IqlPropertyExpression action,
            DotNetIqlParserContext parser)
        {
            var dotNetOutput = parser.Parse(action.Parent
#if TypeScript
                        , null
#endif
            );
            IqlExpression expression =
                new IqlFinalExpression<Expression>(
                    Expression.PropertyOrField(dotNetOutput.Expression, action.PropertyName)
                );
            return expression;
        }
    }
}