using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetStringSubStringExpressionParser : DotNetActionParserBase<IqlStringSubStringExpression>
    {
        static DotNetStringSubStringExpressionParser()
        {
            StringSubStringWithoutTakeMethod = typeof(string).GetMethods().Single(m => m.Name == nameof(string.Substring) && m.GetParameters().Length == 1);
            StringSubStringWithTakeMethod = typeof(string).GetMethods().Single(m => m.Name == nameof(string.Substring) && m.GetParameters().Length == 2);
        }

        public static MethodInfo StringSubStringWithoutTakeMethod { get; set; }
        public static MethodInfo StringSubStringWithTakeMethod { get; set; }

        public override IqlExpression ToQueryString(IqlStringSubStringExpression action,
            DotNetIqlParserInstance parser)
        {
            var parentExpression = parser.Parse(action.Parent
#if TypeScript
                        , null
#endif
            );
            MethodCallExpression methodCallExpression;
            if (action.Take != null)
            {
                methodCallExpression = Expression.Call(parentExpression.Expression,
                    StringSubStringWithTakeMethod,
                    parser.Parse(action.Value
#if TypeScript
                        , null
#endif
                    ).Expression,
                    parser.Parse(action.Take
#if TypeScript
                        , null
#endif
                    ).Expression
                );
            }
            else
            {
                methodCallExpression = Expression.Call(parentExpression.Expression,
                    StringSubStringWithoutTakeMethod,
                    parser.Parse(action.Value
#if TypeScript
                        , null
#endif
                    ).Expression
                );
            }
            IqlExpression expression =
                new IqlFinalExpression<Expression>(
                    methodCallExpression
                );
            return expression;
        }
    }
}