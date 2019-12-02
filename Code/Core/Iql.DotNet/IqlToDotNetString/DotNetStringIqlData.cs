using System.Collections.Generic;

namespace Iql.DotNet.IqlToDotNetString
{
    public class DotNetStringIqlData
    {
        private List<IqlBinaryExpression> _alreadyCoalesced = null;
        public List<IqlBinaryExpression> AlreadyCoalesced => _alreadyCoalesced = _alreadyCoalesced ?? new List<IqlBinaryExpression>();
    }
}