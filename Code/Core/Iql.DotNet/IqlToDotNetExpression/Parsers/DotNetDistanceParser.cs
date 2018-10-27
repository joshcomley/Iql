using System.Linq.Expressions;
using System.Reflection;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetDistanceParser : DotNetActionParserBase<IqlDistanceExpression>
    {
        static DotNetDistanceParser()
        {
            DistanceBetweenMethod = typeof(IqlPointExpression).GetMethod(nameof(IqlPointExpression.DistanceBetween),
                BindingFlags.Static | BindingFlags.Public);
        }

        public static MethodInfo DistanceBetweenMethod { get; set; }

        public override IqlExpression ToQueryString(IqlDistanceExpression action, DotNetIqlParserInstance parser)
        {
            var leftOutput = parser.Parse(action.Left
#if TypeScript
                        , null
#endif
            );
            var rightOutput = parser.Parse(action.Right
#if TypeScript
                        , null
#endif
            );
            return new IqlFinalExpression<Expression>(Expression.Call(null, DistanceBetweenMethod,
                    Expression.Property(leftOutput.Expression, nameof(IqlPointExpression.Y)),
                    Expression.Property(leftOutput.Expression, nameof(IqlPointExpression.X)),
                    Expression.Property(rightOutput.Expression, nameof(IqlPointExpression.Y)),
                    Expression.Property(rightOutput.Expression, nameof(IqlPointExpression.X)),
                    Expression.Constant(IqlDistanceKind.Kilometers)
                )
            );
        }
    }
}