using System;
using System.Collections;
using System.Collections.Generic;

namespace Iql.Queryable.Data.DataStores.InMemory
{
    public class InMemoryQueryResult
    {
        public Dictionary<Type, IList> AllData { get; set; }
        public IList Root { get; set; }
        public InMemoryQueryResult(Dictionary<Type, IList> allData, IList root = null)
        {
            AllData = allData;
            Root = root;
        }
    }
}