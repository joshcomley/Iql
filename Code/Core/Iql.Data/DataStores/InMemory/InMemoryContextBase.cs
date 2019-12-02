using System.Collections.Generic;

namespace Iql.Data.DataStores.InMemory
{
    public class GlobalContext
    {
        private static Dictionary<string, object> _globalVariables;
        public static Dictionary<string, object> GlobalVariables => _globalVariables = _globalVariables ?? new Dictionary<string, object>();
    }
}