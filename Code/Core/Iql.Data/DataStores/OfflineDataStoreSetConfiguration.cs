using Iql.Data.DataStores.InMemory;

namespace Iql.Data.DataStores
{
    public class OfflineDataStoreSetConfiguration
    {
        public int? PageSize { get; set; }
        public AutoIntegerIdStrategy? IntegerIdStrategy { get; set; }
    }
}