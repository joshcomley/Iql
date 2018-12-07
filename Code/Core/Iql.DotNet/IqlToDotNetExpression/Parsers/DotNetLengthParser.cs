using System.Linq.Expressions;
using System.Reflection;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetLengthParser : DotNetActionParserBase<IqlLengthExpression>
    {
        static DotNetLengthParser()
        {
            LengthMethod = typeof(IqlLineExpression).GetMethod(nameof(IqlLineExpression.Length),
                BindingFlags.Instance | BindingFlags.Public);
        }

        public static MethodInfo LengthMethod { get; set; }

        public override IqlExpression ToQueryString(
            IqlLengthExpression action,
            DotNetIqlParserContext parser)
        {
            var line = parser.Parse(action.Parent
#if TypeScript
                        , null
#endif
            );
            return new IqlFinalExpression<Expression>(
                Expression.Call(line.Expression,
                    LengthMethod
                ));
        }
    }
}