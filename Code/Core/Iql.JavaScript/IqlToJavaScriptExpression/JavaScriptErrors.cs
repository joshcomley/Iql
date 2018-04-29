using System;

namespace Iql.JavaScript.IqlToJavaScriptExpression
{
    internal class JavaScriptErrors
    {
        public static void OperationNotSupported(IqlExpressionKind kind)
        {
            var typeName = kind.ToString();
            throw new Exception("Operation not supported in JavaScript: " + typeName);
        }
    }
}