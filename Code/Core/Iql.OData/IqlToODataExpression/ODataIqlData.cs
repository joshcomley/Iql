using System.Collections.Generic;

namespace Iql.OData.IqlToODataExpression
{
    public class ODataIqlData
    {
        private bool _expandsInitialized;
        private Dictionary<string, string> _expands;
        public Dictionary<string, string> Expands { get { if(!_expandsInitialized) { _expandsInitialized = true; _expands = new Dictionary<string, string>(); } return _expands; } set { _expandsInitialized = true; _expands = value; } }
    }
}