using System.Collections.Generic;

namespace Iql.JavaScript.IqlToJavaScript
{
    public class JavaScriptIqlData
    {
        public List<IqlBinaryExpression> AlreadyCoalesced { get; } = new List<IqlBinaryExpression>();
    }
}