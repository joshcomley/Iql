using System.Collections.Generic;

namespace Iql.OData.IqlToODataExpression
{
    public class ODataIqlData
    {
        public Dictionary<string, string> Expands { get; set; } = new Dictionary<string, string>();
    }
}