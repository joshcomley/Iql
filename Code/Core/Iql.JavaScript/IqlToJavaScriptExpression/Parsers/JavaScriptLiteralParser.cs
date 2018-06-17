using System;
using System.Text.RegularExpressions;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptLiteralParser : JavaScriptActionParserBase<IqlLiteralExpression>
    {
        public override IqlExpression ToQueryString(IqlLiteralExpression action,
            JavaScriptIqlParserInstance parser)
        {
            if (action.ReturnType == IqlType.String || (action.ReturnType == IqlType.Unknown && action.InferredReturnType == IqlType.String))
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

            if (action.ReturnType == IqlType.Date ||
                      action.Value is DateTime)
            {
                return new IqlFinalExpression<string>($"new Date(\'{action.Value}\')");
            }

            if (action.ReturnType == IqlType.Boolean ||
                      action.Value is bool)
            {
                return new IqlFinalExpression<string>(action.Value == null ? "null" : ((bool)action.Value ? "true" : "false"));
            }
            return new IqlFinalExpression<string>(action.Value == null ? "null" : action.Value?.ToString());
        }
    }
}