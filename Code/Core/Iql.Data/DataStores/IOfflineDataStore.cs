using System;
using System.Collections;
using System.Collections.Generic;

namespace Iql.Data.DataStores
{
    public interface IOfflineDataStore : IDataStore
    {
        void SynchroniseData(Dictionary<Type, IList> data);
    }
}