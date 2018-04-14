using System;

namespace Iql.JavaScript.IqlToJavaScriptExpression
{
    internal class JavaScriptErrors
    {
        public static void OperationNotSupported(IqlExpressionType type)
        {
            var typeName = type.ToString();
            throw new Exception("Operation not supported in JavaScript: " + typeName);
        }
    }
}