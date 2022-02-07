using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Iql.Extensions;
using Iql.Serialization;
using Newtonsoft.Json;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptLiteralParser : JavaScriptActionParserBase<IqlLiteralExpression>
    {
        private static Dictionary<string, object> _globalObjects;
        private static Dictionary<string, object> GlobalObjects => _globalObjects = _globalObjects ?? new Dictionary<string, object>();

        public static object _PopIqlTemporaryGlobalObject(string key)
        {
            var obj = GlobalObjects[key];
            GlobalObjects.Remove(key);
            return obj;
        }

        public override IqlExpression ToQueryString(IqlLiteralExpression action,
            JavaScriptIqlParserContext parser)
        {
            if (action != null && action.Value != null && action.Value is IqlLiteralExpression literal)
            {
                action = literal;
            }
            if (action.Value != null && action.Value is IqlExpression)
            {
                var exp = action.Value as IqlExpression;
                if (exp.Kind == IqlExpressionKind.Now)
                {
                    return new IqlFinalExpression<string>("new Date()");
                }
                //return new IqlFinalExpression<string>(
                //    "JSON.parse(`" + IqlJsonSerializer.Serialize(action.Value) + "`)");
            }
            if (action.ReturnType == IqlType.Guid)
            {
                return new IqlFinalExpression<string>(action.Value == null ? "null" : $@"'{action.Value}'");
            }
            if (action.ReturnType == IqlType.String || (action.ReturnType == IqlType.Unknown && action.InferredReturnType == IqlType.String))
            {
                var str = action.Value as string;
                if (action.Value != null)
                {
#if !TypeScript
                    str = Regex.Replace(str, @"\\", @"\\\\");
                    str = Regex.Replace(str, @"'", @"\\\\'");
                    str = Regex.Replace(str, @"""", @"\\\""");
#else
                    // ReSharper disable once RedundantCast
                    str = Regex.Replace(str, @"\\", @"\\");
                    str = Regex.Replace(str, @"'", @"\'");
                    str = Regex.Replace(str, @"""", @"\""");
                    str = Regex.Replace(str, "\n", "\\n");
#endif
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

            if (action.Value != null)
            {
                var type = action.Value.GetType().ToIqlType();
                if (type == IqlType.Unknown ||
                    type == IqlType.Class ||
                    type == IqlType.Collection ||
                    action.Value.ClaimsToBeIql())
                {
                    var guid = Guid.NewGuid().ToString();
                    GlobalObjects.Add(guid, action.Value);
                    Func<object> getter = () => JavaScriptLiteralParser._PopIqlTemporaryGlobalObject("MYGUID");
                    var getterStr = getter.ToString().Replace("MYGUID", guid);
                    return new IqlFinalExpression<string>($"({getterStr})()");
                }
            }

            return new IqlFinalExpression<string>(action.Value == null ? "null" : action.Value?.ToString());
        }
    }
}