using System.Linq;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptInvocationParser : JavaScriptActionParserBase<IqlInvocationExpression>
    {
        public override IqlExpression ToQueryString(IqlInvocationExpression action,
            JavaScriptIqlParserContext parser)
        {
            var parentOutput = action.Parent != null
                ? parser.Parse(action.Parent
#if TypeScript
                        , null
#endif
                )
                : null;
            var parent = parentOutput != null ? parentOutput.ToCodeString() : null;
            var parts = new string[]
            {
                string.IsNullOrWhiteSpace(parent) ? "" : $"{parent}.",
                action.MethodName,
                "(",
                string.Join(", ", action.Parameters.Select(p => parser.ParseExpression(p).ToCodeString())),
                ")"
            };
            IqlExpression expression =
                new IqlFinalExpression<string>(string.Join("", parts)
                );
            return expression;
            //var accessorExpression =
            //    parent == null
            //        ? propertyName
            //        : parent.DotAccess(propertyName);
            //return accessorExpression;
        }
    }
}