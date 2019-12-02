using System.Collections.Generic;

namespace Iql.JavaScript.IqlToJavaScriptExpression
{
    public class JavaScriptIqlData
    {
        private List<IqlBinaryExpression> _alreadyCoalesced;
        public List<IqlBinaryExpression> AlreadyCoalesced => _alreadyCoalesced = _alreadyCoalesced ?? new List<IqlBinaryExpression>();
    }
}