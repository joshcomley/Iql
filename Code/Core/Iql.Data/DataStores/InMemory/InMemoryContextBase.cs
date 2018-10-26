using System.Collections.Generic;

namespace Iql.Data.DataStores.InMemory
{
    public class GlobalContext
    {
        public static Dictionary<string, object> GlobalVariables { get; }
            = new Dictionary<string, object>();
    }
}