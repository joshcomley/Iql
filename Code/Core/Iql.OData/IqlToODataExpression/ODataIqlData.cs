using System.Collections.Generic;

namespace Iql.OData.IqlToODataExpression
{
    public class ODataIqlData
    {
        private Dictionary<string, string> _expands = null;
        public Dictionary<string, string> Expands { get => _expands = _expands ?? new Dictionary<string, string>(); set => _expands = value; }
    }
}