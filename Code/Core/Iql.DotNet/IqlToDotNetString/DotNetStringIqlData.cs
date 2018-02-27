using System.Collections.Generic;

namespace Iql.DotNet.IqlToDotNetString
{
    public class DotNetStringIqlData
    {
        public List<IqlBinaryExpression> AlreadyCoalesced { get; } = new List<IqlBinaryExpression>();
    }
}