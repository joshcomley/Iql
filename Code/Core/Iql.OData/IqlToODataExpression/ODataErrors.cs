using System;

namespace Iql.OData.IqlToODataExpression
{
    public class ODataErrors
    {
        public static void OperationNotSupported(IqlExpressionType type)
        {
            throw new Exception("Operation not supported in OData: " + Iql.ExpressionTypes.ResolveName(type));
        }
    }
}