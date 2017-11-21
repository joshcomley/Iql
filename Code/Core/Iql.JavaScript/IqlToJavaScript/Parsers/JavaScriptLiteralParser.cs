using System.Text.RegularExpressions;
using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptLiteralParser : JavaScriptActionParserBase<IqlLiteralExpression>
    {
        public override IqlExpression ToQueryString(IqlLiteralExpression action,
            JavaScriptIqlParserInstance parser)
        {
            if (action.ReturnType == IqlType.String)
            {
                var str = action.Value as string;
                if (action.Value != null)
                {
                    str = Regex.Replace(str, @"\\", @"\\\\");
                    str = Regex.Replace(str, @"'", @"\\\\'");
                    str = Regex.Replace(str, @"""", @"\\\""");
                    return new IqlAggregateExpression(
                        new IqlFinalExpression("'"),
                        new IqlFinalExpression(str),
                        new IqlFinalExpression("'")
                    );
                }
                return new IqlFinalExpression("null");
            }
            return new IqlFinalExpression(action.Value?.ToString());
        }
    }
}