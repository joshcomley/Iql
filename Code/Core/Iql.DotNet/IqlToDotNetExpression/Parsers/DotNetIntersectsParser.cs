using System.Linq.Expressions;
using System.Reflection;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetIntersectsParser : DotNetActionParserBase<IqlIntersectsExpression>
    {
        static DotNetIntersectsParser()
        {
            IntersectsMethod = typeof(IqlPointExpression).GetMethod(nameof(IqlPointExpression.IntersectsPolygon),
                BindingFlags.Static | BindingFlags.Public);
        }

        public static MethodInfo IntersectsMethod { get; set; }

        public override IqlExpression ToQueryString(
            IqlIntersectsExpression action,
            DotNetIqlParserContext parser)
        {
            var leftOutput = parser.Parse(action.Parent
#if TypeScript
                        , null
#endif
            );
            var rightOutput = parser.Parse(action.Polygon
#if TypeScript
                        , null
#endif
            );
            return new IqlFinalExpression<Expression>(
                Expression.Call(null,
                    IntersectsMethod,
                    Expression.Property(leftOutput.Expression, nameof(IqlPointExpression.X)),
                    Expression.Property(leftOutput.Expression, nameof(IqlPointExpression.Y)),
                    rightOutput.Expression
                    ));
        }
    }
}