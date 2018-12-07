using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetStringTrimExpressionParser : DotNetActionParserBase<IqlStringTrimExpression>
    {
        static DotNetStringTrimExpressionParser()
        {
            StringTrimMethod = typeof(string).GetMethods().Single(m => m.Name == nameof(string.Trim) && m.GetParameters().Length == 0);
        }

        public static MethodInfo StringTrimMethod { get; set; }

        public override IqlExpression ToQueryString(IqlStringTrimExpression action,
            DotNetIqlParserContext parser)
        {
            var parentExpression = parser.Parse(action.Parent
#if TypeScript
                        , null
#endif
            );
            var methodCallExpression = Expression.Call(parentExpression.Expression,
                StringTrimMethod
            );
            IqlExpression expression =
                new IqlFinalExpression<Expression>(
                    methodCallExpression
                );
            return expression;
        }
    }
}