using System.Linq;
using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetConditionActionParser : DotNetActionParserBase<IqlConditionExpression>
    {
        public override IqlExpression ToQueryString(IqlConditionExpression action, DotNetIqlParserInstance parser)
        {
            var test = parser.Parse(action.Test).Expression;
            var ifTrue = parser.Parse(action.IfTrue).Expression;
            var ifFalse = parser.Parse(action.IfFalse).Expression;
            if (ifTrue.Type != ifFalse.Type)
            {
                var resolvedType = new[] {ifTrue.Type, ifFalse.Type}.FirstOrDefault(_ => _ != typeof(object))?? ifTrue.Type;
                if (ifTrue.Type != resolvedType)
                {
                    ifTrue = Expression.Convert(ifTrue, resolvedType);
                }
                if (ifFalse.Type != resolvedType)
                {
                    ifFalse = Expression.Convert(ifFalse, resolvedType);
                }
            }
            return new IqlFinalExpression<Expression>(
                Expression.Condition(
                    test,
                    ifTrue,
                    ifFalse));
        }
    }
}