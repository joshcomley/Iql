using System.Collections.Generic;

namespace Iql.DotNet.IqlToDotNetExpression
{
    public class DotNetIqlData
    {
        private List<IqlBinaryExpression> _alreadyCoalesced = null;
        public List<IqlBinaryExpression> AlreadyCoalesced => _alreadyCoalesced = _alreadyCoalesced ?? new List<IqlBinaryExpression>();
    }
}