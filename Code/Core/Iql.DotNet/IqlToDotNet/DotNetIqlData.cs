using System.Collections.Generic;

namespace Iql.DotNet.IqlToDotNet
{
    public class DotNetIqlData
    {
        public List<IqlBinaryExpression> AlreadyCoalesced { get; } = new List<IqlBinaryExpression>();
    }
}