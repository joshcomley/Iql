using System.Collections.Generic;

namespace Iql.DotNet.IqlToDotNetExpression
{
    public class DotNetIqlData
    {
        public List<IqlBinaryExpression> AlreadyCoalesced { get; } = new List<IqlBinaryExpression>();
    }
}