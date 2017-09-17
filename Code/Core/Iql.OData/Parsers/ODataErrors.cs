using System;

namespace Iql.OData.Parsers
{
    public class ODataErrors
    {
        public static void OperationNotSupported(IqlExpressionType type)
        {
            throw new Exception("Operation not supported in OData: " + Iql.ExpressionTypes.ResolveName(type));
        }
    }
}