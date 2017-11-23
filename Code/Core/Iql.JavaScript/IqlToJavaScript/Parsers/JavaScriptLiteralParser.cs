using System.Text.RegularExpressions;

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
                        new IqlFinalExpression<string>("'"),
                        new IqlFinalExpression<string>(str),
                        new IqlFinalExpression<string>("'")
                    );
                }
                return new IqlFinalExpression<string>("null");
            }
            return new IqlFinalExpression<string>(action.Value?.ToString());
        }
    }
}