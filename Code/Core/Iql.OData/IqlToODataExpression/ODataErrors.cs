using System;

namespace Iql.OData.IqlToODataExpression
{
    public class ODataErrors
    {
        public static void OperationNotSupported(IqlExpressionKind kind)
        {
            var typeName = kind.ToString();
            throw new Exception("Operation not supported in OData: " + typeName);
        }
    }
}