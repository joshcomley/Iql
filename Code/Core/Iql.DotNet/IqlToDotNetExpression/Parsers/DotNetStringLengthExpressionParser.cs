using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetStringLengthExpressionParser : DotNetActionParserBase<IqlStringLengthExpression>
    {
        static DotNetStringLengthExpressionParser()
        {
            StringTrimMethod = typeof(string).GetMethods().Single(m => m.Name == nameof(string.Trim) && m.GetParameters().Length == 0);
        }

        public static MethodInfo StringTrimMethod { get; set; }

        public override IqlExpression ToQueryString(IqlStringLengthExpression action,
            DotNetIqlParserInstance parser)
        {
            var parentExpression = parser.Parse(action.Parent
#if TypeScript
                        , null
#endif
            );
            var memberExpression = Expression.Property(parentExpression.Expression,
                nameof(string.Length)
            );
            IqlExpression expression =
                new IqlFinalExpression<Expression>(
                    memberExpression
                );
            return expression;
        }
    }
}