using System;

namespace Iql.OData.IqlToODataExpression
{
    public class ODataErrors
    {
        public static void OperationNotSupported(IqlExpressionType type)
        {
            var typeName = type.ToString();
            throw new Exception("Operation not supported in OData: " + typeName);
        }
    }
}