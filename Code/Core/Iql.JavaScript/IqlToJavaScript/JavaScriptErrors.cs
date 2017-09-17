using System;

namespace Iql.JavaScript.IqlToJavaScript
{
    internal class JavaScriptErrors
    {
        public static void OperationNotSupported(IqlExpressionType type)
        {
            throw new Exception("Operation not supported in JavaScript: " + Iql.ExpressionTypes.ResolveName(type));
        }
    }
}